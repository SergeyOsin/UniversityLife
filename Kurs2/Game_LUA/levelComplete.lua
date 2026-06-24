local composer = require("composer")
local scene = composer.newScene()

function scene:create(event)
    local sceneGroup = self.view
    local params = event.params or {}
    local currentLevel = params.parentScene or "level3"
    local nextLevel = params.nextLevel or "level4"

    local background = display.newRect(sceneGroup, display.contentCenterX, display.contentCenterY, display.contentWidth, display.contentHeight)
    background:setFillColor(0, 0, 0, 0.8)
    background.isHitTestable = true  
    background:addEventListener("touch", function() return true end) 
    background:addEventListener("tap", function() return true end)

    local panel = display.newRoundedRect(sceneGroup, display.contentCenterX, display.contentCenterY, 300, 250, 20)
    panel:setFillColor(0.1, 0.1, 0.2)
    panel.strokeWidth = 4
    panel:setStrokeColor(0, 0.8, 0)

    local title = display.newText({
        parent = sceneGroup,
        text = "Уровень пройден!",
        x = display.contentCenterX,
        y = display.contentCenterY - 70,
        font = native.systemFontBold,
        fontSize = 24
    })
    title:setFillColor(0, 1, 0)

    local function navigateTo(targetScene)
    composer.hideOverlay("fade", 300)
    composer.removeScene(currentLevel)
    composer.gotoScene(targetScene, {effect="fade", time=400})
end

    local replayBtn = display.newRoundedRect(sceneGroup, display.contentCenterX, display.contentCenterY + 10, 220, 50, 12)
    replayBtn:setFillColor(0.3, 0.3, 0.3)
    local replayText = display.newText(sceneGroup, "Повторить", replayBtn.x, replayBtn.y, native.systemFont, 20)
    replayBtn:addEventListener("tap", function() navigateTo(currentLevel) end)

    local nextBtn = display.newRoundedRect(sceneGroup, display.contentCenterX, display.contentCenterY + 80, 220, 50, 12)
    nextBtn:setFillColor(0, 0.6, 0)
    local nextText = display.newText(sceneGroup, "Следующий уровень", nextBtn.x, nextBtn.y, native.systemFont, 20)
    nextBtn:addEventListener("tap", function() navigateTo(nextLevel) end)
end

scene:addEventListener("create", scene)
return scene