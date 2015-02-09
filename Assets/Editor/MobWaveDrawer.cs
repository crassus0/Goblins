using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;

[CustomPropertyDrawer(typeof(MobWave))]

public class MobWaveDrawer : PropertyDrawer
{
    ReorderableList m_list;
    string elementName;
    private ReorderableList GetList(SerializedProperty property)
    {

        if (m_list == null || elementName != property.propertyPath)
        {
            elementName = property.propertyPath;
            m_list = new ReorderableList(property.serializedObject, property, true, true, true, true);
            m_list.drawElementCallback = (UnityEngine.Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.width -= 20;

                EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
            };
        }
        return m_list;
    }
    public override float GetPropertyHeight(SerializedProperty property, UnityEngine.GUIContent label)
    {
        if (!property.isExpanded) return 3*EditorGUIUtility.singleLineHeight;
        return GetList(property.FindPropertyRelative("Mobs")).GetHeight() + 3*EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        position.x += 15;
        position.width -= 15;
        Rect foldoutPos = position;
        foldoutPos.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(foldoutPos, property.FindPropertyRelative("Name"));
        foldoutPos.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(foldoutPos, property.FindPropertyRelative("Cooldown"));
        foldoutPos.y += EditorGUIUtility.singleLineHeight;
        property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, new GUIContent("Monsters"));
        position.y += 3*EditorGUIUtility.singleLineHeight;
        //position.height += 15;
        if (!property.isExpanded) return;
        var listProperty = property.FindPropertyRelative("Mobs");
        var list = GetList(listProperty);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Monster Wave");
        };
        //property.isExpanded;
        var height = 0f;
        for (var i = 0; i < listProperty.arraySize; i++)
        {
            height = EditorGUIUtility.singleLineHeight;
        }
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            //rect.y += 2;
            EditorGUI.LabelField(new Rect(rect.x-15, rect.y, 65, EditorGUIUtility.singleLineHeight), "Monster");
            EditorGUI.PropertyField(
                new Rect(rect.x + 45, rect.y, rect.width - 45 - 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("MobPrefab"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 40, rect.y, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("MobCooldown"), GUIContent.none);
        };
        list.elementHeight = height;
        list.DoList(position);
    }

}
[CustomEditor(typeof(Spawner))]
public class SpawnerEditor:Editor
{
    ReorderableList m_list;
    private void OnEnable()
    {
        m_list = new ReorderableList(serializedObject, serializedObject.FindProperty("MobWaves"), true, true, true, true);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        m_list.DoLayoutList();
        
        m_list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                /*rect.width -= 35;
                rect.x += 15;
                
                
                EditorGUI.PropertyField(rect, serializedObject.FindProperty("MobWaves").GetArrayElementAtIndex(index), true);
                 */
                SerializedProperty element = serializedObject.FindProperty("MobWaves").GetArrayElementAtIndex(index);
                EditorGUI.LabelField(new Rect(rect.x,rect.y, 60, rect.height), "Wave " + index.ToString());
                EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60-40, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Name"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(rect.x  + rect.width - 40, rect.y, 40, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Cooldown"), GUIContent.none);
            };
        
        serializedObject.ApplyModifiedProperties();
    }
}