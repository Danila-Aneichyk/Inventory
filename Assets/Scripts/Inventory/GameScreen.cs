using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Button _addItemButton;
    [SerializeField] private Button _deleteItemButton;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        AddRandomItem();
        _deleteItemButton.onClick.AddListener(DeleteRandomItemInSlot);
    }

    private void AddRandomItem()
    {
        Item item = _items[Random.Range(0, _items.Length)];
        _addItemButton.onClick.AddListener(() =>
        {
            _inventory.AddItem(item.ItemParameters, item.Amount);
            item = _items[Random.Range(0, _items.Length)];
        });
    }

    private void DeleteRandomItemInSlot()
    {
        Slot slot = _inventory._slots[Random.Range(0, _inventory._slots.Count)];
        
        if (slot.IsEmpty)
        {
            Debug.LogError("You try to delete empty slot");
        }
        else
        {
            Debug.Log("Slot is deleted");
        }
        
        if (_inventory._slots.Count > 0)
        {
            _inventory.DeleteItems(slot);
        }
    }
}