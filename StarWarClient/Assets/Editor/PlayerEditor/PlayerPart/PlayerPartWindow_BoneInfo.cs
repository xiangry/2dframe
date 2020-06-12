using System;
using System.Collections.Generic;
using Player.Enum;
using Player.Module;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace PlayerEditor
{
    public partial class PlayerPartWindow
    {
        private bool showSlot = true;
        void DrawPartSlotInfo()
        {
            if (!partInfo.partAnim)
            {
                return;
            }

            showSlot = EditorGUILayout.Foldout(showSlot, "插槽");
            if (showSlot)
            {
                if (editPartDef.partSlots.Length > 0)
                {
                    for (int i = 0; i < editPartDef.partSlots.Length; i++)
                    {
                        DrawSlotInfo(i);
                    }
                }
                else
                {
                    KVEditorHelper.DrawButton("AddNew", () =>
                    {
                        editPartDef.partSlots = new PlayerPartSlot[]{new PlayerPartSlot()};
                    });
                }
            }
        }

        void DrawSlotInfo(int index)
        {
            KVEditorHelper.GroupVertical(MainGroupStyle,() =>
            {
                var slot = editPartDef.partSlots[index];
                EditorGUILayout.BeginHorizontal();
                KVEditorHelper.DrawLabel($"插槽信息:{index}");
                if (GUILayout.Button("", "PaneOptions"))
                {
                    KVEditorHelper.PaneOptions(
                        editPartDef.partSlots, slot,
                        delegate(PlayerPartSlot[] newPartSlots) { editPartDef.partSlots = newPartSlots; });
                }
                EditorGUILayout.EndHorizontal();
                
                slot.slotType = (PlayerPartSoltType)EditorGUILayout.EnumPopup("插槽类型", slot.slotType);
                KVEditorHelper.DrawSpineBoneSelecter("插槽骨骼", editPartDef.skeletonDataAsset, 
                    slot.slotName,
                    s => { slot.slotName = s; });
                slot.orderInLayer = EditorGUILayout.IntField("OrderInLayer", slot.orderInLayer);
            }); 
        }
    }
}