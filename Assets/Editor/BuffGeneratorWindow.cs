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

            // 在Unity编辑器中打开新生成的类文件
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
            // 实现添加后的逻辑
            EventCenter.GetInstance().AddEventListener<BaseBuff>(this.buffApplyTime.ToString(), ApplyHandler);
        }

        public override void OnUpdate()
        {
            // 实现更新的逻辑
            base.OnUpdate();
        }

        public override void AfterBeRemoved()
        {
            // 实现移除后的逻辑
            EventCenter.GetInstance().RemoveEventListener<BaseBuff>(this.buffApplyTime.ToString(), ApplyHandler);
        }

        public override void ApplyHandler(BaseBuff buff)
        {
            this.Apply();
        }

        public override void Apply()
        {
            // 实现应用的逻辑
            base.Apply();
        }
    }
}";

        return classContent;
    }
}
