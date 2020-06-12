using Spine.Unity;
using Sword;
using UnityEditor;
using UnityEngine;

public class PlayerCreater
{
    [MenuItem("GameObject/Player/CreateAvatar", false, -1)]
    public static PlayerAvatar CreatPlayerAvatar()
    {
        PlayerAvatar playerAvatar;
        PlayerLegPart legPart;
        PlayerBodyPart bodyPart;
        {
            GameObject go = new GameObject("NewAvatar");
            if (Selection.activeTransform)
            {
                go.transform.SetParent(Selection.activeTransform);
            }
            UtilityGameObject.Reset(go);
            playerAvatar = go.AddComponent<PlayerAvatar>();
        }

        {
            GameObject go = new GameObject("leg_part");
            go.transform.SetParent(playerAvatar.transform);
            UtilityGameObject.Reset(go);
            legPart = go.AddComponent<PlayerLegPart>();
            playerAvatar.legPart = legPart;

            var anim = go.GetComponent<SkeletonAnimation>();
            anim.loop = true;
        }
        
        {
            GameObject go = new GameObject("body_root");
            go.transform.SetParent(legPart.transform);
            UtilityGameObject.Reset(go);
            bodyPart = go.AddComponent<PlayerBodyPart>();
            playerAvatar.bodyPart = bodyPart;
            legPart.bodyPart = bodyPart;
        }
        
        {
            GameObject go = new GameObject("body_part");
            go.transform.SetParent(bodyPart.transform);
            UtilityGameObject.Reset(go);
            go.AddComponent<SkeletonAnimation>();
            go.AddComponent<SkeletonRenderSeparator>();
            var anim = go.GetComponent<SkeletonAnimation>();
            anim.loop = true;
        }
        
        return playerAvatar;
    }
}
