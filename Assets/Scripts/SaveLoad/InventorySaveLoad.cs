using UnityEngine;

public class InventorySaveLoad : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public void SaveInventory()
    {
        BinarySavingSystem.SaveInventory(_inventory);
    }

    public void LoadInventory()
    {
        InventoryData data = BinarySavingSystem.LoadInventory();
        
        for (int i = 0; i < _inventory._slots.Count; i++)
        {
            if (data.itemNames[i] != null)
            {
                _inventory.ClearSlotData(_inventory._slots[i]);
                ItemParameters item = Resources.Load<ItemParameters>($"Configs/{data.itemNames[i]}");
                int itemAmount = data.itemAmounts[i];
                _inventory.AddItem(item,itemAmount);
            }
            else
            {
                _inventory.ClearSlotData(_inventory._slots[i]);
            }
        }
    }
}