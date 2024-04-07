using UnityEngine;
using System.IO;
using XLua;


/// <summary>
/// lua解析器管理器，集成对lua的操作
/// </summary>
public class LuaMgr : BaseManager<LuaMgr>
{
    private LuaEnv luaEnv;

    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }
    
    public void Init()
    {
        if (luaEnv != null)
        {
            return;
        }

        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
            
        //luaEnv.AddLoader(MyCustomABLoader);
    }

    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require('{0}')", fileName);
        DoString(str);
    }
    //自动执行
    private byte[] MyLoader(ref string filePath )
    {
        string path = Application.dataPath + "/LuaScripts/Lua" + filePath +".lua";
      

        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("文件重定向失败，文件名："+filePath);
        }
        
        
        return null;
    }

    private byte[] MyCustomABLoader(ref string filePath)
    {
       TextAsset lua= ABMgr.GetInstance().LoadRes<TextAsset>("lua", filePath + ".lua");
       if (lua != null)
       {
           return lua.bytes;
       }
       else
       {
           Debug.Log("文件重定向失败，文件名："+filePath);
           return null;
       }

    }
    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("解析器未初始化");
            return;
        }
        luaEnv.DoString(str);
    }

    public void Tick()
    {
        luaEnv.Tick();
    }

    public void Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
    }
}
