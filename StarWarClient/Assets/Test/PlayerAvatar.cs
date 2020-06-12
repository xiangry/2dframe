using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    public PlayerBodyPart bodyPart;
    public PlayerLegPart legPart;
    
    #region Serialized state and Beginner API
    [SerializeField]
    [SpineAnimation]
    private string _animationName;
    public string AnimationName {
        get {
            if (_animationName == null)
            {
                return legPart.GetComponent<SkeletonAnimation>().AnimationName;
            }

            return _animationName;
        }
        set
        {
            _animationName = value;
            legPart.GetComponent<SkeletonAnimation>().AnimationName = value;
            bodyPart.GetComponentInChildren<SkeletonAnimation>().AnimationName = value;
        }
    }
    
    #endregion

    #region BODY POS
    [SerializeField]
    public Vector3 bodyPartOffset = Vector3.zero;
    #endregion
    
}
