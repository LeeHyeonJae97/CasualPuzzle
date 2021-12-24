using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class ClipInfo
    {
        public string id;
        public AudioClip clip;
    }

    public static AudioManager Instance;

    [SerializeField] private ClipInfo[] clipInfos;

    private Dictionary<string, AudioClip> clipDic = new Dictionary<string, AudioClip>();

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private SettingsSO _settings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < clipInfos.Length; i++)
            clipDic.Add(clipInfos[i].id, clipInfos[i].clip);
    }

    private void Start()
    {
        _settings.onBgmMuteChanged += (value) => bgmSource.mute = value;
        _settings.onSfxMuteChanged += (value) => sfxSource.mute = value;
    }

    public void PlayBgm(string id)
    {
        if (clipDic.ContainsKey(id))
        {
            bgmSource.clip = clipDic[id];
            bgmSource.Play();
        }
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PlaySfx(string id)
    {
        if (clipDic.ContainsKey(id))
        {
            sfxSource.clip = clipDic[id];
            sfxSource.Play();
        }
    }

    public void StopSfx()
    {
        sfxSource.Stop();
    }
}
