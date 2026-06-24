local composer = require("composer")
local scene = composer.newScene()
local physics = require("physics")

-- Глобальные переменные сцены
local player
local startPlatform
local startPlatformY
local finishBlock
local platforms = {}
local platformsPassed = 0
local totalPlatforms = 0
local gameActive = false
local playerGrounded = true
local horizontalInput = 0
local nextDirection = 1
local moveSpeed = 300
local sceneGroup
local scoreText
local menuBtn
local spawning = false
local spawnTimer 

local bgMusic, jumpSound, winSound, failSound
local musicChannel = 0
local MUSIC_CHANNEL = 1

local handleTouch, onCollision, enterFrameUpdate

local SCALE_X = 0.14  
local SCALE_Y = 0.14
local FRAME_W = 800   
local FRAME_H = 900  

local function stopLevel()
    if not gameActive then return end
    gameActive = false
    spawning = true 
    horizontalInput = 0
    
    if spawnTimer then
        timer.cancel(spawnTimer)
        spawnTimer = nil
    end

    Runtime:removeEventListener("touch", handleTouch)
    Runtime:removeEventListener("collision", onCollision)
    Runtime:removeEventListener("enterFrame", enterFrameUpdate)
    
    physics.pause()
end

local function goToMenu()
    if musicChannel and musicChannel > 0 then
        audio.stop(musicChannel)
    end
    musicChannel = 0
    stopLevel()
    composer.gotoScene("menu", { effect="fade", time=400 })
end

local function cleanupPlatforms()
    if spawnTimer then timer.cancel(spawnTimer); spawnTimer = nil end
    if platforms then
        for i = #platforms, 1, -1 do
            if platforms[i] then
                display.remove(platforms[i])
                platforms[i] = nil
            end
        end
    end
    platforms = {}
    if finishBlock then
        display.remove(finishBlock)
        finishBlock = nil
    end
end

local function spawnFinish(prevPlatform)
    if not gameActive or not prevPlatform or not prevPlatform.y then return end
    local finish = display.newImageRect(sceneGroup, "pictures/doorTo506.png", 160, 130)
    finish.x = display.contentCenterX
    finish.y = prevPlatform.y - 130
    finish.myName = "finishLvl2"
    physics.addBody(finish, "static", { isSensor=true })
    finishBlock = finish
    table.insert(platforms, finish)
end

local function spawnPlatform(prevPlatform)
    if not gameActive or not prevPlatform or not prevPlatform.y then return end
    if platformsPassed >= totalPlatforms then return end
    
    local width, height = 130, 30
    local gapX = math.random(110, 160)
    local gapY = math.random(-150, -130)
    local x = prevPlatform.x + (gapX * nextDirection)
    nextDirection = nextDirection * -1
    
    if x < 70 then x = 70; nextDirection = 1 end
    if x > display.contentWidth - 70 then x = display.contentWidth - 70; nextDirection = -1 end
    
    local y = prevPlatform.y + gapY
    local platform = display.newImageRect(sceneGroup, "pictures/platform.png", width, height)
    platform.x, platform.y = x, y
    platform.myName = "platform" 
    platform.passed = false
    physics.addBody(platform, "static", { friction=0, bounce=0 })
    table.insert(platforms, platform)
end

handleTouch = function(event)
    if not gameActive or not player then return true end
    if event.phase == "began" or event.phase == "moved" then
        horizontalInput = (event.x < display.contentCenterX) and -1 or 1
        if event.phase == "began" and playerGrounded then
            local vx, vy = player:getLinearVelocity()
            player:setLinearVelocity(vx, -750)
            playerGrounded = false
            player:setSequence("jump")
            player:play()
            if jumpSound then audio.play(jumpSound) end
        end
    elseif event.phase == "ended" or event.phase == "cancelled" then
        horizontalInput = 0
    end
    
    if player and player.xScale then
        if horizontalInput < 0 then player.xScale = -SCALE_X 
        elseif horizontalInput > 0 then player.xScale = SCALE_X end
    end
    return true
end

onCollision = function(event)
    if not gameActive or event.phase ~= "began" or not player then return end
    local a, b = event.object1, event.object2
    local other = (a.myName == "player") and b or a

    if not other or not other.myName then return end

    if other.myName == "finishLvl2" then
        stopLevel()
        if winSound then audio.play(winSound) end
        composer.showOverlay("levelComplete", {
            isModal = true, params = { score=1000, parentScene="level2", nextLevel="level3" }
        })
        return
    end

    -- ИСПРАВЛЕНИЕ: Добавлена проверка startPlatform
    if other.myName == "platform" or other.myName == "startPlatform" then
        local vx, vy = player:getLinearVelocity()
        -- Если игрок падает сверху
        if vy >= 0 and player.y < other.y - 10 then 
            playerGrounded = true
            player:setSequence("idle")
            player:pause()
            
            -- Спавним новую платформу только если это обычная платформа и она еще не пройдена
            if other.myName == "platform" and not other.passed and not spawning then
                other.passed = true
                spawning = true
                platformsPassed = platformsPassed + 1
                scoreText.text = "Платформ: " .. platformsPassed .. " / " .. totalPlatforms
                
                spawnTimer = timer.performWithDelay(50, function()
                    if gameActive then
                        spawning = false
                        if platformsPassed < totalPlatforms then 
                            spawnPlatform(other)
                        else 
                            spawnFinish(other) 
                        end
                    end
                end)
            end
        end
    end
end

enterFrameUpdate = function()
    if not gameActive or not player or not player.getLinearVelocity then return end

    local vx, vy = player:getLinearVelocity()
    player:setLinearVelocity(horizontalInput * moveSpeed, vy)

    -- ИСПРАВЛЕНИЕ: Ограничение по краям экрана (чтобы не застревал в "стенах")
    if player.x < 25 then player.x = 25 
    elseif player.x > display.contentWidth - 25 then player.x = display.contentWidth - 25 end

    -- Движение камеры
    if player.y < display.contentCenterY then
        local diff = display.contentCenterY - player.y
        player.y = display.contentCenterY
        if startPlatform then startPlatform.y = startPlatform.y + diff end
        if finishBlock then finishBlock.y = finishBlock.y + diff end
        for i = 1, #platforms do
            if platforms[i] and platforms[i].y then
                platforms[i].y = platforms[i].y + diff
            end
        end
    end

    if player.y > display.contentHeight + 100 then
        stopLevel()
        if failSound then audio.play(failSound) end
        composer.showOverlay("levelFailed", {
            isModal = true, params = { parentScene = "level2" }
        })
    end
end

function scene:create(event)
    sceneGroup = self.view
    physics.start()
    physics.setGravity(0, 45)
    physics.pause()

    bgMusic = audio.loadStream("sounds/level2_music.mp3")
    jumpSound = audio.loadSound("sounds/jump.mp3")
    winSound = audio.loadSound("sounds/win.mp3")
    failSound = audio.loadSound("sounds/fail.mp3")

    local bg = display.newImageRect(sceneGroup, "pictures/background_level2.png", display.contentWidth, display.contentHeight)
    bg.x, bg.y = display.contentCenterX, display.contentCenterY

    menuBtn = display.newText(sceneGroup, "МЕНЮ", display.contentCenterX, 40, native.systemFontBold, 28)
    menuBtn:setFillColor(0); menuBtn:addEventListener("tap", goToMenu)
    scoreText = display.newText(sceneGroup, "", display.contentCenterX, 80, native.systemFontBold, 28)
    scoreText:setFillColor(0)

    startPlatformY = display.contentHeight - 80
    startPlatform = display.newImageRect(sceneGroup, "pictures/platform.png", 130, 30)
    startPlatform.x, startPlatform.y = display.contentCenterX, startPlatformY
    startPlatform.myName = "startPlatform" 
    physics.addBody(startPlatform, "static", { friction=0, bounce=0 })

    local sheetOptions = {
        frames = {
            { x=0, y=0, width=FRAME_W, height=FRAME_H },
            { x=FRAME_W, y=0, width=FRAME_W, height=FRAME_H },
        }
    }

    local jumpSheet = graphics.newImageSheet("pictures/stud_jump.png", sheetOptions)
    local idleSheet = graphics.newImageSheet("pictures/stud.png", sheetOptions)

    local seqData = {
        { name="idle", sheet=idleSheet, start=1, count=1, time=0 },
        { name="jump", sheet=jumpSheet, start=1, count=2, time=3500, loopCount=0 }, -- Ускорил анимацию прыжка
    }

    player = display.newSprite(sceneGroup, idleSheet, seqData)
    player.xScale, player.yScale = SCALE_X, SCALE_Y
    player.anchorX, player.anchorY = 0.5, 0.5
    player.myName = "player"
    
    local playerShape = { -20,-40, 20,-40, 20,45, -20,45 }
    physics.addBody(player, "dynamic", { density=2.0, friction=0, bounce=0, shape=playerShape })
    player.isFixedRotation = true
end

function scene:show(event)
    if event.phase == "will" then
        cleanupPlatforms()
        
        gameActive = true
        spawning = false 
        playerGrounded = true
        platformsPassed = 0
        horizontalInput = 0
        nextDirection = math.random() > 0.5 and 1 or -1
        finishBlock = nil
        
        startPlatform.x, startPlatform.y = display.contentCenterX, startPlatformY
        
        player.x = startPlatform.x
        player.y = startPlatformY - 80
        player:setLinearVelocity(0, 0)
        player:setSequence("idle")
        player:pause()
        
        totalPlatforms = math.random(5, 8);
        scoreText.text = "Платформ: 0 / " .. totalPlatforms
        
    elseif event.phase == "did" then
        physics.start()
        if musicChannel == 0 or not audio.isChannelPlaying(musicChannel) then 
            musicChannel = audio.play(bgMusic, { loops=-1, channel=MUSIC_CHANNEL, volume=0.6 }) 
        end
        
        Runtime:addEventListener("touch", handleTouch)
        Runtime:addEventListener("collision", onCollision)
        Runtime:addEventListener("enterFrame", enterFrameUpdate)
        
        if #platforms == 0 then
                spawnPlatform(startPlatform)
        end
    end
end

function scene:hide(event) if event.phase == "will" then stopLevel() end end
function scene:destroy(event) audio.stop() end

scene:addEventListener("create", scene); scene:addEventListener("show", scene)
scene:addEventListener("hide", scene); scene:addEventListener("destroy", scene)
return scene