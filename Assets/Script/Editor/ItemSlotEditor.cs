using System.Drawing;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemSlot))]
public class ItemSlotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty type = serializedObject.FindProperty("slotType");


        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemCount"));
        EditorGUILayout.PropertyField(type);

        switch (type.enumValueIndex)
        {
            case (int)SlotType.Item :
                EditorGUILayout.PropertyField(serializedObject.FindProperty("itemSO"));
                break;
            case (int)SlotType.Skill: 

                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}