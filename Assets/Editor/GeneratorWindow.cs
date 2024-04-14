using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Frag;
using System;
using System.ComponentModel;

public class GeneratorWindow : EditorWindow
{
    private string className = "NewData";

    private string path = "/Scripts/Store/Data/";




    private StoreEnum storeEnum = StoreEnum.None;
    private enum StoreEnum
    {
        Buff,
        Card,
        None
    }


    private MonoScript createdClassScript;

    [MenuItem("Window/Generator/Generator Window")]
    public static void ShowWindow()
    {
        GetWindow<GeneratorWindow>("Generator Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select enum:");
        storeEnum = (StoreEnum)EditorGUILayout.EnumPopup(storeEnum);

        GUILayout.Label("Enter class name:");
        className = EditorGUILayout.TextField(className);

        GUILayout.Label("Enter Path:");
        path = EditorGUILayout.TextField(path);

        //GUILayout.Label("Enter assetPath:");
        //assetPath = EditorGUILayout.TextField(assetPath);

        GUILayout.Label("Scripts:");
        createdClassScript = (MonoScript)EditorGUILayout.ObjectField("Class Script", createdClassScript, typeof(MonoScript), false);


        GUILayout.Label("Select Button:");
        if (GUILayout.Button("Generate New Class"))
        {
            OnClickNewClass();
        }

        if (GUILayout.Button("Create New Obj"))
        {

            OnClickNewObj();

        }
    }


    private void OnClickNewClass()
    {
        switch (storeEnum)
        {
            case StoreEnum.None:
                Debug.LogError("storeEnum 没有设置");
                break;
            case StoreEnum.Buff:
                GenerateNewClassFile(CreateBuffClass(this.className), GetPath(storeEnum, 1));
                break;
            case StoreEnum.Card:
                GenerateNewClassFile(CreateCardClass(this.className), GetPath(storeEnum, 1));
                break;

        }
    }


    private void OnClickNewObj()
    {
        switch (storeEnum)
        {
            case StoreEnum.None:
                Debug.LogError("storeEnum 没有设置");
                break;
            case StoreEnum.Buff:
                CreateNewObj<BuffModel>(GetPath(storeEnum, 1));
                break;
            case StoreEnum.Card:
                CreateNewObj<BaseCard>(GetPath(storeEnum, 2));
                break;

        }
    }




    private void GenerateNewClassFile(string classContent, string classPath)
    {
        // 获取默认的保存目录和文件名
        string defaultDirectory = Application.dataPath + path + classPath;
        string defaultFileName = className + ".cs";

        // 拼接文件完整路径
        string filePath = Path.Combine(defaultDirectory, defaultFileName);



        // 将内容写入文件
        File.WriteAllText(filePath, classContent);

        // 刷新 Unity 资源
        AssetDatabase.Refresh();

        // 在 Unity 编辑器中打开新生成的类文件
        createdClassScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        // AssetDatabase.OpenAsset(createdClassScript);
    }


    private void CreateNewObj<T>(string assetPath) where T : UnityEngine.ScriptableObject
    {

        string defaultDirectory = "Assets" + path + assetPath;

        string defaultFileName = className + ".asset";

        // 创建父目录
        Directory.CreateDirectory(Path.GetDirectoryName(defaultDirectory));


        // 拼接文件完整路径
        string filePath = Path.Combine(defaultDirectory, defaultFileName);


        Type buffType = createdClassScript.GetClass();

        if (buffType == null)
        {
            Debug.LogError("Failed to get class type from script: " + createdClassScript.name);
            return;
        }

        // 创建类实例
        T newObj = (T)Activator.CreateInstance(buffType);
        if (newObj == null)
        {
            Debug.LogError("Failed to create instance of class: " + buffType.Name);
            return;
        }

        AssetDatabase.CreateAsset(newObj, filePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newObj;

    }


    private string GetPath(StoreEnum storeEnum, int tag)
    {
        if (storeEnum == StoreEnum.None || tag == 0) { return ""; }

        switch (tag)
        {
            case 1:
                return $"{storeEnum}/{storeEnum}Class"; ;
            case 2:
                return $"{storeEnum}/{storeEnum}Data"; ;
        }
        return "";

    }



    /// <summary>
    /// buff class 
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    private string CreateBuffClass(string className)
    {
        string classContent = @"
using UnityEngine;

namespace Frag
{

    public class " + className + @" : BuffModel
    {
       //基础回调点

        //创造回调点
        public override void OnCreate(BuffInfo buff) { }

        //移除回调点
        public override void OnRemoved(BuffInfo buff) { }

    }
}";

        return classContent;
    }

    /// <summary>
    /// card class
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    private string CreateCardClass(string className)
    {
        string classContent = @"
using UnityEngine;

namespace Frag
{

    public class " + className + @" : BaseCard
    {
        public override void Apply(DamageInfo damageInfo) { }

        
    }
}";

        return classContent;
    }



}
