using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpellData))]
[CanEditMultipleObjects]
public class SpellDataCustomInspector : Editor
{
    SpellData data;
    SerializedProperty spellSprite;

    private void OnEnable()
    {
        data = (SpellData)target;
        spellSprite = serializedObject.FindProperty("UISprite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        #region DisplaySettings

        EditorGUILayout.LabelField("Display Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(15);

        EditorGUILayout.ObjectField("Spell Sprite", spellSprite.objectReferenceValue, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        data.Uses = EditorGUILayout.IntField("Uses", data.Uses);
        data.Damage = EditorGUILayout.IntField("Damage", data.Damage);

        data.Visuals = (Visuals)EditorGUILayout.EnumPopup("Spell Visuals", data.Visuals);

        EditorGUILayout.Space(30);

        #endregion

        #region SelectionFormSettings

        EditorGUILayout.LabelField("Selection From Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(15);

        data.LaunchForm = (SelectionForm)EditorGUILayout.EnumPopup("Launch Form", data.LaunchForm);
        data.SelectionMaxRange = EditorGUILayout.IntField("Selection Max Range", data.SelectionMaxRange);

        EditorGUILayout.Space(10);

        data.ThrowableOnWalls = EditorGUILayout.Toggle("Throwable On Walls", data.ThrowableOnWalls);
        data.GoesThroughWalls = EditorGUILayout.Toggle("Goes Throught Walls", data.GoesThroughWalls);

        data.BypassNearSelection = EditorGUILayout.Toggle("ByPass Near Selection", data.BypassNearSelection);
        if (data.BypassNearSelection)
        {
            data.SelectionBypassSize = EditorGUILayout.FloatField("Selection ByPass Size", data.SelectionBypassSize);
        }

        EditorGUILayout.Space(30);

        #endregion

        #region SpellFormSettings

        EditorGUILayout.LabelField("Spell Form Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(15);

        data.TargetForm = (SpellForm)EditorGUILayout.EnumPopup("Target Form", data.TargetForm);
        data.SpellMaxRange = EditorGUILayout.IntField("Spell Max Range", data.SpellMaxRange);

        EditorGUILayout.Space(10);

        data.BypassNearSpell = EditorGUILayout.Toggle("ByPass Near Spell", data.BypassNearSpell);
        if (data.BypassNearSpell)
        {
            data.SpellBypassSize = EditorGUILayout.FloatField("Spell ByPass Size", data.SpellBypassSize);
        }

        #endregion
    }

}
