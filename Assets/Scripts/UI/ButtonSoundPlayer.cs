using UnityEngine;
public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundList _sounds;

    public void Click()
    {
        _sounds.PlaySound(SoundTypes.Click);
    }
}
