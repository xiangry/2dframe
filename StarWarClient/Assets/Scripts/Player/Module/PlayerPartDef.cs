using System;
using System.Collections.Generic;
using Player.Enum;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Player.Module
{
    [CreateAssetMenu(fileName = "part", menuName = "KVGame/Player/CreatePart", order = 50)] 
    public class PlayerPartDef : ScriptableObject
    {
        [SerializeField] public string name;
        [SerializeField] public PlayerPartType partType = PlayerPartType.None;
        [SerializeField] public PlayerPartSlot[] partSlots = new PlayerPartSlot[]{};

        [SerializeField] public SkeletonDataAsset skeletonDataAsset = null;
        [NonSerialized] public SkeletonAnimation skeletonAnimation = null;
    }
}