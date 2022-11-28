using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPool : MonoBehaviour, ISoundPlayHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 1)] private float _masterVolume;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private AudioPlayer _playerPrefab;

    [SerializeField] private MixerList _mixers;

    private Dictionary<MixerTypes, int> _playingSounds;
    private MonoPool<AudioPlayer> _players;

    private void OnEnable()
    {
        EventBus.Subscribe(this);

        int maxCapacity = 0;

        foreach (MixerType mixer in _mixers.Mixers)
        {
            maxCapacity += mixer.SoundsCountLimit;
        }

        _playingSounds = new Dictionary<MixerTypes, int>();

        foreach(var mixer in _mixers.Mixers)
        {
            _playingSounds.Add(mixer.Type, 0);
        }

        _players = new MonoPool<AudioPlayer>(_playerPrefab, maxCapacity, transform);
        _musicPlayer.PlayMusic();
    }
    
    private void OnDisable()
    {
        EventBus.Subscribe(this);
    }

    public void OnSoundPlay(SoundType sound)
    {
        if (_isDebug) Debug.Log("Try to play " + sound);

        if (sound.Sound == null)
        {
            if (_isDebug) Debug.Log("Missing clip!");

            return;
        }

        if (!CheckMaxSounds(sound))
        {
            if (_isDebug) Debug.Log("Reached limit of sound at same time");

            return;
        }

        AudioMixerGroup mixer = _mixers[sound];

        if (mixer != null)
        {
            if (_isDebug) Debug.Log("Play " + sound.Sound);

            AudioPlayer player = _players.Pull();

            if (player != null)
            {
                player.Play(sound.Sound, mixer, _masterVolume);

                _playingSounds[sound.MixerType]++;

                StartCoroutine(WaitRelease(player, sound));
            }
            else if (_isDebug) Debug.Log("Missing AudioPlayer!");
        }
        else if (_isDebug) Debug.Log("Missing mixer!");
    }

    private bool CheckMaxSounds(SoundType sound)
    {
        if (_playingSounds.ContainsKey(sound.MixerType))
        {
            return _playingSounds[sound.MixerType] < _mixers[sound.MixerType];
        }
        else return false;
    }

    private IEnumerator WaitRelease(AudioPlayer player, SoundType sound)
    {
        yield return new WaitForSeconds(sound.Sound.length);

        if (_isDebug) Debug.Log("Releasing " + player);

        _playingSounds[sound.MixerType]--;
        _players.Release(player);
    }
}
