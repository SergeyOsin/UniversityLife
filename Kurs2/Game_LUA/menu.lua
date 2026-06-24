local composer = require( "composer" )
local scene = composer.newScene()
local widget = require "widget"
local buttons = {}
local backgroundMusic

local function onLevelBtnRelease(event)
    local btn = event.target
    transition.to(btn, { time=100, xScale=0.9, yScale=0.9, transition=easing.outQuad, onComplete=function()
        transition.to(btn, { time=100, xScale=1, yScale=1, transition=easing.outQuad })
    end })
    composer.removeScene("level"..(6-btn.levelNum))
    composer.gotoScene("level"..(6-btn.levelNum), { time=500, effect="fade" }) 
    return true
end

function scene:create( event )
    local sceneGroup = self.view
    
    local background = display.newImageRect( sceneGroup, "pictures/game_background.png", display.actualContentWidth, display.actualContentHeight )
    background.x = display.contentCenterX
    background.y = display.contentCenterY
    
    local btnWidth, btnHeight = 220, 55 
    local buttonSpacing = 35 
    local startY = display.contentHeight - 120 

    for i=1,5 do
        local btn = widget.newButton{
            label = "УРОВЕНЬ "..(6-i), 
            labelColor = { default={0, 0.6, 0}, over={0, 0.4, 0} },
            font = native.systemFontBold,
            fontSize = 20,
            shape = "roundedRect",
            cornerRadius = btnHeight / 2, 
            width = btnWidth, 
            height = btnHeight,
            fillColor = { default={1, 1, 1, 0.85}, over={0.9, 1, 0.9, 1} }, 
            strokeColor = { default={0, 0.8, 0}, over={0, 0.5, 0} },
            strokeWidth = 4,
            onRelease = onLevelBtnRelease
        }
        btn.levelNum = i  
        btn.x = display.contentCenterX
        btn.y = startY - (btnHeight + buttonSpacing) * (i-1)
        
        sceneGroup:insert(btn)
        buttons[i] = btn
    end
end

function scene:show( event )
    if event.phase == "did" then
        backgroundMusic = audio.loadSound( "sounds/back_music.mp3" )
        if backgroundMusic then
            audio.play( backgroundMusic, { channel=1, loops=-1, fadein=250 } )
        end
    end    
end

function scene:hide( event )
    if event.phase == "will" then
        audio.fadeOut( { channel=1, time=500 } )
    end    
end

function scene:destroy( event )
    if backgroundMusic then
        audio.stop( 1 ) 
        audio.dispose( backgroundMusic )
        backgroundMusic = nil
    end
    buttons = {}
end

scene:addEventListener( "create", scene )
scene:addEventListener( "show", scene )
scene:addEventListener( "hide", scene )
scene:addEventListener( "destroy", scene )

return scene