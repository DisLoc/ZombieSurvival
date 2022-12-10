using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataPath
{
    public static readonly string DefaultPath = Application.persistentDataPath + "/";

    public static readonly string PlayerLevel = DefaultPath + "PlayerLevel.dat";

    public static readonly string EquipmentInventory = DefaultPath + "EquipmentInventory.dat";
    public static readonly string MaterialsInventory = DefaultPath + "MaterialsInventory.dat";

    public static readonly string CoinsInventory = DefaultPath + "CoinsInventory.dat";
    public static readonly string GemsInvneotry = DefaultPath + "GemsInventory.dat";
    public static readonly string KeysInventory = DefaultPath + "KeysInventory.dat";
    public static readonly string EnergyInventory = DefaultPath + "EnergyInventory.dat";

    public static readonly string Supplies = DefaultPath + "Supplies.dat";
    public static readonly string SpecialGift = DefaultPath + "SpecialGift.dat";

    public static void Save(string path, SerializableData data)
    {
        if (data == null) return;

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        bf.Serialize(file, data);
        file.Close();

        //Debug.Log("Data saved to " + path);
    }

    public static SerializableData Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            SerializableData data = (SerializableData)bf.Deserialize(file);

            file.Close();

            //Debug.Log("Loaded data from " + path + ". " + data);

            return data;
        }
        else
        {
            //Debug.Log("No data to load from " + path);

            return null;
        }
    }
}
