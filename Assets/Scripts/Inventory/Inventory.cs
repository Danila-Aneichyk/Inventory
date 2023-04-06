using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsParentObject;
    [SerializeField] private List<Slot> _slots = new List<Slot>();

    private void Start()
    {
        GetAllSlots();
    }

    private void GetAllSlots()
    {
        for (int i = 0; i < _slotsParentObject.childCount; i++)
        {
            if (_slotsParentObject.GetChild(i).GetComponent<Slot>() != null)
            {
                _slots.Add(_slotsParentObject.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    public void AddItem(ItemParameters itemParameters, int amount)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.ItemParameters == itemParameters)
            {
                slot.Amount += amount;
                Debug.Log("Item added in stack ");
                return;
            }
            else if (slot.IsEmpty == false)
            {
                slot.ItemParameters = itemParameters;
                slot.Amount = amount;
                slot.IsEmpty = true;
                Debug.Log("Item added in empty slot");
            }
        }
    }
}