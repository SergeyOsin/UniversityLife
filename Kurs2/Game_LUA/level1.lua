local composer = require("composer")
local scene = composer.newScene()
local physics = require("physics")

-- Глобальные переменные сцены
local playerBody, playerSprite
local scoreText, menuBtn
local wallTimer, floorTimer
local gameActive = false
local walls = {}
local floorSegments = {}
local wallsJumped = 0
local wallsCreated = 0
local totalWalls = 10
local finishSpawned = false
local playerGrounded = false

local musicTrack, jumpSound, failSound, winSound
local musicChannel

local SCALE_X = 0.14  
local SCALE_Y = 0.14
local FRAME_W = 800  
local FRAME_H = 850   

local PLAYER_WIDTH  = 90 
local PLAYER_HEIGHT = 110 
local SPEED         = -230
local JUMP_FORCE    = -340 

local bgGroup, worldGroup, playerGroup, uiGroup

local jump, onCollision, enterFrameUpdate, spawnWall, spawnFloor

local function setAnimation(state)
    if not playerSprite then return end
    if state == "jump" and playerSprite.sequence ~= "jump" then
        playerSprite:setSequence("jump")
        playerSprite:play()
    elseif state == "run" and playerSprite.sequence ~= "run" then
        playerSprite:setSequence("run")
        playerSprite:play()
    end
end

-- =======================
-- Остановка логики уровня
-- =======================
local function stopAllLevelLogic()
    gameActive = false
    physics.pause()

    -- Музыка здесь НЕ останавливается (чтобы продолжать играть при рестарте)

    if wallTimer then 
        timer.cancel(wallTimer)
        wallTimer = nil 
    end
    if floorTimer then 
        timer.cancel(floorTimer)
        floorTimer = nil 
    end

    Runtime:removeEventListener("touch", jump)
    Runtime:removeEventListener("collision", onCollision)
    Runtime:removeEventListener("enterFrame", enterFrameUpdate)
end

-- =======================
-- Очистка объектов
-- =======================
local function cleanupObjects()
    if walls then
        for i = #walls, 1, -1 do display.remove(walls[i]); walls[i] = nil end
        walls = {}
    end
    if floorSegments then
        for i = #floorSegments, 1, -1 do display.remove(floorSegments[i]); floorSegments[i] = nil end
        floorSegments = {}
    end
end

local function goToMenu()
    -- Останавливаем музыку только при выходе в меню
    if musicChannel then
        audio.stop(musicChannel)
        musicChannel = nil
    end
    stopAllLevelLogic()
    composer.gotoScene("menu", { effect="fade", time=400 })
end

local function spawnFinish()
    if not gameActive or finishSpawned then return end
    finishSpawned = true
    local finish = display.newImageRect(worldGroup, "pictures/7korpus.png", 200, 250)
    finish.x = display.contentWidth + 300
    finish.y = display.contentHeight - 40 - finish.height / 2
    physics.addBody(finish, "kinematic", { isSensor=true })
    finish.myName = "finish"
    finish:setLinearVelocity(SPEED, 0)
    table.insert(walls, finish)
end

spawnFloor = function()
    if not gameActive then return end
    local floor = display.newImageRect(worldGroup, "pictures/road.png", 102, 40)
    floor.x = display.contentWidth + 100
    floor.y = display.contentHeight - 20
    floor.myName = "ground"
    physics.addBody(floor, "kinematic", { friction=0.5, bounce=0 })
    floor:setLinearVelocity(SPEED, 0)
    table.insert(floorSegments, floor)
end

spawnWall = function()
    if not gameActive then return end

    -- Если все стены созданы
    if wallsCreated >= totalWalls then
        if not finishSpawned then 
            timer.performWithDelay(250, spawnFinish) 
        end
        return
    end

    wallsCreated = wallsCreated + 1
    local wallHeight = math.random(60, 95)
    local wall = display.newImageRect(worldGroup, "pictures/walls.png", 45, wallHeight)
    wall.x = display.contentWidth + 150
    wall.y = display.contentHeight - 40 - wallHeight / 2
    physics.addBody(wall, "kinematic", { isSensor=true })
    wall.myName = "wall"
    wall.passed = false
    wall:setLinearVelocity(SPEED, 0)
    table.insert(walls, wall)

    if gameActive then
        wallTimer = timer.performWithDelay(math.random(1500,2400), spawnWall)
    end
end

jump = function(event)
    if event.phase == "began" and gameActive and playerGrounded then
        playerBody:setLinearVelocity(0, JUMP_FORCE)
        playerGrounded = false
        setAnimation("jump")
        if jumpSound then audio.play(jumpSound) end
    end
    return true
end

onCollision = function(event)
    if not gameActive or event.phase ~= "began" then return end
    local a, b = event.object1, event.object2

    if (a.myName == "player" and b.myName == "ground") or (b.myName == "player" and a.myName == "ground") then
        playerGrounded = true
        setAnimation("run")
    elseif (a.myName == "player" and b.myName == "wall") or (b.myName == "player" and a.myName == "wall") then
        stopAllLevelLogic()
        if failSound then audio.play(failSound) end
        composer.showOverlay("levelFailed", { isModal=true, effect="fade", params={score=wallsJumped, parentScene="level1"} })
    elseif (a.myName == "player" and b.myName == "finish") or (b.myName == "player" and a.myName == "finish") then
        stopAllLevelLogic()
        if winSound then audio.play(winSound) end
        composer.showOverlay("levelComplete", { isModal=true, effect="fade", params={parentScene="level1", nextLevel="level2"} })
    end
end

enterFrameUpdate = function()
    if not gameActive or not playerBody or not playerSprite then return end
    playerBody.x = 80
    playerSprite.x = playerBody.x
    playerSprite.y = playerBody.y 

    for i = #walls, 1, -1 do
        local w = walls[i]
        if w.x and w.myName == "wall" and not w.passed and w.x < playerBody.x then
            w.passed = true
            wallsJumped = wallsJumped + 1
            scoreText.text = "Счёт: " .. wallsJumped .. " / " .. totalWalls
        end
        if w.x and w.x < -250 then display.remove(w); table.remove(walls, i) end
    end
    for i = #floorSegments, 1, -1 do
        local f = floorSegments[i]
        if f.x and f.x < -200 then display.remove(f); table.remove(floorSegments, i) end
    end
end

function scene:create(event)
    local sceneGroup = self.view
    physics.start()
    physics.setGravity(0, 14)
    physics.pause()

    bgGroup = display.newGroup(); sceneGroup:insert(bgGroup)
    worldGroup = display.newGroup(); sceneGroup:insert(worldGroup)
    playerGroup = display.newGroup(); sceneGroup:insert(playerGroup)
    uiGroup = display.newGroup(); sceneGroup:insert(uiGroup)

    jumpSound  = audio.loadSound("sounds/jump.mp3")
    failSound  = audio.loadSound("sounds/fail.mp3")
    winSound   = audio.loadSound("sounds/win.mp3")
    musicTrack = audio.loadStream("sounds/level1_music.mp3")

    local bg = display.newImageRect(bgGroup, "pictures/back_1lvl.jpg", display.contentWidth, display.contentHeight)
    bg.x, bg.y = display.contentCenterX, display.contentCenterY

    local sheetOptions = {
        frames = {
            { x=0, y=0, width=FRAME_W, height=FRAME_H },      
            { x=0, y=0, width=FRAME_W, height=FRAME_H }, 
        }
    }

    local runSheet  = graphics.newImageSheet("pictures/stud_run.png", sheetOptions)
    local jumpSheet = graphics.newImageSheet("pictures/stud_jump.png", sheetOptions)
    local seqData = {
        { name="run",  sheet=runSheet,  start=1, count=2, time=300, loopCount=0},
        { name="jump", sheet=jumpSheet, start=1, count=2, time=300, loopCount=1 },
    }

    playerSprite = display.newSprite(playerGroup, runSheet, seqData)
    playerSprite.xScale = PLAYER_WIDTH / 450
    playerSprite.yScale = PLAYER_HEIGHT / 900
    playerSprite.anchorY = 0.5
    playerSprite:play()

    playerBody = display.newRect(playerGroup, 80, display.contentHeight - 100, PLAYER_WIDTH, PLAYER_HEIGHT)
    playerBody.isVisible = false 
    physics.addBody(playerBody, "dynamic", { density=2.0, friction=0, bounce=0 })
    playerBody.isFixedRotation = true
    playerBody.myName = "player"

    menuBtn = display.newText(uiGroup, "МЕНЮ", 60, 40, native.systemFontBold, 22)
    menuBtn:addEventListener("tap", goToMenu)
    scoreText = display.newText(uiGroup, "", display.contentCenterX, 40, native.systemFont, 24)
end

function scene:show(event)
    if event.phase == "will" then
        -- Чистим всё старое перед запуском
        stopAllLevelLogic()
        cleanupObjects()

        wallsJumped, wallsCreated, finishSpawned = 0, 0, false
        gameActive, playerGrounded = true, false
        totalWalls = math.random(5,7)
        scoreText.text = "Счёт: 0 / " .. totalWalls

        playerBody.x = 80
        playerBody.y = display.contentHeight - 150
        playerBody:setLinearVelocity(0, 0)
        playerSprite.x, playerSprite.y = playerBody.x, playerBody.y
        setAnimation("run")

    elseif event.phase == "did" then
        -- Убеждаемся, что старый таймер удален
        if wallTimer then timer.cancel(wallTimer); wallTimer = nil end

        physics.start()
        
        -- Музыка продолжает играть, если уже запущена
        if musicTrack and (not musicChannel or not audio.isChannelPlaying(musicChannel)) then 
            musicChannel = audio.play(musicTrack, { loops=-1, channel=2 }) 
        end
        
        Runtime:addEventListener("touch", jump)
        Runtime:addEventListener("collision", onCollision)
        Runtime:addEventListener("enterFrame", enterFrameUpdate)

        for i = 1, 15 do
            local f = display.newImageRect(worldGroup, "pictures/road.png", 102, 40)
            f.x, f.y, f.myName = (i - 1) * 100, display.contentHeight - 20, "ground"
            physics.addBody(f, "kinematic", { friction=0.5, bounce=0 })
            f:setLinearVelocity(SPEED, 0)
            table.insert(floorSegments, f)
        end

        wallTimer  = timer.performWithDelay(1500, spawnWall)
        floorTimer = timer.performWithDelay(400, spawnFloor, 0)
    end
end

function scene:hide(event)
    if event.phase == "will" then 
    end
end

function scene:destroy(event)
    if musicChannel then audio.stop(musicChannel); musicChannel = nil end
    timer.performWithDelay(50, function()
        if musicTrack then audio.dispose(musicTrack); musicTrack = nil end
        if jumpSound then audio.dispose(jumpSound); jumpSound = nil end
        if failSound then audio.dispose(failSound); failSound = nil end
        if winSound then audio.dispose(winSound); winSound = nil end
    end)
end

scene:addEventListener("create",  scene)
scene:addEventListener("show",    scene)
scene:addEventListener("hide",    scene)
scene:addEventListener("destroy", scene)

return scene