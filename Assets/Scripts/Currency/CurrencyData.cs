using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Currency/Currency data", fileName = "New currency data")]
public class CurrencyData : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected Sprite _background;
    [SerializeField] protected UpgradeMarker _marker;

    public Sprite Icon => _icon;
    public Sprite Background => _background;
    public UpgradeMarker Marker => _marker;
}
