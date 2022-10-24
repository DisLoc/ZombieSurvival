using UnityEngine;
using UnityEngine.UI;

public sealed class CombineAbilityUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Image _image;

    public void Initialize(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
