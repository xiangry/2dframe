using Player.Module;
using UnityEditor;
using UnityEngine;

namespace PlayerEditor
{
    [CustomEditor(typeof(PlayerPartDef))]
    public class PlayerPartEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            KVEditorHelper.DrawHelpBox("这是角色组件");
            KVEditorHelper.DrawSpace();
            KVEditorHelper.DrawButton("Open PlayerPart", () =>
            {
                PlayerPartWindow.OpenPlayerWindow();
            });
//            base.OnInspectorGUI();
        }
    }
}