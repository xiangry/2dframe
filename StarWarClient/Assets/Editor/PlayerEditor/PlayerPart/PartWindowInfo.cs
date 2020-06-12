using Spine.Unity;
using UnityEngine;

namespace PlayerEditor
{
    public class PartWindowInfo
    {
        public SkeletonAnimation partAnim = null;

        public void Clear()
        {
            if (partAnim)
            {
                GameObject.DestroyImmediate(partAnim.gameObject);
                partAnim = null;
            }
        }
    }
}