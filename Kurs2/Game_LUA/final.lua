local composer = require("composer")
local scene = composer.newScene()

function scene:create(event)
    local sceneGroup = self.view

    local background = display.newRect(sceneGroup, display.contentCenterX, display.contentCenterY,
                                       display.contentWidth, display.contentHeight)
    background:setFillColor(1, 1, 1, 0.9)  
    background.isHitTestable = true          

    local message = "Поздравляем, вы прошли игру.\nТеперь вы увидели, как живет активист ФВТ.\nНажмите, чтобы выйти в меню."
    local text = display.newText({
        parent = sceneGroup,
        text = message,
        x = display.contentCenterX,
        y = display.contentCenterY,
        width = display.contentWidth - 40,
        font = native.systemFont,
        fontSize = 22,
        align = "center"
    })
    text:setFillColor(0, 0, 0)

    local function closeWindow()
        composer.hideOverlay("fade", 300)
        timer.performWithDelay(300, function()
            composer.gotoScene("menu", { effect = "fade", time = 400 })
        end)
        return true 
    end

    background:addEventListener("tap", closeWindow)
    text:addEventListener("tap", closeWindow)
end

scene:addEventListener("create", scene)
return scene