using UnityEngine;

public interface ISoundPlayHandler : ISubscriber
{
    public void OnSoundPlay(SoundType sound);
}
