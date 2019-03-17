using UnityEngine;

namespace LuxTools.ScriptTemplateCreator
{
    [CreateAssetMenu(fileName = "New Script Template", menuName = "Script Template")]
    public class TemplateDataObject : ScriptableObject
    {
        public TemplateData data;
    }
}