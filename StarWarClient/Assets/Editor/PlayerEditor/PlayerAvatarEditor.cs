using UnityEditor;

[CustomEditor(typeof(PlayerAvatar))]
public class PlayerAvatarEditor : Editor
{
    private bool dataChanged = false;
    public override void OnInspectorGUI()
    {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            dataChanged |= EditorGUI.EndChangeCheck(); // Value used in the next update.
            if (dataChanged)
            {
                var avatar = target as PlayerAvatar;
                avatar.AnimationName = avatar.AnimationName;
            }
    }
}
