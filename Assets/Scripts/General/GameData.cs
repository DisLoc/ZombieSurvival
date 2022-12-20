using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameData
{
    #region Scene IDs
    public static readonly int LoadingScene = 0;
    public static readonly int MainMenuScene = 1;
    public static readonly int MainGameScene = 2;
    #endregion

    #region Serialization
    public static readonly string DefaultPath = Application.persistentDataPath + "/";

    public static readonly string PlayerLevel = DefaultPath + "PlayerLevelTest.dat";

    public static readonly string EquipmentInventory = DefaultPath + "EquipmentTest.dat";
    public static readonly string MaterialsInventory = DefaultPath + "MaterialsTest.dat";

    public static readonly string CampInventory = DefaultPath + "CampTest.dat";

    public static readonly string CoinsInventory = DefaultPath + "CoinsTest.dat";
    public static readonly string GemsInvneotry = DefaultPath + "GemsTest.dat";
    public static readonly string KeysInventory = DefaultPath + "KeysTest.dat";
    public static readonly string EnergyInventory = DefaultPath + "EnergyTest.dat";

    public static readonly string Supplies = DefaultPath + "SuppliesTest.dat";
    public static readonly string SpecialGift = DefaultPath + "SpecialGiftTest.dat";

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
    #endregion
}
