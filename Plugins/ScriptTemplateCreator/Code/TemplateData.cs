namespace LuxTools.ScriptTemplateCreator
{
    [System.Serializable]
    public class TemplateData
    {
        public string templateName;

        public string namespaceUsage;
        public string classBody;

        public bool useNamespace;
        public string namespaceName;

        public bool useClassAtt;
        public string classAtts;

        public bool useInheritance;
        public string inheritanceName;
    }
}
