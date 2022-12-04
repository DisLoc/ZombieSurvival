public abstract class Inventory
{
    public abstract SerializableData SaveData();

    public abstract void LoadData(SerializableData data);

    public abstract void ResetData();
}
