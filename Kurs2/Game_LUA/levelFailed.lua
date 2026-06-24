local composer = require("composer")
local scene = composer.newScene()

function scene:create(event)
    local sceneGroup = self.view
    local params = event.params or {}
    local parentName = params.parentScene or "level3"

    local bg = display.newRect(sceneGroup, display.contentCenterX, display.contentCenterY, display.contentWidth, display.contentHeight)
    bg:setFillColor(0,0,0,0.7)
    bg.isHitTestable = true

    local panel = display.newRoundedRect(sceneGroup, display.contentCenterX, display.contentCenterY, 280, 200, 20)
    panel:setFillColor(0.2,0,0)

    local title = display.newText(sceneGroup, "ПОРАЖЕНИЕ", display.contentCenterX, display.contentCenterY - 40, native.systemFontBold, 32)

    local replayBtn = display.newRoundedRect(sceneGroup, display.contentCenterX, display.contentCenterY + 40, 220, 60, 12)
    replayBtn:setFillColor(0.6,0,0)
    local replayText = display.newText(sceneGroup, "Повторить попытку", replayBtn.x, replayBtn.y, native.systemFont, 20)

    local function onReplay()
        composer.hideOverlay("fade", 200)
        timer.performWithDelay(250, function()
            if composer.getScene(parentName) then
                composer.removeScene(parentName, true)
            end
            composer.gotoScene(parentName, {effect="fade", time=400})
        end)
    end

    replayBtn:addEventListener("tap", onReplay)
    replayText:addEventListener("tap", onReplay)
end

scene:addEventListener("create", scene)
return scene