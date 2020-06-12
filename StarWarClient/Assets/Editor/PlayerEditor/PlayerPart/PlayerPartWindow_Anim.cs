using System;
using System.Collections.Generic;
using System.Diagnostics;
using Spine;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace PlayerEditor
{
    public partial class PlayerPartWindow
    {
        void DrawPartAnim()
        {
            EditorGUI.BeginChangeCheck();
            editPartDef.skeletonDataAsset = EditorGUILayout.ObjectField("动画", editPartDef.skeletonDataAsset, typeof(SkeletonDataAsset)) as SkeletonDataAsset;
            if (!editPartDef.skeletonDataAsset)
            {
                KVEditorHelper.DrawHelpBox("请设置动画", MessageType.Error);
                return;
            }
            
            
            if (!partInfo.partAnim)
            {
                KVEditorHelper.DrawButton("加载动画", () =>
                {
                    LoadPartAnim();
                });
            }
            else
            {
                partInfo.partAnim = EditorGUILayout.ObjectField(
                    "动画Object", 
                    partInfo.partAnim, 
                    typeof(SkeletonAnimation)) as SkeletonAnimation;
            }
        }

        void LoadPartAnim()
        {
            var skeletonAnimation = EditorInstantiation.InstantiateSkeletonAnimation(editPartDef.skeletonDataAsset);
            skeletonAnimation.gameObject.name = $"{editPartDef.name}_{editPartDef.partType}";
            partInfo.partAnim = skeletonAnimation;

            var seletonRenderSeparator = skeletonAnimation.gameObject.AddComponent<SkeletonRenderSeparator>();
//            seletonRenderSeparator.partsRenderers
            AddPartsRenderer(seletonRenderSeparator, 1);
            DetectOrphanedPartsRenderers(seletonRenderSeparator);
            
//            foreach (var slot in skeleton.DrawOrder) {
//	            var slotNames = SkeletonRendererInspector.GetSeparatorSlotNames(skeletonRenderer);
//	            for (int i = 0, n = slotNames.Length; i < n; i++) {
//		            if (string.Equals(slotNames[i], slot.Data.Name, System.StringComparison.Ordinal)) {
//			            EditorGUILayout.LabelField(SeparatorString);
//			            break;
//		            }
//	            }
//	            using (new EditorGUI.DisabledScope(!slot.Bone.Active)) {
//		            EditorGUILayout.LabelField(
//			            SpineInspectorUtility.TempContent(slot.Data.Name, Icons.slot), 
//			            GUILayout.ExpandWidth(false));
//	            }
//            }
            
        }
        
        

        #region SpineSeqaratorTool 
        public void AddPartsRenderer (SkeletonRenderSeparator component, int count) {
			var componentRenderers = component.partsRenderers;
			bool emptyFound = componentRenderers.Contains(null);
			if (emptyFound) {
				bool userClearEntries = EditorUtility.DisplayDialog("Empty entries found", "Null entries found. Do you want to remove null entries before adding the new renderer? ", "Clear Empty Entries", "Don't Clear");
				if (userClearEntries) componentRenderers.RemoveAll(x => x == null);
			}

			Undo.RegisterCompleteObjectUndo(component, "Add Parts Renderers");
			for (int i = 0; i < count; i++) {
				int index = componentRenderers.Count;
				var smr = SkeletonPartsRenderer.NewPartsRendererGameObject(component.transform, index.ToString());
				Undo.RegisterCreatedObjectUndo(smr.gameObject, "New Parts Renderer GameObject.");
				componentRenderers.Add(smr);

				// increment renderer sorting order.
				if (index == 0) continue;
				var prev = componentRenderers[index - 1]; if (prev == null) continue;

				var prevMeshRenderer = prev.GetComponent<MeshRenderer>();
				var currentMeshRenderer = smr.GetComponent<MeshRenderer>();
				if (prevMeshRenderer == null || currentMeshRenderer == null) continue;

				int prevSortingLayer = prevMeshRenderer.sortingLayerID;
				int prevSortingOrder = prevMeshRenderer.sortingOrder;
				currentMeshRenderer.sortingLayerID = prevSortingLayer;
				currentMeshRenderer.sortingOrder = prevSortingOrder + SkeletonRenderSeparator.DefaultSortingOrderIncrement;
			}

		}

		/// <summary>Detects orphaned parts renderers and offers to delete them.</summary>
		public void DetectOrphanedPartsRenderers (SkeletonRenderSeparator component) {
			var children = component.GetComponentsInChildren<SkeletonPartsRenderer>();

			var orphans = new System.Collections.Generic.List<SkeletonPartsRenderer>();
			foreach (var r in children) {
				if (!component.partsRenderers.Contains(r))
					orphans.Add(r);
			}

			if (orphans.Count > 0) {
				if (EditorUtility.DisplayDialog("Destroy Submesh Renderers", "Unassigned renderers were found. Do you want to delete them? (These may belong to another Render Separator in the same hierarchy. If you don't have another Render Separator component in the children of this GameObject, it's likely safe to delete. Warning: This operation cannot be undone.)", "Delete", "Cancel")) {
					foreach (var o in orphans) {
						Undo.DestroyObjectImmediate(o.gameObject);
					}
				}
			}
		}
        #endregion
    }
}