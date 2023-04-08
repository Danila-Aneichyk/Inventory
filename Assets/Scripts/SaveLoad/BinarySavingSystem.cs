using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinarySavingSystem
{
        public static void SaveInventory(Inventory inventory)
        {
                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.persistentDataPath + "/Inventory.b";
                FileStream stream = new FileStream(path, FileMode.Create);

                InventoryData data = new InventoryData(inventory);
        
                formatter.Serialize(stream, data);
                stream.Close();
        }

        public static InventoryData LoadInventory()
        {
                string path = Application.persistentDataPath + "/Inventory.b";
                if (File.Exists(path))
                {
                        BinaryFormatter formatter = new BinaryFormatter();
                        FileStream stream = new FileStream(path, FileMode.Open);

                        InventoryData data = formatter.Deserialize(stream) as InventoryData;
                        stream.Close();
            
                        return data;
                }
                else
                {
                        Debug.LogError("Save file not found in " + path);
                        return null;
                }
        }
}