using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private AudioSource _source;

    private AudioClip _currentClip;
    private AudioMixerGroup _currentGroup;

    public void ResetObject()
    {
        Stop();

        _currentClip = null;
        _currentGroup = null;
    }

    public void Play(AudioClip clip, AudioMixerGroup group, float volume = 1)
    {
        _currentClip = clip;
        _currentGroup = group;

        _source.clip = _currentClip;
        _source.outputAudioMixerGroup = _currentGroup;
        _source.volume = volume;

        _source.Play();
    }

    public void Stop()
    {
        _source.Stop();
    }
}
