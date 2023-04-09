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

    public void LoadItemToSlot(ItemParameters itemParameters, int amount, int slotId)
    {
        Slot slot = _slots[slotId];
        slot.ItemParameters = itemParameters;
        slot.ItemType = itemParameters.ItemType.ToString();
        slot.IsEmpty = false;
        slot.SetIcon(itemParameters.Icon);

        if (amount <= itemParameters._maximumAmount)
        {
            slot.Amount = amount;
            if (slot.ItemParameters._maximumAmount != 1)
            {
                slot.TextAmount.text = slot.Amount.ToString();
            }
            else
            {
                slot.Amount = itemParameters._maximumAmount;
                amount = itemParameters._maximumAmount;
                if (slot.ItemParameters._maximumAmount != 1)
                {
                    slot.TextAmount.text = slot.Amount.ToString();
                }
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
                    slot.ItemType = itemParameters.ItemType.ToString();
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
                    slot.ItemType = itemParameters.ItemType.ToString();
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

    public void DeleteItems()
    {
        List<Slot> filledSlots = new List<Slot>();
        foreach (Slot slot in _slots)
        {
            if (!slot.IsEmpty)
            {
                filledSlots.Add(slot);
            }
        }

        if (filledSlots.Count == 0)
        {
            Debug.LogError("All slots are EMPTY!");
            return;
        }

        Slot randomSlot = filledSlots[Random.Range(0, filledSlots.Count)];
        ClearSlotData(randomSlot);
    }

    public void ClearSlotData(Slot slot)
    {
        slot.IsEmpty = true;
        slot.ItemParameters = null;
        slot.Amount = 0;
        slot.ItemType = null;
        if (slot != null)
        {
            slot.Icon.GetComponent<Image>().sprite = null;
            slot.TextAmount.text = "";
        }
    }

    public void AddAmmo()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.ItemParameters != null)
            {
                if (slot.ItemType == ItemType.Ammo.ToString() && slot.Amount != slot.ItemParameters._maximumAmount)
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
        List<Slot> ammoSlots = new List<Slot>();
        foreach (Slot slot in _slots)
        {
            if (slot.ItemType == ItemType.Ammo.ToString())
            {
                ammoSlots.Add(slot);
            }
        }

        if (ammoSlots.Count == 0)
        {
            Debug.Log("No ammo in inventory");
            return;
        }

        Slot randomAmmoSlot = ammoSlots[Random.Range(0, ammoSlots.Count)];
        randomAmmoSlot.Amount--;

        if (randomAmmoSlot.Amount == 0)
        {
            ClearSlotData(randomAmmoSlot);
        }

        randomAmmoSlot.TextAmount.text = randomAmmoSlot.Amount.ToString();
        if (randomAmmoSlot.Amount < 1)
        {
            randomAmmoSlot.TextAmount.text = "";
        }
    }
}