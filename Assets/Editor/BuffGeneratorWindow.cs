using UnityEngine;
using UnityEditor;
using System.IO;

public class BuffGeneratorWindow : EditorWindow
{
    private string className = "NewBuff";

    [MenuItem("Window/Buff Generator")]
    public static void ShowWindow()
    {
        GetWindow<BuffGeneratorWindow>("Buff Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Enter class name:");
        className = EditorGUILayout.TextField(className);

        if (GUILayout.Button("Generate Buff Class"))
        {
            GenerateNewClassFile();
        }
    }

    private void GenerateNewClassFile()
    {
        string defaultDirectory = Application.dataPath;
        string defaultFileName = className + ".cs";

        string filePath = EditorUtility.SaveFilePanel("Save Buff Class", defaultDirectory, defaultFileName, "cs");

        if (!string.IsNullOrEmpty(filePath))
        {
            string classContent = GenerateClassContent(className);
            File.WriteAllText(filePath, classContent);

            AssetDatabase.Refresh();

            // ��Unity�༭���д������ɵ����ļ�
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath));
        }
    }

    private string GenerateClassContent(string className)
    {
        string classContent = @"
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "" " +className+ @" "", menuName = ""ScriptableObject/BaseBuff/" + className + @" "")]
    public class " + className + @" : BaseBuff
    {
        public override void AfterBeAdded()
        {
            // ʵ����Ӻ���߼�
            EventCenter.GetInstance().AddEventListener<BaseBuff>(this.buffApplyTime.ToString(), ApplyHandler);
        }

        public override void OnUpdate()
        {
            // ʵ�ָ��µ��߼�
            base.OnUpdate();
        }

        public override void AfterBeRemoved()
        {
            // ʵ���Ƴ�����߼�
            EventCenter.GetInstance().RemoveEventListener<BaseBuff>(this.buffApplyTime.ToString(), ApplyHandler);
        }

        public override void ApplyHandler(BaseBuff buff)
        {
            this.Apply();
        }

        public override void Apply()
        {
            // ʵ��Ӧ�õ��߼�
            base.Apply();
        }
    }
}";

        return classContent;
    }
}
