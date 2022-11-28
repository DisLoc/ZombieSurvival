using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class MixerList
{
    [SerializeField] private List<MixerType> _mixerTypes;

    public List<MixerType> Mixers => _mixerTypes;

    public AudioMixerGroup this[SoundType type]
    {
        get
        {
            MixerType mixer = _mixerTypes.Find(item => item.Type.Equals(type.MixerType));

            if (mixer != null)
            {
                return mixer.Mixer;
            }

            return null;
        }
    }

    public int this[MixerTypes mixerType]
    {
        get
        {
            MixerType mixer = _mixerTypes.Find(item => item.Type.Equals(mixerType));

            if (mixer == null) return -1;

            else return mixer.SoundsCountLimit;
        }
    }
}
