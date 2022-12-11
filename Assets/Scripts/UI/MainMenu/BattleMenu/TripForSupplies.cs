using System;
using UnityEngine;
using Zenject;

public class TripForSupplies : MonoBehaviour
{
    [Tooltip("Limit in seconds")]
    [SerializeField] private int _tripTimeLimit;

    [SerializeField] private TripRewards _tripRewards;

    [Inject] private MainInventory _mainInventory;

    public void OnTripClick()
    {
        var reward = _tripRewards.GetLevelRewards((int)_mainInventory.PlayerLevel.Value);
    }

    public SerializableData SaveData()
    {
        TripData data = new TripData();

        data.time = DateTime.Now;

        return data;
    }

    public void LoadData(SerializableData data)
    {
        if (data is TripData loadedData)
        {

        }
    }

    public void ResetData()
    {

    }

    [Serializable]
    public class TripData : SerializableData
    {
        public DateTime time;
    }
}
