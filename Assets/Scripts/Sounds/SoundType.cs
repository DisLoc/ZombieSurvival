using UnityEngine;

[System.Serializable]
public class SoundType
{
    [SerializeField] private SoundTypes _soundType;
    [SerializeField] private MixerTypes _mixerType;
    [SerializeField] private AudioClip _sound;

    public SoundTypes Type => _soundType;
    public MixerTypes MixerType => _mixerType;
    public AudioClip Sound => _sound;
}
