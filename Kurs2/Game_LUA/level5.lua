local composer = require("composer")
local scene = composer.newScene()
local physics = require("physics")

-- =============================================================
-- ПЕРЕМЕННЫЕ УРОВНЯ
-- =============================================================
local player, sceneGroup, scoreText, coinGroup
local cnt = 0
local totalTargets = 5
local gameActive = false
local vx, vy = 0, 0
local moveSpeed = 220
local spawnX, spawnY = 50, 50

local COLS = 5
local ROWS = 5
local FRAME_W = 1280 / COLS 
local FRAME_H = 1280 / ROWS 
local baseScale = 1 

local coinSound, winSound, failSound, bgMusic, musicChannel

-- Предварительное объявление функций
local onCollision
local enterFrameUpdate

-- =============================================================
-- ВСПОМОГАТЕЛЬНЫЕ ФУНКЦИИ
-- =============================================================

local function stopGame()
    gameActive = false
    Runtime:removeEventListener("collision", onCollision)
    Runtime:removeEventListener("enterFrame", enterFrameUpdate)

    vx, vy = 0, 0
    
    if musicChannel and musicChannel > 0 then
        audio.stop(musicChannel)
        musicChannel = nil
    end

    if player and player.setLinearVelocity then
        player:setLinearVelocity(0, 0)
        player.angularVelocity = 0
        player:applyLinearImpulse(0, 0, player.x, player.y)  -- ✅ Сброс импульса
        player:pause()
    end
end

local function clearCoins()
    if coinGroup and coinGroup.numChildren then
        for i = coinGroup.numChildren, 1, -1 do
            display.remove(coinGroup[i])
        end
    end
end

local function failLevel()
    if not gameActive then return end
    stopGame()
    if failSound then audio.play(failSound) end
    composer.showOverlay("levelFailed", {
        isModal = true,
        effect = "fade",
        time = 300,
        params = { parentScene = "level5" }
    })
end

local function spawnLevelObjects()
    clearCoins()
    local coinPositions = { {280,50}, {50,250}, {280,250}, {50,430}, {280,430} }
    for i = 1, #coinPositions do
        local coin = display.newCircle(coinGroup, coinPositions[i][1], coinPositions[i][2], 10)
        coin:setFillColor(1, 0.8, 0)
        coin.myName = "target"
        coin.collected = false
        physics.addBody(coin, "static", { isSensor = true })
    end
end

-- =============================================================
-- ОБРАБОТЧИКИ СОБЫТИЙ
-- =============================================================

onCollision = function(event)
    if not gameActive or event.phase ~= "began" then return end
    
    local a, b = event.object1, event.object2
    if not (a and b and a.myName and b.myName) then return end

    -- Сбор монет
    if (a.myName == "player" and b.myName == "target") or (a.myName == "target" and b.myName == "player") then
        local coin = (a.myName == "target") and a or b
        if not coin.collected then
            coin.collected = true
            timer.performWithDelay(1, function() 
                display.remove(coin) 
            end)
            cnt = cnt + 1
            scoreText.text = "Монеты: " .. cnt .. "/" .. totalTargets
            if coinSound then audio.play(coinSound) end

            if cnt >= totalTargets then
                stopGame()
                if winSound then audio.play(winSound) end
                composer.gotoScene("final", { effect = "fade", time = 500 })
            end
        end
    end

    -- Столкновение с препятствиями
    if (a.myName == "player" and ((b.myName == "wall" and not b.isBorder) or b.myName == "hole")) or
       (b.myName == "player" and ((a.myName == "wall" and not a.isBorder) or a.myName == "hole")) then
        failLevel()
    end
end

local function handleTouch(event)
    if not gameActive or not player then return true end

    if event.phase == "began" or event.phase == "moved" then
        local dx, dy = event.x - player.x, event.y - player.y

        if math.abs(dx) > math.abs(dy) then
            vy = 0
            vx = (dx > 0) and moveSpeed or -moveSpeed
        else
            vx = 0
            vy = (dy > 0) and moveSpeed or -moveSpeed
        end
    elseif event.phase == "ended" or event.phase == "cancelled" then
        vx, vy = 0, 0
    end
    return true
end

enterFrameUpdate = function()
    if not gameActive or not player or not player.setLinearVelocity then return end
    
    if vx == 0 and vy == 0 and player:getLinearVelocity() then
        local currentVx, currentVy = player:getLinearVelocity()
        if math.abs(currentVx) > 1 or math.abs(currentVy) > 1 then
            player:setLinearVelocity(0, 0)
            return
        end
    end
    
    player:setLinearVelocity(vx, vy)

    if vx ~= 0 or vy ~= 0 then
        -- Проигрываем анимацию всех спрайтов при движении
        if player.sequence ~= "walk" then
            player:setSequence("walk")
        end
        player:play()

        -- Поворот влево/вправо с учетом маленького масштаба
        if vx > 0 then
            player.xScale = baseScale
        elseif vx < 0 then
            player.xScale = -baseScale
        end
    else
        -- Остановка анимации
        player:pause()
    end
end

-- =============================================================
-- СЦЕНА
-- =============================================================

function scene:create(event)
    sceneGroup = self.view
    physics.start()
    physics.setGravity(0, 0)
    physics.pause()

    -- Звуки
    coinSound = audio.loadSound("sounds/eating.mp3")
    winSound = audio.loadSound("sounds/win.mp3")
    failSound = audio.loadSound("sounds/fail.mp3")
    bgMusic = audio.loadStream("sounds/level2_music.mp3")

    -- Фон
    local background = display.newImageRect(sceneGroup, "pictures/background_level5.png", display.contentWidth, display.contentHeight)
    background.x, background.y = display.contentCenterX, display.contentCenterY
    background:addEventListener("touch", handleTouch)

    coinGroup = display.newGroup()
    sceneGroup:insert(coinGroup)

   -- Границы экрана (толщина 10 пикселей, сдвинуты внутрь)
local borderThickness = 10
local borders = {
    { display.contentCenterX, borderThickness/2, display.contentWidth, borderThickness }, -- верх
    { display.contentCenterX, display.contentHeight-borderThickness/2, display.contentWidth, borderThickness }, -- низ
    { borderThickness/2, display.contentCenterY, borderThickness, display.contentHeight }, -- лево
    { display.contentWidth-borderThickness/2, display.contentCenterY, borderThickness, display.contentHeight }, -- право
}
for i=1,#borders do
    local b = borders[i]
    local border = display.newRect(sceneGroup, b[1], b[2], b[3], b[4])
    border.alpha = 0
    border.myName = "wall"
    border.isBorder = true
    physics.addBody(border, "static", { bounce=0, friction=0.0 })
end

    -- Стены
    local wallPositions = { {105,155,20,100}, {215,155,20,100},  {160,315,80,20} }
    for i=1,#wallPositions do
        local w = wallPositions[i]
        local wall = display.newImageRect(sceneGroup, "pictures/walls.png", w[3], w[4])
        wall.x, wall.y = w[1], w[2]
        wall.myName = "wall"
        wall.isBorder = false
        physics.addBody(wall, "static", { bounce=0, friction=0.5 })
    end

    -- Ямы
    local holePositions = { {160,150}}
    for i=1,#holePositions do
        local hole = display.newImageRect(sceneGroup, "pictures/luk.png", 40, 20)
        hole.x, hole.y = holePositions[i][1], holePositions[i][2]
        hole.myName = "hole"
        physics.addBody(hole, "static", { isSensor = true })
    end

    -- НАСТРОЙКА ИГРОКА (СПРАЙТЫ)
    local sheetOptions = {
        width = FRAME_W,
        height = FRAME_H,
        numFrames = COLS * ROWS
    }
    local walkSheet = graphics.newImageSheet("pictures/stud_walk.png", sheetOptions)
    
    local seqData = {
        { name="walk", start=1, count=COLS * ROWS, time=800, loopCount=0 }
    }
    
    player = display.newSprite(sceneGroup, walkSheet, seqData)
    
    -- Установка маленького размера (высота 60 пикселей)
    baseScale = 60 / FRAME_H
    player.xScale, player.yScale = baseScale, baseScale
    
    player.x, player.y = spawnX, spawnY
    player.myName = "player"

    -- Физика игрока (радиус 15 для маленького спрайта)
    physics.addBody(player, "dynamic", { radius = 15, bounce=0 })
    player.isFixedRotation = true
    player:setSequence("walk")

    -- UI
    scoreText = display.newText({
        parent = sceneGroup,
        text = "Монеты: 0/" .. totalTargets,
        x = display.contentCenterX,
        y = 30,
        font = native.systemFontBold,
        fontSize = 18
    })

    local menuBtn = display.newText(sceneGroup, "Меню", 50, 30, native.systemFontBold, 20)
    menuBtn:addEventListener("tap", function()
        stopGame()
        composer.gotoScene("menu", { effect = "fade", time = 400 })
    end)
end

function scene:show(event)
    if event.phase == "will" then
        gameActive = false
        cnt = 0
        scoreText.text = "Монеты: 0/" .. totalTargets
        
        -- ✅ ПОЛНЫЙ СБРОС ПЕРЕД physics.start()
        player.x, player.y = spawnX, spawnY
        player:setLinearVelocity(0, 0)
        player.angularVelocity = 0         
        vx, vy = 0, 0                      
        
        spawnLevelObjects()
        
    elseif event.phase == "did" then
        physics.start()
        timer.performWithDelay(100, function()
            gameActive = true
            Runtime:addEventListener("collision", onCollision)
            Runtime:addEventListener("enterFrame", enterFrameUpdate)
            
            player:setLinearVelocity(0, 0)
            player.angularVelocity = 0
            vx, vy = 0, 0
            player:pause()  
        end)
        
        if bgMusic then 
            musicChannel = audio.play(bgMusic, { loops=-1, volume=0.7 }) 
        end
    end
end

function scene:hide(event)
    if event.phase == "will" then 
        stopGame() 
    end
end

function scene:destroy(event)
    audio.stop()
    local sounds = { coinSound, winSound, failSound, bgMusic }
    for i=1,#sounds do
        if sounds[i] then audio.dispose(sounds[i]); sounds[i] = nil end
    end
end

scene:addEventListener("create", scene)
scene:addEventListener("show", scene)
scene:addEventListener("hide", scene)
scene:addEventListener("destroy", scene)

return scene