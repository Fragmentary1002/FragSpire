using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class RunLua : MonoBehaviour
{
    private void Start()
    {
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoLuaFile("Main");
    }
}