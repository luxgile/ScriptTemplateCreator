using UnityEngine;

using UnityEditor;

namespace LuxTools.ScriptTemplateCreator.Editor
{
    public class ScriptTemplateCreatorEditorWindow : EditorWindow
    {
        private enum EditorType { Inspector, PropertyDrawer, }

        private TemplateData template;

        private string rawScriptName;
        private string rawScriptContent;

        private int currToolbar;

        private const int defaultScriptMode = 0;
        private const int rawScriptMode = 1;

        [MenuItem("Open Script Template Creator", menuItem = "Tools/Script Template Creator")]
        public static void Open()
        {
            GetWindow<ScriptTemplateCreatorEditorWindow>();
        }

        private void OnEnable()
        {
            titleContent = new GUIContent("Script Template Creator");
        }

        private void OnGUI()
        {
            if (template == null)
                template = new TemplateData();

            EditorGUILayout.Space();

            currToolbar = GUILayout.Toolbar(currToolbar, new string[] { "Default Script", "Raw Script" });

            switch (currToolbar)
            {
                case defaultScriptMode:
                    DrawDefaultEditor();
                    break;

                case rawScriptMode:
                    DrawRawEditor();
                    break;
            }
        }

        private void DrawDefaultEditor()
        {
            if (Event.current.commandName == "ObjectSelectorUpdated")
                template = (EditorGUIUtility.GetObjectPickerObject() as TemplateDataObject).data;
            
            EditorGUILayout.LabelField("Template name", EditorStyles.boldLabel);
            template.templateName = EditorGUILayout.TextArea(template.templateName);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Namespace usage", EditorStyles.boldLabel);
            template.namespaceUsage = EditorGUILayout.TextArea(template.namespaceUsage);

            template.useNamespace = EditorGUILayout.Toggle("Use namespace", template.useNamespace);
            if (template.useNamespace)
            {
                template.namespaceName = EditorGUILayout.TextField("Namespace", template.namespaceName);

            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Attributes", EditorStyles.boldLabel);
            template.useClassAtt = EditorGUILayout.Toggle("Use attributes", template.useClassAtt);
            if (template.useClassAtt)
            {
                template.classAtts = EditorGUILayout.TextField("Attributes", template.classAtts);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Inheritance", EditorStyles.boldLabel);
            template.useInheritance = EditorGUILayout.Toggle("Use inheritance", template.useInheritance);
            if (template.useInheritance)
            {
                template.inheritanceName = EditorGUILayout.TextField("Inherits", template.inheritanceName);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Body", EditorStyles.boldLabel);
            template.classBody = EditorGUILayout.TextArea(template.classBody);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.HelpBox(TemplateUtils.GetScriptContent(template), MessageType.None);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if (GUILayout.Button("Load Template"))
                EditorGUIUtility.ShowObjectPicker<TemplateDataObject>(null, true, "", 0);

            if (GUILayout.Button("Save As"))
                SaveCurrentTemplate();

            if (GUILayout.Button("Clear"))
                template = new TemplateData();

            EditorGUILayout.Space();

            GUI.enabled = CanCreateTemplate();
            if (GUILayout.Button("Create Template"))
            {
                ScriptTemplateCreator.CreateScript(template);
            }
            GUI.enabled = true;

            EditorGUILayout.HelpBox("Remember to restart unity to see new templates.", MessageType.Warning);

            if (!CheckTemplateName())
                EditorGUILayout.HelpBox("Template name cannot be empty", MessageType.Error);

            if (!CheckNamespaceName())
                EditorGUILayout.HelpBox("Namespace name cannot be empty", MessageType.Error);

            if (!CheckAttributes())
                EditorGUILayout.HelpBox("Attributes cannot be empty", MessageType.Error);

            if (!CheckInheritanceName())
                EditorGUILayout.HelpBox("Inheritance cannot be empty", MessageType.Error);
        }

        private void DrawRawEditor()
        {
            EditorGUILayout.LabelField("Template name", EditorStyles.boldLabel);
            rawScriptName = EditorGUILayout.TextArea(rawScriptName);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Template content", EditorStyles.boldLabel);
            rawScriptContent = EditorGUILayout.TextArea(rawScriptContent);

            GUI.enabled = !string.IsNullOrEmpty(rawScriptName);
            if (GUILayout.Button("Create Template"))
            {
                ScriptTemplateCreator.CreateScript(rawScriptName, rawScriptContent);
            }
            GUI.enabled = true;

            EditorGUILayout.HelpBox("Remember to restart unity to see new templates.", MessageType.Warning);
        }

        private void SaveCurrentTemplate()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Template", "New Template", "asset", "");
            if (!string.IsNullOrEmpty(path))
            {
                TemplateDataObject newTemplate = CreateInstance<TemplateDataObject>();
                newTemplate.data = template;

                AssetDatabase.CreateAsset(newTemplate, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private bool CanCreateTemplate()
        {
            bool can = true;
            can &= CheckTemplateName();
            can &= CheckNamespaceName();
            can &= CheckAttributes();
            can &= CheckInheritanceName();
            return can;
        }

        private bool CheckTemplateName()
        {
            return template.templateName != null && !string.IsNullOrEmpty(template.templateName);
        }

        private bool CheckNamespaceName()
        {
            if (!template.useNamespace)
                return true;

            return template.namespaceName != null && !string.IsNullOrEmpty(template.namespaceName);
        }

        private bool CheckAttributes()
        {
            if (!template.useClassAtt)
                return true;

            return template.classAtts != null && !string.IsNullOrEmpty(template.classAtts);
        }

        private bool CheckInheritanceName()
        {
            if (!template.useInheritance)
                return true;

            return template.inheritanceName != null && !string.IsNullOrEmpty(template.inheritanceName);
        }
    }
}
