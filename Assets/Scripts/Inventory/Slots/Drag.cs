using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Slot _oldSlot;

    //private ScrollRect _scrollRect;

    private void Awake()
    {
        //_scrollRect = FindObjectOfType<ScrollRect>();
        _oldSlot = transform.GetComponentInParent<Slot>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;

        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;

        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;

        //Поставить DraggableObject обратно в свой старый слот
        transform.SetParent(_oldSlot.transform);
        transform.position = _oldSlot.transform.position;

        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>() != null)
        {
            //Перемещаем данные из одного слота в другой
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>());
        }

        if (eventData.pointerCurrentRaycast.gameObject.name == "Inventory") 
        {
            return;
        }
        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent == null)
        {
            return;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_oldSlot.IsEmpty)
            return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    private void ExchangeSlotData(Slot newSlot)
    {
        // Временно храним данные newSlot в отдельных переменных
        ItemParameters item = newSlot.ItemParameters;
        int amount = newSlot.Amount;
        bool isEmpty = newSlot.IsEmpty;
        GameObject icon = newSlot.Icon;
        TMP_Text textAmount = newSlot.TextAmount;

        // Заменяем значения newSlot на значения oldSlot
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

        // Заменяем значения oldSlot на значения newSlot сохраненные в переменных
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
}