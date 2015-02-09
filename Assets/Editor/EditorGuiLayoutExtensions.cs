using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
public static class EditorGUILayoutExtensions
{
    public static List<T> InterfaceListField<T>(string name, List<T> value, ref bool collapsed, params GUILayoutOption[] elementFieldsOptions) where T : class
    {
        collapsed = EditorGUILayout.Foldout(collapsed, name);
        if (collapsed)
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel++;
            int arraySize = EditorGUILayout.IntField("Count", value.Count);
            if (value.Count > arraySize)
            {
                value.RemoveRange(arraySize, value.Count - arraySize);
            }
            for (int i = value.Count; i < arraySize; i++)
            {
                value.Add(null);
            }
            for (int i = 0; i < arraySize; i++)
            {
                MonoBehaviour x;
                x = (EditorGUILayout.ObjectField("Element " + i.ToString(), value[i] as UnityEngine.Object, typeof(MonoBehaviour), false, elementFieldsOptions) as MonoBehaviour);
                value[i] = x as T;

            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        return value;
    }
    public static List<T> ObjectListField<T>(string name, List<T> value, ref bool collapsed, params GUILayoutOption[] elementFieldsOptions) where T : class
    {
        collapsed = EditorGUILayout.Foldout(collapsed, name);
        if (collapsed)
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel++;
            int arraySize = EditorGUILayout.IntField("Count", value.Count);
            if (value.Count > arraySize)
            {
                value.RemoveRange(arraySize, value.Count - arraySize);
            }
            for (int i = value.Count; i < arraySize; i++)
            {
                value.Add(null);
            }
            for (int i = 0; i < arraySize; i++)
            {
                
                
                value[i] = (EditorGUILayout.ObjectField("Element " + i.ToString(), (value[i] as UnityEngine.Object), typeof(T), false, elementFieldsOptions) as T);

            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        return value;
    }

}

