using System.IO;

using UnityEngine;

using UnityEditor;

namespace LuxTools.ScriptTemplateCreator
{
    public static class ScriptTemplateCreator
    {
        private const string folderPath = "Assets/ScriptTemplates";
         
        public static void CreateScript(TemplateData data)
        {
            CreateScript(data.templateName, TemplateUtils.GetScriptContent(data));
        }

        public static void CreateScript(string name, string content)
        {
            CheckScriptFolder();

            File.WriteAllText(folderPath + "/" + GetAssetName(name), content);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string GetAssetName(string name)
        {
            return "80 -Script Templates__" + name + " -" + name + ".cs.txt";
        }

        private static void CheckScriptFolder()
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
                AssetDatabase.CreateFolder("Assets", "ScriptTemplates");
        }
    }
}
