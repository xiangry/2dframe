using System;
using Player.Module;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace PlayerEditor
{
    public partial class PlayerPartWindow : EditorWindow
    {
        const string WindonTitle = "Edit Player Part Info";
        private Vector2 m_scrollPosition = Vector2.zero;
        private PlayerPartDef editPartDef;

        private PartWindowInfo partInfo = null;

        private const string MainGroupStyle = "GroupBox";
        private const string SubGroupStyle = "ObjectFieldThumb";

        public static void OpenPlayerWindow()
        {
            PlayerPartWindow window = (PlayerPartWindow)GetWindow( typeof( PlayerPartWindow ), false, WindonTitle );
            window.Show();
            window.RefreshWindowTitle();
        }
        
        private void RefreshIfNeed()
        {
            UnityEngine.Object[] selection = Selection.GetFiltered(typeof(PlayerPartDef), SelectionMode.Assets);
            if (selection.Length > 0)
            {
                if (selection[0] == null)
                {
                    return;
                }

                //characterInfoSO = new SerializedObject(selection[0]);
                var selectPartDef = selection[0] as PlayerPartDef;
                if (selectPartDef && (!editPartDef || !selectPartDef.Equals(editPartDef)))
                {
                    ResetPartDef(selectPartDef);
                }
            }
        }

        private void ResetPartDef(PlayerPartDef partDef)
        {
            if (partInfo == null)
            {
                partInfo = new PartWindowInfo();
            }
            else
            {
                partInfo.Clear();
            }

            editPartDef = partDef;
            RefreshWindowTitle();
        }

        private void OnEnable()
        {
            RefreshIfNeed();
//            Debug.LogError($"OnEnable");
        }

        private void OnDisable()
        {
//            Debug.LogError($"OnDisable");
        }

        private void OnFocus()
        {
            RefreshIfNeed();
//            Debug.LogError($"OnFocus");
        }

        private void OnLostFocus()
        {
//            Debug.LogError($"OnLostFocus");
        }

        private void OnSelectionChange()
        {
            RefreshIfNeed();
//            Debug.LogError($"OnSelectionChange");
        }

        private void OnDestroy()
        {
//            Debug.LogError($"OnDestroy");
            partInfo.Clear();
            partInfo = null;
        }

        public void OnGUI()
        {
            DrawPartBaseInfo();
            DrawPartAnim();
            
            m_scrollPosition = GUILayout.BeginScrollView( m_scrollPosition );
            DrawPartSlotInfo();
            GUILayout.EndScrollView();
            
            KVEditorHelper.DrawButton("Save", () =>
            {
                EditorUtility.SetDirty(editPartDef);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            });
        }
    }
}