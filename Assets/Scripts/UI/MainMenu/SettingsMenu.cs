using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : UIMenu 
{
    [SerializeField] private AudioMixer _musicGroup;
    [SerializeField] private List<AudioMixer> _soundGroups;

    private SoundsStates _musicState;
    private SoundsStates _soundState;

    private const float MIN_BOUNDS = -80f;
    private const float MAX_MUSIC_BOUNDS = -15f;
    private const float MAX_SOUNDS_BOUNDS = -10f;

    private const string MIXER_NAME = "Master";

    public void Initialize()
    {
        _musicState = SoundsStates.Enabled;
        _soundState = SoundsStates.Enabled;
    }

    private void Start()
    {
        _musicGroup.SetFloat(MIXER_NAME, MAX_MUSIC_BOUNDS);

        foreach (var mixer in _soundGroups)
        {
            mixer.SetFloat(MIXER_NAME, MAX_SOUNDS_BOUNDS);
        }
    }

    public void OnSoundsClick()
    {
        if (_soundState.Equals(SoundsStates.Enabled))
        {
            foreach (var mixer in _soundGroups)
            {
                mixer.SetFloat(MIXER_NAME, MIN_BOUNDS);
            }

            _soundState = SoundsStates.Disabled;
        }
        else
        {
            foreach (var mixer in _soundGroups)
            {
                mixer.SetFloat(MIXER_NAME, MAX_SOUNDS_BOUNDS);
            }

            _soundState = SoundsStates.Enabled;
        }
    }

    public void OnMusicClick()
    {        
        if (_musicState.Equals(SoundsStates.Enabled))
        {
            _musicGroup.SetFloat(MIXER_NAME, MIN_BOUNDS);

            _musicState = SoundsStates.Disabled;
        }
        else
        {
            _musicGroup.SetFloat(MIXER_NAME, MAX_MUSIC_BOUNDS);

            _musicState = SoundsStates.Enabled;
        }
    }

    private enum SoundsStates
    {
        Enabled,
        Disabled
    }
}
