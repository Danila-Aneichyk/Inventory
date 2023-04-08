using UnityEngine;

[System.Serializable]
public class InventoryData
{
    [Header(("Inventory Data"))]
    public string[] itemNames;
    public int[] itemAmounts;

    public InventoryData(Inventory inventory)
    {
        itemNames = new string[inventory._slots.Count];
        itemAmounts = new int[inventory._slots.Count];

        for (int i = 0; i < inventory._slots.Count; i++)
        {
            if (inventory._slots[i].ItemParameters != null)
                itemNames[i] = inventory._slots[i].ItemParameters.name;
            if (inventory._slots[i].ItemParameters != null)
                itemAmounts[i] = inventory._slots[i].Amount;
        }
    }
}