using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioChannel : MonoBehaviour
{
    public static Dictionary<string, AudioChannel> _channels = new Dictionary<string, AudioChannel>();

    public static AudioChannel Get(string name)
    {
        return _channels[name];
    }

    public static bool TryGet(string name, out AudioChannel channel)
    {
        return _channels.TryGetValue(name, out channel);
    }

    [SerializeField] protected string _name;
    [SerializeField] private bool _dontDestroyOnLoad;
    [SerializeField] private bool _useAsGlobal;
    [SerializeField] protected AudioMixerGroup _mixerGroup;
    [SerializeField] protected AudioClipDictionary _clips;

    public abstract void Play(string name, float volume = 1f, float pitch = 1f, bool loop = false, float spatialBlend = 0f);
    public abstract AudioSource Play(string name, Transform parent, Vector3 position, float volume = 1f, float pitch = 1f, bool loop = false, float spatialBlend = 0f);
    public abstract void Stop();
    public abstract void Stop(AudioSource source);

    protected virtual void Awake()
    {
        DontDestroyOnLoad();
        UseAsGlobal();

        void DontDestroyOnLoad()
        {
            if (_dontDestroyOnLoad)
            {
                GameObject.DontDestroyOnLoad(gameObject);
            }
        }

        void UseAsGlobal()
        {
            if (_useAsGlobal)
            {
                _channels[_name] = this;
            }
        }
    }

    private void OnDestroy()
    {
        if (_useAsGlobal)
        {
            _channels.Remove(name);
        }
    }
}

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