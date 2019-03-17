
namespace LuxTools.ScriptTemplateCreator
{
    public static class TemplateUtils
    {
        public static string GetScriptContent(TemplateData data)
        {
            int depth = 0;
            string classContent = "";

            if (data.namespaceUsage != null)
            {
                string[] namespaceUsageLines = data.namespaceUsage.Split('\n');
                for (int i = 0; i < namespaceUsageLines.Length; i++)
                {
                    classContent += namespaceUsageLines[i] + "\n";
                }

                classContent += "\n";
            }

            if (data.useNamespace)
            {
                depth++;
                classContent += "namespace " + data.namespaceName + " \n";
                classContent += "{";
                classContent += "\n";
            }

            if (data.useClassAtt)
            {
                classContent += "[" + data.classAtts + "]" + "\n";
            }

            classContent += GetIndentDepth(depth) + "public class #SCRIPTNAME#";

            if (data.useInheritance)
            {
                classContent += " : " + data.inheritanceName;
            }

            classContent += "\n";
            classContent += GetIndentDepth(depth) + "{";
            classContent += "\n";

            if (data.classBody != null)
            {
                depth++;
                string[] classBodyLines = data.classBody.Split('\n');
                for (int i = 0; i < classBodyLines.Length; i++)
                {
                    classContent += GetIndentDepth(depth) + classBodyLines[i] + "\n";
                }
                depth--;
            }

            classContent += GetIndentDepth(depth) + "}";

            if (data.useNamespace)
            {
                depth--;
                classContent += "\n";
                classContent += "}";
            }

            return classContent;
        }

        private static string GetIndentDepth(int depth)
        {
            string indent = "";
            for (int i = 0; i < depth; i++)
            {
                indent += "\t";
            }
            return indent;
        }
    }
}