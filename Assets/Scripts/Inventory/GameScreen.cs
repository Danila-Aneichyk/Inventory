using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Button _addItemButton;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        AddRandomItem();
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
}