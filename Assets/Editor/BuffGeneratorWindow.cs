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
        // ��ȡĬ�ϵı���Ŀ¼���ļ���
        string defaultDirectory = Application.dataPath + classPath;
        string defaultFileName = className + ".cs";

        // ƴ���ļ�����·��
        string filePath = Path.Combine(defaultDirectory, defaultFileName);

        // ����������
        string classContent = GenerateClassContent(className);

        // ������д���ļ�
        File.WriteAllText(filePath, classContent);

        // ˢ�� Unity ��Դ
        AssetDatabase.Refresh();

        // �� Unity �༭���д������ɵ����ļ�
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
       //�����ص���

        //����ص���
        public override void OnCreate(BuffInfo buff) { }

        //�Ƴ��ص���
        public override void OnRemoved(BuffInfo buff) { }

    }
}";

        return classContent;
    }

    private void CreateNewBuff()
    {

        string defaultDirectory = "Assets" + assetPath;

        string defaultFileName = className + ".asset";

        // ������Ŀ¼
        Directory.CreateDirectory(Path.GetDirectoryName(defaultDirectory));


        // ƴ���ļ�����·��
        string filePath = Path.Combine(defaultDirectory, defaultFileName);




        Type buffType = createdClassScript.GetClass();
        if (buffType == null)
        {
            Debug.LogError("Failed to get class type from script: " + createdClassScript.name);
            return;
        }

        // ������ʵ��
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
