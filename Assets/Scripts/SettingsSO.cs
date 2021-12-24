using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
public class SettingsSO : ScriptableObject
{
    private string _dirPath;
    private string _filePath;

    [HideInInspector] [SerializeField] private bool _bgmMute;
    public bool BgmMute
    {
        get { return _bgmMute; }

        set
        {
            _bgmMute = value;
            onBgmMuteChanged?.Invoke(value);
            Save();
        }
    }

    [HideInInspector] [SerializeField] private bool _sfxMute;
    public bool SfxMute
    {
        get { return _sfxMute; }

        set
        {
            _sfxMute = value;
            onSfxMuteChanged?.Invoke(value);
            Save();
        }
    }

    public UnityAction<bool> onBgmMuteChanged;
    public UnityAction<bool> onSfxMuteChanged;

    public void Load()
    {
        //string json = File.ReadAllText(Path.Combine(_dirPath, _filePath));
        //Debug.Log("Load : " + json);
        //JsonUtility.FromJsonOverwrite(json, this);

        //BgmMute = _bgmMute;
        //SfxMute = _sfxMute;
    }

    public void Save()
    {
        //string json = JsonUtility.ToJson(this);

        //Debug.Log("Save : " + json);

        //if (!Directory.Exists(_dirPath)) Directory.CreateDirectory(_dirPath);
        //File.WriteAllText(Path.Combine(_dirPath, _filePath), json);
    }
}
