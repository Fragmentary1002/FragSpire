Object = {}

function Object:new()
    local obj = {}
    self.__index = self
    setmetatable(obj, self)
    return obj
end

function Object:subClass(className)
    _G[className] = {}
    local obj = _G[className]

    obj.base = self

    self.__index = self

    setmetatable(obj, self)

    return obj
end
