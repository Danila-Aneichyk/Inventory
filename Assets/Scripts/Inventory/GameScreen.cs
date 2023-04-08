using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Button _addItemButton;
    [SerializeField] private Button _deleteItemButton;
    [SerializeField] private Button _addAmmoButton;
    [SerializeField] private Button _shootAmmoButton;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        AddRandomItem();
        _deleteItemButton.onClick.AddListener(_inventory.DeleteItems);
        _addAmmoButton.onClick.AddListener(_inventory.AddAmmo);
        _shootAmmoButton.onClick.AddListener(_inventory.ShootAmmo);
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