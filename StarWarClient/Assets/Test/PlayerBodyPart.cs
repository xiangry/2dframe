using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayerBodyPart : MonoBehaviour
{
    private BoneFollower boneFollower;
    public SkeletonAnimation bodyAnim;

    void Start()
    {
        boneFollower = GetComponent<BoneFollower>();
    }
}
