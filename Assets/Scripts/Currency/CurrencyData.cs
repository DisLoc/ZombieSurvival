using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Currency/Currency data", fileName = "New currency data")]
public class CurrencyData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _background;
    [SerializeField] private UpgradeMarker _marker;

    public Sprite Icon => _icon;
    public Sprite Background => _background;
    public UpgradeMarker Marker => _marker;
}
