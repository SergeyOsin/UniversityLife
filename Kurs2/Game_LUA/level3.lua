local composer = require("composer")
local physics = require("physics")
local audio = require("audio")
local scene = composer.newScene()

-- =============================================================
-- ПЕРЕМЕННЫЕ
-- =============================================================
local player, floor
local count = 0
local life = 1
local scoreText, livesText
local menuBtn
local onCollision

local spawnLetT, spawnSnT, spawnSandT, spawnFrogT, cleanupT
local gameActive = false
local spawnedObjects = {}
local sceneGroup
local CountToWin = 10

local bgMusic, eatSound, winSound, failSound
local bgChannel

local FRAME_W = 386
local FRAME_H = 3240/6
local SCALE = 0.25

-- =============================================================
-- ФУНКЦИИ ОБНОВЛЕНИЯ
-- =============================================================
local function updateText()
    if scoreText then scoreText.text = "Счет: " .. count end
    if livesText then livesText.text = "Жизни: " .. life end
end

local function clearSpawnedObjects()
    if spawnedObjects then
        for i = #spawnedObjects, 1, -1 do
            local obj = spawnedObjects[i]
            if obj and obj.removeSelf then
                display.remove(obj)
            end
            table.remove(spawnedObjects, i)
        end
    end
end

local function stopLevelActions()
    gameActive = false
    if physics then physics.pause() end
    display.currentStage:setFocus(nil)
    
    if spawnLetT then timer.cancel(spawnLetT); spawnLetT = nil end
    if spawnSnT then timer.cancel(spawnSnT); spawnSnT = nil end
    if spawnSandT then timer.cancel(spawnSandT); spawnSandT = nil end
    if spawnFrogT then timer.cancel(spawnFrogT); spawnFrogT = nil end
    if cleanupT then timer.cancel(cleanupT); cleanupT = nil end

    Runtime:removeEventListener("collision", onCollision)

    -- Очистку объектов делаем с микро-задержкой, чтобы не упало при коллизии
    timer.performWithDelay(1, clearSpawnedObjects)
    
    if bgChannel then audio.stop(bgChannel); bgChannel = nil end
    if player and player.pause then player:pause() end
end

local function showLevelCompleteDialog()
    if not gameActive then return end
    if winSound then audio.play(winSound) end
    stopLevelActions()
    composer.showOverlay("levelComplete", { isModal = true, effect = "fade", time = 300, params = { parentScene = "level3", nextLevel = "level4" } })
end

local function showLevelFailedDialog()
    if not gameActive then return end
    if failSound then audio.play(failSound) end
    stopLevelActions()
    composer.showOverlay("levelFailed", { isModal = true, effect = "fade", time = 300, params = { parentScene = "level3" } })
end

local function addScore(value)
    if not gameActive then return end
    count = count + value
    updateText()
    if eatSound then audio.play(eatSound) end
    if count >= CountToWin then showLevelCompleteDialog() end
end

-- =============================================================
-- ДВИЖЕНИЕ ИГРОКА
-- =============================================================
local function movePlayer(event)
    if not gameActive or not player then return true end
    if event.phase == "began" then
        display.currentStage:setFocus(player)
        player.isFocus = true
        player.offset = event.x - player.x
    elseif player.isFocus then
        if event.phase == "moved" then
            local x = event.x - player.offset
            local hw = 40
            if x < hw then x = hw end
            if x > display.contentWidth - hw then x = display.contentWidth - hw end
            player.x = x
        else
            display.currentStage:setFocus(nil)
            player.isFocus = false
        end
    end
    return true
end

-- Функция безопасного удаления объектов (ГЛАВНЫЙ ФИКС)
local function safeRemove(obj)
    if obj and obj.removeSelf then
        obj.isVisible = false 
        -- Удаляем в следующем кадре, чтобы физика успела "разблокироваться"
        timer.performWithDelay(1, function()
            display.remove(obj)
        end)
    end
end

function onCollision(event)
    if not gameActive or event.phase ~= "began" then return end
    local a, b = event.object1, event.object2
    
    local other = (a.ID == "player") and b or ((b.ID == "player") and a or nil)
    if not other or other.isConsumed then return end

    other.isConsumed = true -- Защита от двойного срабатывания

    if other.ID == "doshik" or other.ID == "sandwich" then
        addScore(1)
        safeRemove(other)
    elseif other.ID == "shaurma" then
        addScore(5)
        safeRemove(other)
    elseif other.ID == "frog" then
        safeRemove(other)
        showLevelFailedDialog()
    end
end

-- =============================================================
-- СПАВН ОБЪЕКТОВ
-- =============================================================
local function spawnObj(img, id, w, h, grav)
    if not gameActive then return end
    local obj = display.newImageRect(sceneGroup, img, w, h)
    if obj then
        obj.x = math.random(w, display.contentWidth - w)
        obj.y = -50
        obj.ID = id
        obj.isConsumed = false
        physics.addBody(obj, "dynamic", {isSensor = true})
        obj.gravityScale = grav or 0.3
        table.insert(spawnedObjects, obj)
    end
end

local function cleanupObjects()
    if not spawnedObjects then return end
    for i = #spawnedObjects, 1, -1 do
        local obj = spawnedObjects[i]
        if obj and (not obj.y or obj.y > display.contentHeight + 100 or obj.isConsumed) then
            display.remove(obj)
            table.remove(spawnedObjects, i)
        end
    end
end

-- =============================================================
-- СОЗДАНИЕ СЦЕНЫ
-- =============================================================
function scene:create(event)
    sceneGroup = self.view
    physics.start()
    physics.pause()

    bgMusic = audio.loadStream("sounds/level3_music.mp3")
    eatSound = audio.loadSound("sounds/eating.mp3")
    winSound = audio.loadSound("sounds/win.mp3")
    failSound = audio.loadSound("sounds/fail.mp3")

    local bg = display.newImageRect(sceneGroup, "pictures/background_level3.png", display.contentWidth, display.contentHeight)
    bg.x, bg.y = display.contentCenterX, display.contentCenterY

    floor = display.newImageRect(sceneGroup, "pictures/floor.png", display.contentWidth, 40)
    floor.x, floor.y = display.contentCenterX, display.contentHeight - 20
    physics.addBody(floor, "static")

    local sheetOptions = { width = FRAME_W, height = FRAME_H, numFrames = 36 }
    local playerSheet = graphics.newImageSheet("pictures/stud_up.png", sheetOptions)

    if not playerSheet then
        player = display.newRect(sceneGroup, 0, 0, 50, 100)
    else
        local seqData = { { name = "dance", start = 1, count = 36, time = 5000, loopCount = 0 } }
        player = display.newSprite(sceneGroup, playerSheet, seqData)
    end

    player.x, player.y = display.contentCenterX, display.contentHeight - 80
    player.xScale, player.yScale = SCALE, SCALE
    player.ID = "player"
    physics.addBody(player, "dynamic", {isSensor = true, radius = 30})
    player.gravityScale = 0
    player:addEventListener("touch", movePlayer)

    livesText = display.newText(sceneGroup, "Жизни: 1", 70, 50, native.systemFontBold, 22)
    scoreText = display.newText(sceneGroup, "Счет: 0", display.contentWidth - 70, 50, native.systemFontBold, 22)
    livesText:setFillColor(0,0,0); scoreText:setFillColor(0,0,0)

    menuBtn = display.newText(sceneGroup, "Меню", 60, 20, native.systemFontBold, 20)
    menuBtn:setFillColor(0,0,0)
    menuBtn:addEventListener("tap", function() stopLevelActions(); composer.gotoScene("menu") end)
end

function scene:show(event)
    if event.phase == "will" then
        count = 0
        gameActive = true
        CountToWin = math.random(9, 13)
        updateText()
        player.x = display.contentCenterX
        player.y = display.contentHeight - 60
        if player.setSequence then
            player:setSequence("dance")
            player:play()
        end
    elseif event.phase == "did" then
        physics.start()
        if bgMusic then bgChannel = audio.play(bgMusic, {loops = -1, volume = 0.5}) end
        
        spawnLetT = timer.performWithDelay(1500, function() spawnObj("pictures/doshik.png", "doshik", 40, 30, 0.3) end, 0)
        spawnSnT = timer.performWithDelay(5000, function() spawnObj("pictures/shaurma.png", "shaurma", 30, 30, 0.3) end, 0)
        spawnSandT = timer.performWithDelay(7000, function() spawnObj("pictures/sandwich.png", "sandwich", 35, 25, 0.3) end, 0)
        spawnFrogT = timer.performWithDelay(3000, function() spawnObj("pictures/frog.png", "frog", 35, 35, 0.4) end, 0)
        cleanupT = timer.performWithDelay(500, cleanupObjects, 0)

        Runtime:addEventListener("collision", onCollision)
    end
end

function scene:hide(event)
    if event.phase == "will" then
        stopLevelActions()
    end
end

function scene:destroy(event)
    if bgMusic then audio.dispose(bgMusic) end
    if eatSound then audio.dispose(eatSound) end
    if winSound then audio.dispose(winSound) end
    if failSound then audio.dispose(failSound) end
end

scene:addEventListener("create", scene)
scene:addEventListener("show", scene)
scene:addEventListener("hide", scene)
scene:addEventListener("destroy", scene)

return scene