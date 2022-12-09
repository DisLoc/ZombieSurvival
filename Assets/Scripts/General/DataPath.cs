using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataPath
{
    private static readonly string _defaultPath = Application.persistentDataPath;

    public static readonly string PlayerLevel = _defaultPath + "/PlayerLevel.dat";

    public static readonly string EquipmentInventory = _defaultPath + "/EquipmentInventory.dat";
    public static readonly string MaterialsInventory = _defaultPath + "/MaterialsInventory.dat";

    public static readonly string CoinsInventory = _defaultPath + "/CoinsInventory.dat";
    public static readonly string GemsInvneotry = _defaultPath + "/GemsInventory.dat";
    public static readonly string EnergyInventory = _defaultPath + "/EnergyInventory.dat";

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
