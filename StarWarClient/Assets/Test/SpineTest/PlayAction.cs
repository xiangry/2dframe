using System;
using UnityEngine;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp()]
public class PlayAction : MonoBehaviour
{
	List<string> actionList = new List<string>(5);

	public float uiOffsetX = 0f;
	
	private void Awake()
	{
		SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
		var animationClips = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Animations;
		foreach (var animation in animationClips)
		{
			actionList.Add(animation.Name);
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(uiOffsetX,0,300, 400));
		GUILayout.BeginVertical();
		foreach (var actionName in actionList)
		{
			if (GUILayout.Button("播放" + actionName))
			{
				PlayNewAction(actionName);
			}
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	[LuaCallCSharp]
	private void PlayNewAction(string actionName)
	{
		SkeletonAnimation[] skeletonAnimations = GetComponentsInChildren<SkeletonAnimation>();
		foreach (var animation in skeletonAnimations)
		{
			animation.AnimationName = actionName;
		}
	}
}
