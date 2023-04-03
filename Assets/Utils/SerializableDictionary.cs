using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public List<KeyValuePair> kvps;

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        foreach (var kvp in kvps)
        {
            this[kvp.key] = kvp.value;
        }
    }

    [System.Serializable]
    public struct KeyValuePair
    {
        public TKey key;
        public TValue value;
    }
}

#if UNITY_EDITOR
public class SerializableDictionaryDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("kvps"));
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property.FindPropertyRelative("kvps"), label, true);
        EditorGUI.EndProperty();
    }
}

public class SerializableDictionaryKeyValuePairDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect rect1 = new Rect(position.x, position.y, position.width / 2 - 4, position.height);
        Rect rect2 = new Rect(position.center.x + 2, position.y, position.width / 2 - 4, position.height);

        EditorGUI.PropertyField(rect1, property.FindPropertyRelative("key"), GUIContent.none);
        EditorGUI.PropertyField(rect2, property.FindPropertyRelative("value"), GUIContent.none);
    }
}
#endif

#region Example
/*
[System.Serializable]
public class AudioClipDictionary : SerializableDictionary<string, AudioClip>
{

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(AudioClipDictionary))]
public class AudioClipDictionaryDrawer : SerializableDictionaryDrawer
{

}

[CustomPropertyDrawer(typeof(AudioClipDictionary.KeyValuePair))]
public class AudioClipDictionaryKeyValuePairDrawer : SerializableDictionaryKeyValuePairDrawer
{

}
#endif
*/
#endregion