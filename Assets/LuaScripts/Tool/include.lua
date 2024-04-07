require("Tool/Object")
require("Tool/splitTool")
Json = require("Tool/JsonTool")

local cs = CS

-- c
local u = cs.UnityEngine
GameObject = u.GameObject
Resources = u.Resources
Transform = u.Transform
RectTransform = u.RectTransform
SpriteAtlas = u.U2D.SpriteAtlas
TextAsset = u.TextAsset

--vec
Vector3 = u.Vector3
Vector2 = u.Vector2

-- ui
local UI = u.UI
Image = UI.Image
Text = UI.Text
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect

UIBehaviour = u.EventSystems.UIBehaviour
ResMgr = cs
ABMgr = cs.ABMgr.GetInstance()
