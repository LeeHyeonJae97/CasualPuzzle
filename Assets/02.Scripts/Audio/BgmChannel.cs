using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChannel : AudioChannel
{
    private AudioSource _source;

    public override void Play(string name, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        if (!_clips.TryGetValue(name, out var clip)) return;

        if (_source == null)
        {
            _source = gameObject.AddComponent<AudioSource>();
        }

        _source.name = name;
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = pitch;
        _source.loop = true;
        _source.spatialBlend = 0f;
        _source.outputAudioMixerGroup = _mixerGroup;
        _source.Play();
    }

    public override AudioSource Play(string name, Transform parent, Vector3 position, float volume = 1, float pitch = 1, bool loop = false, float spatialBlend = 0)
    {
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        _source.Stop();
    }

    public override void Stop(AudioSource source)
    {
        throw new System.NotImplementedException();
    }
}
