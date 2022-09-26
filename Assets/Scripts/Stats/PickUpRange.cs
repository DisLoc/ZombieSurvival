using UnityEngine;

[System.Serializable]
public struct PickUpRange : IStat
{
    [SerializeField] private float _pickUpRange;
    [SerializeField] private Level _level;

    public float Value => _pickUpRange;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
