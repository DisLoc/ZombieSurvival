using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPool : MonoBehaviour, ISoundPlayHandler//, IGameStartHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 1)] private float _masterVolume;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private AudioPlayer _playerPrefab;

    [SerializeField] private MixerList _mixers;

    private Dictionary<MixerTypes, int> _playingSounds;
    private List<AudioPlayer> _currentPlayers;
    private MonoPool<AudioPlayer> _players;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        EventBus.Unsubscribe(this);
    }

    public void OnGameStart()
    {
        _musicPlayer.PlayMusic();
    }

    private void Start()
    {
        int maxCapacity = 0;

        foreach (MixerType mixer in _mixers.Mixers)
        {
            maxCapacity += mixer.SoundsCountLimit;
        }

        _playingSounds = new Dictionary<MixerTypes, int>();

        foreach (var mixer in _mixers.Mixers)
        {
            _playingSounds.Add(mixer.Type, 0);
        }

        _players = new MonoPool<AudioPlayer>(_playerPrefab, maxCapacity, transform);
        _currentPlayers = new List<AudioPlayer>();

        OnGameStart();
    }

    public void OnSoundPlay(SoundType sound)
    {
        if (_players == null) return;

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
                if (sound.Type.Equals(SoundTypes.GameOver) || sound.Type.Equals(SoundTypes.LevelPassed))
                {
                    if (_isDebug) Debug.Log("Stop all sounds");
                    StopAllCoroutines();

                    _musicPlayer.StopAllCoroutines();

                    foreach (AudioPlayer audioPlayer in _currentPlayers)
                    {
                        audioPlayer.Stop(); 
                        _players.Release(audioPlayer);
                    }

                    _currentPlayers.Clear();

                    foreach (var mixer1 in _mixers.Mixers)
                    {
                        _playingSounds[mixer1.Type] = 0;
                    }
                }

                player.Play(sound.Sound, mixer, _masterVolume);
                StartCoroutine(WaitRelease(player, sound));

                return;
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
        _currentPlayers.Add(player);
        _playingSounds[sound.MixerType]++;

        yield return new WaitForSecondsRealtime(sound.Sound.length);

        if (_isDebug) Debug.Log("Releasing " + player);

        _currentPlayers.Remove(player);
        _playingSounds[sound.MixerType]--;
        _players.Release(player);
    }
}
