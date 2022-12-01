using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : UIMenu 
{
    [Header("Settings menu")]
    [SerializeField] private AudioMixer _musicGroup;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Sprite _musicOnIcon;
    [SerializeField] private Sprite _musicOffIcon;

    [Space(5)]
    [SerializeField] private List<AudioMixer> _soundGroups;
    [SerializeField] private Image _soundsImage;
    [SerializeField] private Sprite _soundsOnIcon;
    [SerializeField] private Sprite _soundsOffIcon;

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
            _soundsImage.sprite = _soundsOffIcon;
        }
        else
        {
            foreach (var mixer in _soundGroups)
            {
                mixer.SetFloat(MIXER_NAME, MAX_SOUNDS_BOUNDS);
            }

            _soundState = SoundsStates.Enabled;
            _soundsImage.sprite = _soundsOnIcon;
        }
    }

    public void OnMusicClick()
    {        
        if (_musicState.Equals(SoundsStates.Enabled))
        {
            _musicGroup.SetFloat(MIXER_NAME, MIN_BOUNDS);

            _musicState = SoundsStates.Disabled;
            _musicImage.sprite = _musicOffIcon;
        }
        else
        {
            _musicGroup.SetFloat(MIXER_NAME, MAX_MUSIC_BOUNDS);

            _musicState = SoundsStates.Enabled;
            _musicImage.sprite = _musicOnIcon;
        }
    }

    private enum SoundsStates
    {
        Enabled,
        Disabled
    }
}
