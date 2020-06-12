using System;
using KVFramework;
using Player.Enum;
using Spine.Unity;
using UnityEngine;

namespace Player.Module
{
    [Serializable]
    public class PlayerPartSlot : CloneBase
    {
        [SerializeField] public PlayerPartSoltType slotType = PlayerPartSoltType.None;
        [SerializeField] public string slotName = "root";
        [SerializeField] public int orderInLayer = 0;
    }
}