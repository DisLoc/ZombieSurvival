using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChanceCombiner<T> where T : class
{
    [SerializeField] private List<ObjectChanceSpawn<T>> _spawnChances;

    public List<ObjectChanceSpawn<T>> SpawnChances => _spawnChances;

    public ChanceCombiner()
    {
        _spawnChances = new List<ObjectChanceSpawn<T>>();
    }

    public ChanceCombiner(List<ObjectChanceSpawn<T>> spawnChances)
    {
        _spawnChances = new List<ObjectChanceSpawn<T>>();

        foreach(ObjectChanceSpawn<T> spawnChance in spawnChances)
        {
            Add(spawnChance);
        }
    }

    public void Initialize()
    {
        foreach (ObjectChanceSpawn<T> spawnChance in _spawnChances)
        {
            spawnChance.SpawnChance.Initialize();
        }
    }

    /// <summary>
    /// Add spawnChance in all chances sorted by chance probability
    /// </summary>
    /// <param name="spawnChance"></param>
    public void Add(ObjectChanceSpawn<T> spawnChance)
    {
        spawnChance.SpawnChance.Initialize();

        if (_spawnChances.Count == 0) 
        {
            _spawnChances.Add(spawnChance);

            return;
        }

        for (int i = 0; i < _spawnChances.Count; i++)
        {
            if (_spawnChances[i].SpawnChance.Probability > spawnChance.SpawnChance.Probability)
            {
                if (i != _spawnChances.Count - 1 && _spawnChances[i + 1].SpawnChance.Probability <= spawnChance.SpawnChance.Probability) 
                {
                    _spawnChances.Insert(i + 1, spawnChance);
                    return;
                }
                else if (i == _spawnChances.Count - 1)
                {
                    _spawnChances.Add(spawnChance);
                    return;
                }
            }
            else if (_spawnChances[i].SpawnChance.Probability <= spawnChance.SpawnChance.Probability)
            {
                if (i > 0)
                {
                    _spawnChances.Insert(i - 1, spawnChance);
                    return;
                }
                else
                {
                    _spawnChances.Insert(0, spawnChance);
                    return;
                }
            }
        }
    }

    public bool Remove(ObjectChanceSpawn<T> spawnChance) => _spawnChances.Remove(spawnChance);

    /// <summary>
    /// Calculate chances and find striked object
    /// </summary>
    /// <returns>Returns first first object with TrueChance or last striked SpawnChance</returns>
    public T GetStrikedObject()
    {
        if (_spawnChances == null || _spawnChances.Count == 0) return null;

        T strikedObject = _spawnChances[0].Object;

        bool isStrike = false;

        while(!isStrike)
        {
            foreach (ObjectChanceSpawn<T> spawnChance in _spawnChances)
            {
                if (spawnChance.ChanceIsTrue) return spawnChance.Object;

                if (spawnChance.SpawnChance.IsStrike)
                {
                    isStrike = true;

                    strikedObject = spawnChance.Object;
                }
            }
        }

        return strikedObject;
    }
}
