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
                Debug.LogError("storeEnum û������");
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
                Debug.LogError("storeEnum û������");
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
        // ��ȡĬ�ϵı���Ŀ¼���ļ���
        string defaultDirectory = Application.dataPath + path + classPath;
        string defaultFileName = className + ".cs";

        // ƴ���ļ�����·��
        string filePath = Path.Combine(defaultDirectory, defaultFileName);



        // ������д���ļ�
        File.WriteAllText(filePath, classContent);

        // ˢ�� Unity ��Դ
        AssetDatabase.Refresh();

        // �� Unity �༭���д������ɵ����ļ�
        createdClassScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        // AssetDatabase.OpenAsset(createdClassScript);
    }


    private void CreateNewObj<T>(string assetPath) where T : UnityEngine.ScriptableObject
    {

        string defaultDirectory = "Assets" + path + assetPath;

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
       //�����ص���

        //����ص���
        public override void OnCreate(BuffInfo buff) { }

        //�Ƴ��ص���
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
