using UnityEngine;

[System.Serializable]
public sealed class ObjectChanceSpawn<TObject>
{
    [SerializeField] private TObject _objectPrefab;
    [SerializeField] private Chance _spawnChance;

    public TObject ObjectPrefab => _objectPrefab;
    public float SpawnChance => _spawnChance.Value;
    public bool ChanceIsTrue => SpawnChance == 1f;
}
