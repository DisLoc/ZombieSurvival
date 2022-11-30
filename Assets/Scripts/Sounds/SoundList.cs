using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundList
{
    [SerializeField] private List<SoundType> _sounds;

    public AudioClip PlaySound(SoundTypes type)
    {
        List<SoundType> sounds = _sounds.FindAll(item => item.Type.Equals(type) && (!item.wasPlayed || 
                                                    (item.wasPlayed && Time.realtimeSinceStartup >= item.LastPlayTime + item.PlaybackCooldown)));

        SoundType sound = null;

        if (sounds.Count == 0)
        {
            return null;
        }
        else
        {
            sound = sounds[Random.Range(0, sounds.Count)];
            sound.Play(Time.realtimeSinceStartup);
        }

        EventBus.Publish<ISoundPlayHandler>(handler => handler.OnSoundPlay(sound));

        return sound.Sound;
    }

    public void PlaySound(AudioClip clip)
    {
        SoundType sound = _sounds.Find(item => item.Sound.Equals(clip));

        if (sound != null)
        {
            EventBus.Publish<ISoundPlayHandler>(handler => handler.OnSoundPlay(sound));
        }
        else return;
    }
}
