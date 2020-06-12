/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated January 1, 2020. Replaces all prior versions.
 *
 * Copyright (c) 2013-2020, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using System;
using UnityEngine;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using UnityEngine.UI;

/// <summary>
/// Example code for a component that replaces the default attachment of a slot with an image from a Spine atlas.</summary>
public class SoltAppend : MonoBehaviour {

	[SerializeField] protected bool inheritProperties = true;
	[SerializeField] [SpineSlot] public string slotName;
	public GameObject slotNode;
	[SerializeField]protected float baseScale = 1f;

	private SkeletonRenderer skeletonRenderer;
	private Slot slot;

	void Awake () {
		skeletonRenderer = GetComponent<SkeletonRenderer>();
		skeletonRenderer.OnRebuild += Apply;
		if (skeletonRenderer.valid) Apply(skeletonRenderer);
	}

	void Apply (SkeletonRenderer skeletonRenderer) {
		if (!this.enabled) return;
	}

	private void Update()
	{
		slot = skeletonRenderer.Skeleton.FindSlot(slotName);
		if (slot != null && slotNode != null) {
			slotNode.transform.localPosition = new Vector3(slot.Bone.WorldX, slot.Bone.WorldY, 0); 
			slotNode.transform.localScale = new Vector3(slot.Bone.WorldScaleX * baseScale, slot.Bone.WorldScaleY * baseScale, 0); 
			slotNode.transform.localRotation = Quaternion.Euler(0, 0, slot.Bone.WorldRotationY); 
		}
	}
}
