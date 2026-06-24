local composer = require("composer")
local scene = composer.newScene()

-- =============================================================
-- ПЕРЕМЕННЫЕ УРОВНЯ
-- =============================================================
local player, door, scoreText, sceneGroup
local correctCircles = {}
local wrongCircles = {}
local collectedCount = 0
local gameActive = false
local stage = 1
local totalToCollect = 8
local moveTimer, bgMusicChannel

local bgMusic, winSound, failSound, touchSound

local wallSize = 30 

-- НАСТРОЙКИ СПРАЙТА
local COLS = 5
local ROWS = 5
local FRAME_W = 1280 / COLS 
local FRAME_H = 1280 / ROWS 
local baseScale = 1         
local lastX, lastY = 0, 0   

local LAB_SPEED = 3 

-- Предварительное объявление функций для корректного удаления слушателей
local enterFrameUpdate
local moveLabs
local checkCollision

-- =============================================================
-- ФУНКЦИИ ЛОГИКИ
-- =============================================================

local function updateText()
    if scoreText then
        scoreText.text = "Сдано лаб. работ: " .. collectedCount
    end
end

local function cleanupLabs()
    if correctCircles then
        for i = #correctCircles, 1, -1 do
            display.remove(correctCircles[i])
            table.remove(correctCircles, i)
        end
    end
    if wrongCircles then
        for i = #wrongCircles, 1, -1 do
            display.remove(wrongCircles[i])
            table.remove(wrongCircles, i)
        end
    end
end

local function stopGame()
    gameActive = false
    
    -- ГАРАНТИРОВАННОЕ УДАЛЕНИЕ СЛУШАТЕЛЕЙ
    Runtime:removeEventListener("enterFrame", enterFrameUpdate)
    
    if moveTimer then
        timer.cancel(moveTimer)
        moveTimer = nil
    end

    if bgMusicChannel then
        audio.stop(bgMusicChannel)
        bgMusicChannel = nil
    end

    if player then player:pause() end
    if door then display.remove(door); door = nil end
    
    cleanupLabs()
end

local function failLevel()
    if not gameActive then return end
    stopGame()
    if failSound then audio.play(failSound) end
    composer.showOverlay("levelFailed", { 
        isModal = true, effect = "fade", time = 300, 
        params = { parentScene = "level4" } 
    })
end

local function completeLevel()
    if not gameActive then return end
    stopGame()
    if winSound then audio.play(winSound) end
    composer.showOverlay("levelComplete", { 
        isModal = true, effect = "fade", time = 300, 
        params = { parentScene = "level4", nextLevel = "level5" } 
    })
end

local function spawnLabs(currentStage)
    if not gameActive then return end
    cleanupLabs()

    local function randX() return math.random(wallSize + 40, display.contentWidth - wallSize - 40) end
    local function randY() return math.random(120, 350) end

    local countCorrect = (currentStage == 1) and 5 or 3
    local countWrong = (currentStage == 1) and 3 or 2

    for i = 1, countCorrect do
        local o = display.newImageRect(sceneGroup, "pictures/lab.png", 30, 30)
        o.x, o.y = randX(), randY()
        o.speed = LAB_SPEED
        o.dir = (math.random(0,1)==1) and 1 or -1
        table.insert(correctCircles, o)
    end
    for i = 1, countWrong do
        local o = display.newImageRect(sceneGroup, "pictures/lose.png", 30, 30)
        o.x, o.y = randX(), randY()
        o.speed = LAB_SPEED
        o.dir = (math.random(0,1)==1) and 1 or -1
        table.insert(wrongCircles, o)
    end
end

checkCollision = function()
    if not gameActive or not player then return end

    for i = #correctCircles, 1, -1 do
        local c = correctCircles[i]
        if c and c.x then
            local dist = math.sqrt((player.x - c.x)^2 + (player.y - c.y)^2)
            if dist < 40 then
                collectedCount = collectedCount + 1
                updateText()
                if touchSound then audio.play(touchSound) end
                display.remove(c)
                table.remove(correctCircles, i)
            end
        end
    end

    for i = #wrongCircles, 1, -1 do
        local w = wrongCircles[i]
        if w and w.x then
            local dist = math.sqrt((player.x - w.x)^2 + (player.y - w.y)^2)
            if dist < 35 then
                failLevel()
                return
            end
        end
    end

    -- ✅ ПЕРЕХОД НА ВТОРУЮ ФАЗУ + РЕСПАВН ВНИЗУ
     if stage == 1 and #correctCircles == 0 then
        stage = 2
        
        -- ✅ ПРИНУДИТЕЛЬНЫЙ РЕСПАВН (игнорирует касание)
        player.x = display.contentCenterX
        player.y = display.contentHeight - 80
        
        lastX, lastY = player.x, player.y
        player:pause()
        
        spawnLabs(2)
        return 
    end

    if collectedCount >= totalToCollect and not door then
        door = display.newImageRect(sceneGroup, "pictures/door.png", 140, 140)
        door.x, door.y = display.contentCenterX, 85
    end

    if door and math.abs(player.x - door.x) < 60 and math.abs(player.y - door.y) < 60 then
        completeLevel()
    end
end

-- РЕАЛИЗАЦИЯ ФУНКЦИЙ ОБНОВЛЕНИЯ
enterFrameUpdate = function()
    if not gameActive or not player then return end
    local isMoving = (math.abs(player.x - lastX) > 0.1 or math.abs(player.y - lastY) > 0.1)
    if isMoving then
        player:play()
        player.xScale = (player.x < display.contentCenterX) and -baseScale or baseScale
    else
        player:pause() 
    end
    lastX, lastY = player.x, player.y
    checkCollision()
end

moveLabs = function()
    if not gameActive then return end
    local lists = {correctCircles, wrongCircles}
    for _, t in ipairs(lists) do
        for i = 1, #t do
            local o = t[i]
            if o and o.x then
                o.x = o.x + o.speed * o.dir
                if o.x < wallSize + 10 or o.x > display.contentWidth - wallSize - 10 then 
                    o.dir = -o.dir 
                end
            end
        end
    end
end

local function movePlayer(event)
    if not gameActive or not player then return true end
    
    if stage == 1 and #correctCircles == 0 then return true end
    
    if event.phase == "moved" then
        local pW, pH = 25, 25
        player.x = math.max(wallSize + pW, math.min(event.x, display.contentWidth - wallSize - pW))
        player.y = math.max(wallSize + pH, math.min(event.y, display.contentHeight - wallSize - pH))
    end
    return true
end

-- =============================================================
-- ЖИЗНЕННЫЙ ЦИКЛ СЦЕНЫ
-- =============================================================

function scene:create(event)
    sceneGroup = self.view
    bgMusic = audio.loadStream("sounds/level4_music.mp3")
    winSound = audio.loadSound("sounds/win.mp3")
    failSound = audio.loadSound("sounds/fail.mp3")
    touchSound = audio.loadSound("sounds/touchWithWin.mp3")

    local bg = display.newImageRect(sceneGroup, "pictures/background_level4.png", display.contentWidth, display.contentHeight)
    bg.x, bg.y = display.contentCenterX, display.contentCenterY
    bg:addEventListener("touch", movePlayer)

    local sheetOptions = { width = FRAME_W, height = FRAME_H, numFrames = COLS * ROWS }
    local playerSheet = graphics.newImageSheet("pictures/stud_walk.png", sheetOptions)
    local seqData = { { name="walk", start=1, count=COLS*ROWS, time=1000, loopCount=0 } }

    player = display.newSprite(sceneGroup, playerSheet, seqData)
    baseScale = 60 / FRAME_H
    player.xScale, player.yScale = baseScale, baseScale
    player:setSequence("walk")

    scoreText = display.newText({ parent = sceneGroup, text = "", x = display.contentCenterX, y = 30, font = native.systemFontBold, fontSize = 22 })
    scoreText:setFillColor(0,0,0)

    local menuBtn = display.newText(sceneGroup, "Меню", 60, 50, native.systemFontBold, 20)
    menuBtn:setFillColor(0,0,0)
    menuBtn:addEventListener("tap", function() stopGame(); composer.gotoScene("menu") end)
end

function scene:show(event)
    if event.phase == "will" then
        stopGame() -- Чистим всё перед запуском
        gameActive = true
        collectedCount = 0
        stage = 1
        updateText()
        spawnLabs(1)
        player.x, player.y = display.contentCenterX, display.contentHeight - 80
        lastX, lastY = player.x, player.y
        player:setSequence("walk")
    elseif event.phase == "did" then
        -- Удаляем на всякий случай, если stopGame не сработал
        Runtime:removeEventListener("enterFrame", enterFrameUpdate)
        if moveTimer then timer.cancel(moveTimer) end

        bgMusicChannel = audio.play(bgMusic, {loops=-1})
        moveTimer = timer.performWithDelay(30, moveLabs, 0)
        Runtime:addEventListener("enterFrame", enterFrameUpdate)
    end
end

function scene:hide(event)
    if event.phase == "will" then
        stopGame()
    end
end

function scene:destroy(event)
    audio.stop()
    local snds = {bgMusic, winSound, failSound, touchSound}
    for i=1,#snds do if snds[i] then audio.dispose(snds[i]) end end
end

scene:addEventListener("create", scene)
scene:addEventListener("show", scene)
scene:addEventListener("hide", scene)
scene:addEventListener("destroy", scene)

return scene