using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private SoundList _musics;

    public void PlayMusic()
    {
        AudioClip clip =  _musics.PlaySound(SoundTypes.Music);

        if (clip == null)
        {
            Debug.Log("Missing track!");
            return;
        }

        StartCoroutine(WaitForNewTrack(clip.length));
    }

    private IEnumerator WaitForNewTrack(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        PlayMusic();
    }
}
