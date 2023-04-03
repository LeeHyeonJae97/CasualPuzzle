using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxChannel : AudioChannel
{
    private Dictionary<string, AudioSource> _sources = new Dictionary<string, AudioSource>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var clip in _clips)
        {
            var source = new GameObject(clip.Key).AddComponent<AudioSource>();
            source.transform.SetParent(transform);

            _sources[clip.Key] = source;
        }
    }

    public override void Play(string name, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        if (!_clips.TryGetValue(name, out var clip)) return;

        if (!_sources.TryGetValue(name, out var source)) return;

        source.name = name;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = false;
        source.spatialBlend = 0f;
        source.outputAudioMixerGroup = _mixerGroup;
        source.Play();
    }

    public override AudioSource Play(string name, Transform parent, Vector3 position, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    public override void Stop(AudioSource source)
    {
        throw new System.NotImplementedException();
    }
}
