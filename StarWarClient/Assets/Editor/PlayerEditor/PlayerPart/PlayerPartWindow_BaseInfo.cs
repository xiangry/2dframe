using Player.Enum;
using UnityEditor;
using UnityEngine;

namespace PlayerEditor
{
    public partial class PlayerPartWindow
    {
        void DrawPartBaseInfo()
        {
            EditorGUI.BeginChangeCheck();
            editPartDef.name = EditorGUILayout.TextField("部件", editPartDef.name);
            editPartDef.partType = (PlayerPartType)EditorGUILayout.EnumPopup("部件类型", editPartDef.partType);
            if (EditorGUI.EndChangeCheck())
            {
                RefreshWindowTitle();
            }
        }

        void RefreshWindowTitle()
        {
            titleContent = new GUIContent($"{editPartDef.name}的组件{editPartDef.partType}");
        }
    }
}