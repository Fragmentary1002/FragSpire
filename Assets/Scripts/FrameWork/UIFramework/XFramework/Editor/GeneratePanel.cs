using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

#if UNITY_EDITOR
public class GeneratePanel : Editor
{
    [MenuItem("Assets/Create/CreatePanelCS", false, 1)]
    private static void CreatePanelCS()
    {
        var objs = Selection.gameObjects;
        var selected = Selection.assetGUIDs;
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = objs[i];
            string name = obj.name;
            string filePath = $"Assets/Scripts/UI/Panel/{name}.cs";
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            if (objPath.Contains(".prefab"))
                objPath = objPath.Replace(".prefab", "");
            if (objPath.Contains("Assets/Resources/"))
                objPath = objPath.Replace("Assets/Resources/", "");

            // Debug.Log($"filePath = {filePath}, objPath = {objPath}");
            if (!File.Exists(filePath))
            {
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

/// <summary>
/// 
/// </summary>
public class {name} : BasePanel
{{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = ""{objPath}"";

    /// <summary>
    /// 
    /// </summary>
    public {name}() : base(new UIType(path))
    {{
        
    }}

    protected override void InitEvent()
    {{
        // ActivePanel.GetOrAddComponentInChildren<Button>(""BtnExit"").onClick.AddListener(() =>
        // {{
        //            Pop();
        // }});
    }}

    public override void OnStart()
    {{
        base.OnStart();
    }}

    public override void OnChange(BasePanel newPanel)
    {{
        // {name} panel = newPanel as {name};
    }}
}}
";
                #endregion
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }

    [MenuItem("Assets/Create/GenerateSceneCS", false, 2)]
    private static void GenerateSceneCS()
    {
        var objs = Selection.gameObjects;
        var selected = Selection.assetGUIDs;
        for (int i = 0; i < selected.Length; i++)
        {
            string objPath = AssetDatabase.GUIDToAssetPath(selected[i]);

            if (!objPath.EndsWith(".unity"))
                continue;

            objPath = objPath.Replace(".unity", "");
            int index = objPath.LastIndexOf('/');
            string name = objPath;
            if (index > 0)
                name = objPath.Substring(index + 1);

            string filePath = $"Assets/Scripts/Scene/{name}Scene.cs";

            if (!File.Exists(filePath))
            {
                string content =
                #region cs code
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

public class {name}Scene : SceneState
{{
    /// <summary>
    /// 
    /// </summary>
    public {name}Scene()
    {{
        sceneName = ""{name}"";
    }}

    public override void OnEnter()
    {{
        // panelManager.Push(new {name}Panel());
    }}
}}
";
                #endregion
                File.WriteAllText(filePath, content);
                Debug.Log($"生成完毕\n路径为{filePath}");
                AssetDatabase.Refresh();
            }
            else
                Debug.Log("已存在该文件");
        }
    }
}
#endif
