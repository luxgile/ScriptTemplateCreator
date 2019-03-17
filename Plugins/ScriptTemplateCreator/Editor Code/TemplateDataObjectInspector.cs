using UnityEngine;

using UnityEditor;

namespace LuxTools.ScriptTemplateCreator.Editor
{
    [CustomEditor(typeof(TemplateDataObject))]
    public class TemplateDataObjectInspector : UnityEditor.Editor
    {
        private TemplateDataObject template;

        public override void OnInspectorGUI()
        {
            if (template == null)
                template = target as TemplateDataObject;

            EditorGUILayout.LabelField("Template name", EditorStyles.boldLabel);
            template.data.templateName = EditorGUILayout.TextArea(template.data.templateName);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Namespace usage", EditorStyles.boldLabel);
            template.data.namespaceUsage = EditorGUILayout.TextArea(template.data.namespaceUsage);

            template.data.useNamespace = EditorGUILayout.Toggle("Use namespace", template.data.useNamespace);
            if (template.data.useNamespace)
            {
                template.data.namespaceName = EditorGUILayout.TextField("Namespace", template.data.namespaceName);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Attributes", EditorStyles.boldLabel);
            template.data.useClassAtt = EditorGUILayout.Toggle("Use attributes", template.data.useClassAtt);
            if (template.data.useClassAtt)
            {
                template.data.classAtts = EditorGUILayout.TextField("Attributes", template.data.classAtts);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Inheritance", EditorStyles.boldLabel);
            template.data.useInheritance = EditorGUILayout.Toggle("Use inheritance", template.data.useInheritance);
            if (template.data.useInheritance)
            {
                template.data.inheritanceName = EditorGUILayout.TextField("Inherits", template.data.inheritanceName);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Class Body", EditorStyles.boldLabel);
            template.data.classBody = EditorGUILayout.TextArea(template.data.classBody);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.HelpBox(TemplateUtils.GetScriptContent(template.data), MessageType.None);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorUtility.SetDirty(template);
        }
    }
}