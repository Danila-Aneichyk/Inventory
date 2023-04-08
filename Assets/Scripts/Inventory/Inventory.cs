using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsParentObject;
    public List<Slot> _slots = new List<Slot>();

    private void Awake()
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
                    slot.ItemType = itemParameters.ItemType;
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
                    slot.ItemType = itemParameters.ItemType;
                    int amountToAdd = Mathf.Min(itemParameters._maximumAmount, amount);
                    slot.Amount = amountToAdd;
                    slot.IsEmpty = false;
                    slot.SetIcon(itemParameters.Icon);
                    if (slot.ItemParameters._maximumAmount != 1)
                    {
                        slot.TextAmount.text = amountToAdd.ToString();
                    }

                    amount -= amountToAdd;
                }

                if (amount == 0)
                {
                    break;
                }
            }
        }
    }

    public void DeleteItems(Slot itemInSlotToDestroy)
    {
        itemInSlotToDestroy.IsEmpty = true;
        itemInSlotToDestroy.ItemParameters = null;
        itemInSlotToDestroy.Amount = 0;
        if (itemInSlotToDestroy != null)
        {
            itemInSlotToDestroy.Icon.GetComponent<Image>().sprite = null;
            itemInSlotToDestroy.TextAmount.text = "";
        }
    }

    public void AddAmmo()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.ItemParameters != null)
            {
                if (slot.ItemType == ItemType.Ammo && slot.Amount != slot.ItemParameters._maximumAmount)
                {
                    int amountToAdd = slot.ItemParameters._maximumAmount - slot.Amount;
                    slot.Amount += amountToAdd;
                    slot.TextAmount.text = slot.Amount.ToString();
                    break;
                }
            }
        }
    }
    
    public void ShootAmmo()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.ItemType == ItemType.Ammo && slot.Amount > 0)
            {
                slot.Amount--;
                slot.TextAmount.text = slot.Amount.ToString();

                if (slot.Amount == 0)
                {
                    DeleteItems(slot);
                }

                break;
            }
        }
    }
}