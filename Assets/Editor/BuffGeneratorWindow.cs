using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Frag;
using System;

public class BuffGeneratorWindow : EditorWindow
{
    private string className = "NewBuff";
    private string classPath = @"/Scripts/Store/Data/Buff/BuffClass";
    private string assetPath = "/Scripts/Store/Data/Buff/BuffData";
    private MonoScript createdClassScript;

    [MenuItem("Window/Buff Generator")]
    public static void ShowWindow()
    {
        GetWindow<BuffGeneratorWindow>("Buff Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enter class name:");
        className = EditorGUILayout.TextField(className);

        GUILayout.Label("Enter classPath:");
        classPath = EditorGUILayout.TextField(classPath);

        GUILayout.Label("Enter assetPath:");
        assetPath = EditorGUILayout.TextField(assetPath);

        GUILayout.Label("Scripts:");
        createdClassScript = (MonoScript)EditorGUILayout.ObjectField("Class Script", createdClassScript, typeof(MonoScript), false);

        if (GUILayout.Button("Generate Buff Class"))
        {
            GenerateNewClassFile();
        }

        if (GUILayout.Button("Create New Buff"))
        {
            CreateNewBuff();
        }
    }

    private void GenerateNewClassFile()
    {
        MonoMgr.GetInstance().StartCoroutine(LoadClass());
    }

    private IEnumerator LoadClass()
    {
        // 获取默认的保存目录和文件名
        string defaultDirectory = Application.dataPath + classPath;
        string defaultFileName = className + ".cs";

        // 拼接文件完整路径
        string filePath = Path.Combine(defaultDirectory, defaultFileName);

        // 生成类内容
        string classContent = GenerateClassContent(className);

        // 将内容写入文件
        File.WriteAllText(filePath, classContent);

        // 刷新 Unity 资源
        AssetDatabase.Refresh();

        // 在 Unity 编辑器中打开新生成的类文件
        createdClassScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        // AssetDatabase.OpenAsset(createdClassScript);
        yield return null;
    }

    private string GenerateClassContent(string className)
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

    private void CreateNewBuff()
    {

        string defaultDirectory = "Assets" + assetPath;

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
        BuffModel newBuff = (BuffModel)Activator.CreateInstance(buffType);
        if (newBuff == null)
        {
            Debug.LogError("Failed to create instance of class: " + buffType.Name);
            return;
        }

        AssetDatabase.CreateAsset(newBuff, filePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newBuff;

    }
}
