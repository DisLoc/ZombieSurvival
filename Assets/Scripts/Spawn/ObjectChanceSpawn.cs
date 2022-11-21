using UnityEngine;

[System.Serializable]
public sealed class ObjectChanceSpawn<TObject> where TObject : class
{
    [SerializeField] private TObject _object;
    [SerializeField] private Chance _spawnChance;

    public TObject Object => _object;
    public Chance SpawnChance => _spawnChance;
    public bool ChanceIsTrue => _spawnChance.Probability == 1;
}
