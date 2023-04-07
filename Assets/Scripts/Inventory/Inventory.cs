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
        bool addedToStack = false;
        foreach (Slot slot in _slots)
        {
            if (slot.ItemParameters == itemParameters)
            {
                if (slot.Amount < itemParameters._maximumAmount)
                {
                    int amountToAdd = Mathf.Min(itemParameters._maximumAmount - slot.Amount, amount);
                    slot.Amount += amountToAdd;
                    slot.TextAmount.text = slot.Amount.ToString();
                    Debug.Log("Added " + amountToAdd + " items to an existing stack");
                    amount -= amountToAdd;
                    addedToStack = true;
                }
            }


            if (amount == 0)
            {
                break;
            }
        }

        if (amount > 0 && !addedToStack)
        {
            foreach (Slot slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    slot.ItemParameters = itemParameters;
                    int amountToAdd = Mathf.Min(itemParameters._maximumAmount, amount);
                    slot.Amount = amountToAdd;
                    slot.IsEmpty = false;
                    slot.SetIcon(itemParameters.Icon);
                    if (slot.ItemParameters._maximumAmount != 1)
                    {
                        slot.TextAmount.text = amountToAdd.ToString();
                    }

                    Debug.Log("Added " + amountToAdd + " items to an empty slot");
                    amount -= amountToAdd;
                }

                if (amount == 0)
                {
                    break;
                }
            }
        }
    }
}