using UnityEngine;

[System.Serializable]
public class SoundType
{
    [SerializeField] private SoundTypes _soundType;
    [SerializeField] private MixerTypes _mixerType;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _playbackCooldown = 0.5f;

    private float _time;

    public SoundTypes Type => _soundType;
    public MixerTypes MixerType => _mixerType;
    public AudioClip Sound => _sound;
    public float PlaybackCooldown => _playbackCooldown;
    public float LastPlayTime => _time;

    public bool wasPlayed { private set; get; }

    public void Play(float time)
    {
        _time = time;
        wasPlayed = true;
    }
}
