using UnityEngine;

public static class DataPath
{
    private static readonly string _defaultPath = Application.persistentDataPath;

    public static readonly string PlayerLevel = _defaultPath + "/PlayerLevel.dat";

    public static readonly string EquipmentInventory = _defaultPath + "/EquipmentInventory.dat";

    public static readonly string CoinsInventory = _defaultPath + "/CoinsInventory.dat";
    public static readonly string GemsInvneotry = _defaultPath + "/GemsInventory.dat";
    public static readonly string EnergyInventory = _defaultPath + "/EnergyInventory.dat";
}
