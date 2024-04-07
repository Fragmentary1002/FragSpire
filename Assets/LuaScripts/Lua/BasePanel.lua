BasePanel = Object:subClass("BasePanel")

BasePanel.panelObj = nil

BasePanel.controls = {}

function BasePanel:OnInit(name)
    if self.panelObj ~= nil then
        return
    end

    self.panelObj = ABMgr:LoadRes("ui", name, typeof(GameObject))
    self.panelObj.transform:SetParent(Canvas, false)

    local allControls = self.panelObj:s(typeof(UIBehaviour))


    for i = 0, allControls.Length - 1 do
        local data = allControls[i]
        local dataName = data.name

        if string.find(dataName, "btn") ~= nil or
            string.find(dataName, "tog") ~= nil or
            string.find(dataName, "img") ~= nil or
            string.find(dataName, "sv") ~= nil or
            string.find(dataName, "txt") ~= nil then
            local typeName = data:GetType().Name

            if self.controls[dataName] ~= nil then
                self.controls[dataName][typeName] = data
            else
                self.controls[dataName] = { [typeName] = data }
            end
        end
    end
end

function BasePanel:ShowMe(name)
    self:OnInit(name)
    self.panelObj:SetActive(true)
end

function BasePanel:HideMe()
    -- body
    self.panelObj:SetActive(false)
end

--
