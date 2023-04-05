using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private int _maximumAmount;
    [SerializeField] private float _weight;
    [SerializeField] private Image _icon;
    [SerializeField] private ItemType _itemType;

}
