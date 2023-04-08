using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Slot _oldSlot;

    private void Awake()
    {
        _oldSlot = transform.GetComponentInParent<Slot>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;

        MakeImageSemiVisible();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;

        MakeImageVisible();
        BackItemToTheOldSlot();
        MoveItemInNewSlot(eventData);
        CheckInventory(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;
        DragItem(eventData);
    }

    private void MoveItemInNewSlot(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>() != null)
        {
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>());
        }
    }

    private void DragItem(PointerEventData eventData)
    {
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    private void ExchangeSlotData(Slot newSlot)
    {
        ItemParameters item = ChangeOldSlotToNewSlot(newSlot, out int amount, out bool isEmpty);
        ChangeNewToOldSlot(item, amount, isEmpty);
    }

    private void MakeImageVisible()
    {
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;
    }

    private void MakeImageSemiVisible()
    {
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    private void CheckInventory(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "Inventory") 
            return;
    }

    private void BackItemToTheOldSlot()
    {
        Transform cachedTransform;
        (cachedTransform = transform).SetParent(_oldSlot.transform);
        cachedTransform.position = _oldSlot.transform.position;
    }

    private void ChangeNewToOldSlot(ItemParameters item, int amount, bool isEmpty)
    {
        _oldSlot.ItemParameters = item;
        _oldSlot.Amount = amount;
        if (isEmpty == false)
        {
            _oldSlot.SetIcon(item.Icon);
            if (item._maximumAmount != 1) // added this if statement for single items
            {
                _oldSlot.TextAmount.text = amount.ToString();
            }
            else
            {
                _oldSlot.TextAmount.text = "";
            }
        }
        else
        {
            _oldSlot.Icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _oldSlot.Icon.GetComponent<Image>().sprite = null;
            _oldSlot.TextAmount.text = "";
        }

        _oldSlot.IsEmpty = isEmpty;
    }

    private ItemParameters ChangeOldSlotToNewSlot(Slot newSlot, out int amount, out bool isEmpty)
    {
        ItemParameters item = newSlot.ItemParameters;
        amount = newSlot.Amount;
        isEmpty = newSlot.IsEmpty;
        GameObject icon = newSlot.Icon;
        TMP_Text textAmount = newSlot.TextAmount;


        newSlot.ItemParameters = _oldSlot.ItemParameters;
        newSlot.Amount = _oldSlot.Amount;
        if (_oldSlot.IsEmpty == false)
        {
            newSlot.SetIcon(_oldSlot.Icon.GetComponent<Image>().sprite);
            if (_oldSlot.ItemParameters._maximumAmount != 1) // added this if statement for single items
            {
                newSlot.TextAmount.text = _oldSlot.Amount.ToString();
            }
            else
            {
                newSlot.TextAmount.text = "";
            }
        }
        else
        {
            newSlot.Icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.Icon.GetComponent<Image>().sprite = null;
            newSlot.TextAmount.text = "";
        }

        newSlot.IsEmpty = _oldSlot.IsEmpty;
        return item;
    }
}