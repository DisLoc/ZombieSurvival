using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

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
    private const string MUSIC_VOLUME_VAR = "MusicVolume";
    private const string SOUNDS_VOLUME_VAR = "SoundsVolume";

    public void Initialize()
    {
        _musicState = SoundsStates.Enabled;
        _soundState = SoundsStates.Enabled;
    }

    private void Start()
    {
        float bounds = Load(MUSIC_VOLUME_VAR);

        _musicGroup.SetFloat(MIXER_NAME, bounds);
        _musicState = bounds == MIN_BOUNDS ? SoundsStates.Disabled : SoundsStates.Enabled;

        if (_musicState.Equals(SoundsStates.Enabled))
        {
            _musicImage.sprite = _musicOnIcon;
        }
        else
        {
            _musicImage.sprite = _musicOffIcon;
        }


        bounds = Load(SOUNDS_VOLUME_VAR);
        _soundState = bounds == MIN_BOUNDS ? SoundsStates.Disabled : SoundsStates.Enabled;
        
        foreach (var mixer in _soundGroups)
        {
            mixer.SetFloat(MIXER_NAME, bounds);
        }

        if (_soundState.Equals(SoundsStates.Enabled))
        {
            _soundsImage.sprite = _soundsOnIcon;
        }
        else
        {
            _soundsImage.sprite = _soundsOffIcon;
        }
    }

    public void OnSoundsClick()
    {
        float bounds;

        if (_soundState.Equals(SoundsStates.Enabled))
        {
            foreach (var mixer in _soundGroups)
            {
                mixer.SetFloat(MIXER_NAME, MIN_BOUNDS);
            }

            _soundState = SoundsStates.Disabled;
            _soundsImage.sprite = _soundsOffIcon;

            bounds = MIN_BOUNDS;
        }
        else
        {
            foreach (var mixer in _soundGroups)
            {
                mixer.SetFloat(MIXER_NAME, MAX_SOUNDS_BOUNDS);
            }

            _soundState = SoundsStates.Enabled;
            _soundsImage.sprite = _soundsOnIcon;

            bounds = MAX_SOUNDS_BOUNDS;
        }

        Save(SOUNDS_VOLUME_VAR, bounds);
    }

    public void OnMusicClick()
    {
        float bounds;

        if (_musicState.Equals(SoundsStates.Enabled))
        {
            _musicGroup.SetFloat(MIXER_NAME, MIN_BOUNDS);

            _musicState = SoundsStates.Disabled;
            _musicImage.sprite = _musicOffIcon;

            bounds = MIN_BOUNDS;
        }
        else
        {
            _musicGroup.SetFloat(MIXER_NAME, MAX_MUSIC_BOUNDS);

            _musicState = SoundsStates.Enabled;
            _musicImage.sprite = _musicOnIcon;

            bounds = MAX_MUSIC_BOUNDS;
        }

        Save(MUSIC_VOLUME_VAR, bounds);
    }

    private void Save(string varName, float value)
    {
        PlayerPrefs.SetFloat(varName, value);
    }

    private float Load(string varName)
    {
        if (PlayerPrefs.HasKey(varName))
        {
            return PlayerPrefs.GetFloat(varName);
        }
        else
        {
            switch (varName)
            {
                case MUSIC_VOLUME_VAR: return MAX_MUSIC_BOUNDS;
                case SOUNDS_VOLUME_VAR: return MAX_SOUNDS_BOUNDS;
                default:
                    Debug.Log("Missing key!");
                    return 0;
            }
        }
    }

    [ContextMenu("ResetData")]
    private void ResetData()
    {
        PlayerPrefs.DeleteKey(SOUNDS_VOLUME_VAR);
        PlayerPrefs.DeleteKey(MUSIC_VOLUME_VAR);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private enum SoundsStates
    {
        Enabled,
        Disabled
    }
}
