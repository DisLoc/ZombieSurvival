using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Currency/Currency data", fileName = "New currency data")]
public class CurrencyData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private UpgradeMarker _marker;

    public Sprite Icon => _icon;
    public UpgradeMarker Marker => _marker;
}
