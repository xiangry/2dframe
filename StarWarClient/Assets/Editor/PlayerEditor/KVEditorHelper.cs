using System;
using System.Collections.Generic;
using KVFramework;
using Spine;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace PlayerEditor
{
    public class KVEditorHelper
    {
        public static void DrawButton(string text, Action action)
        {
            if (GUILayout.Button(text))
            {
                action.Invoke();
            }
        }
        
        public static void DrawSpace()
        {
            EditorGUILayout.Space();
        }

        public static void DrawLabel(string label)
        {
            EditorGUILayout.LabelField(label);
        }
        
        public static void DrawHelpBox(string helpText, MessageType messageType = MessageType.None, bool wide = true)
        {
            EditorGUILayout.HelpBox(helpText, messageType, wide);
        }

        #region Layout
        public static void GrouptHorizontal(GUIStyle style, Action action)
        {
            EditorGUILayout.BeginHorizontal(style);
            action.Invoke();
            EditorGUILayout.EndHorizontal();
        }

        public static void GroupVertical(GUIStyle style, Action action)
        {
            EditorGUILayout.BeginVertical(style);
            action.Invoke();
            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Spine GUI
        public static string DrawSpineBoneSelecter(string label, SkeletonDataAsset skeletonDataAsset, string current, Action<string> selectFunc)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            var bones = skeletonDataAsset.GetSkeletonData(true).Bones;
            var selectValues = new List<string>(bones.Count);
            bones.ForEach((data => {selectValues.Add(data.Name);}));
            var index = selectValues.FindIndex(a=>a.Equals(current));
            EditorGUILayout.LabelField(label);
            if (GUILayout.Button(SpineInspectorUtility.TempContent(current, SpineEditorUtilities.Icons.slot), EditorStyles.popup))
            {
                DrawSelectBone(skeletonDataAsset, current, selectFunc);
            }
            EditorGUILayout.EndHorizontal();
            return selectValues[index];
        }

        public static void DrawSelectBone(SkeletonDataAsset skeletonDataAsset , string current, Action<string> selectFunc)
        {
            SkeletonData data = skeletonDataAsset.GetSkeletonData(true);
            if (data == null) return;
            
            var menu = new GenericMenu();
            DrawBonePopulateMenu(menu, skeletonDataAsset, current, selectFunc);
            menu.ShowAsContext();
        }
        
        public static void DrawBonePopulateMenu (GenericMenu menu, SkeletonDataAsset skeletonDataAsset, string current, Action<string> selectFunc)
        {
            var skeletonData = skeletonDataAsset.GetSkeletonData(true);
            menu.AddDisabledItem(new GUIContent(skeletonDataAsset.name));
            menu.AddSeparator("");

            for (int i = 0; i < skeletonData.Bones.Count; i++) {
                string name = skeletonData.Bones.Items[i].Name;
                    menu.AddItem(new GUIContent(name), 
                        name == current,
                        data =>
                        {
                            string value = data as string;
                            selectFunc?.Invoke(value);
                        }, 
                        name);
            }
        }
        #endregion

        #region GUI STL
        public static T[] RemoveElement<T>(T[] elements, T element)
        {
            List<T> elementsList = new List<T>(elements);
            elementsList.Remove(element);
            return elementsList.ToArray();
        }

        public static T[] AddElement<T>(T[] elements, T element)
        {
            List<T> elementsList = new List<T>(elements);
            elementsList.Add(element);
            return elementsList.ToArray();
        }

        public static T[] CopyElement<T>(T[] elements, T element)
        {
            var objCopy = (object)(element as ICloneable).Clone();
            return elements;
        }

        public static T[] PasteElement<T>(T[] elements, T element)
        {
            if (CloneObject.objCopy == null) return elements;
            List<T> elementsList = new List<T>(elements);
            elementsList.Insert(elementsList.IndexOf(element) + 1, (T)CloneObject.objCopy);
            CloneObject.objCopy = null;
            return elementsList.ToArray();
        }

        public static T[] DuplicateElement<T>(T[] elements, T element)
        {
            List<T> elementsList = new List<T>(elements);
            elementsList.Insert(elementsList.IndexOf(element) + 1, (T)(element as ICloneable).Clone());
            return elementsList.ToArray();
        }

        public static T[] MoveElement<T>(T[] elements, T element, int steps)
        {
            List<T> elementsList = new List<T>(elements);
            int newIndex = Mathf.Clamp(elementsList.IndexOf(element) + steps, 0, elements.Length - 1);
            elementsList.Remove(element);
            elementsList.Insert(newIndex, element);
            return elementsList.ToArray();
        }

        public static void PaneOptions<T>(T[] elements, T element, System.Action<T[]> callback)
        {
            if (elements == null || elements.Length == 0) return;
            GenericMenu toolsMenu = new GenericMenu();

            if ((elements[0] != null && elements[0].Equals(element)) || (elements[0] == null && element == null) || elements.Length == 1)
            {
                toolsMenu.AddDisabledItem(new GUIContent("Move Up"));
                toolsMenu.AddDisabledItem(new GUIContent("Move To Top"));
            }
            else
            {
                toolsMenu.AddItem(new GUIContent("Move Up"), false, delegate() { callback(MoveElement<T>(elements, element, -1)); });
                toolsMenu.AddItem(new GUIContent("Move To Top"), false, delegate() { callback(MoveElement<T>(elements, element, -elements.Length)); });
            }
            if ((elements[elements.Length - 1] != null && elements[elements.Length - 1].Equals(element)) || elements.Length == 1)
            {
                toolsMenu.AddDisabledItem(new GUIContent("Move Down"));
                toolsMenu.AddDisabledItem(new GUIContent("Move To Bottom"));
            }
            else
            {
                toolsMenu.AddItem(new GUIContent("Move Down"), false, delegate() { callback(MoveElement<T>(elements, element, 1)); });
                toolsMenu.AddItem(new GUIContent("Move To Bottom"), false, delegate() { callback(MoveElement<T>(elements, element, elements.Length)); });
            }

            toolsMenu.AddSeparator("");

            if (element != null && element is System.ICloneable)
            {
                toolsMenu.AddItem(new GUIContent("Copy"), false, delegate() { callback(CopyElement<T>(elements, element)); });
            }
            else
            {
                toolsMenu.AddDisabledItem(new GUIContent("Copy"));
            }

            if (element != null && CloneObject.objCopy != null && CloneObject.objCopy.GetType() == typeof(T))
            {
                toolsMenu.AddItem(new GUIContent("Paste"), false, delegate() { callback(PasteElement<T>(elements, element)); });
            }
            else
            {
                toolsMenu.AddDisabledItem(new GUIContent("Paste"));
            }

            toolsMenu.AddSeparator("");

            if (!(element is System.ICloneable))
            {
                toolsMenu.AddDisabledItem(new GUIContent("Duplicate"));
            }
            else
            {
                toolsMenu.AddItem(new GUIContent("Duplicate"), false, delegate() { callback(DuplicateElement<T>(elements, element)); });
            }
            toolsMenu.AddItem(new GUIContent("Remove"), false, delegate() { callback(RemoveElement<T>(elements, element)); });

            toolsMenu.ShowAsContext();
            EditorGUIUtility.ExitGUI();
        }
    
        #endregion
    }
}