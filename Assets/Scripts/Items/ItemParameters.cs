using UnityEngine;
using UnityEngine.UI;

public class ItemParameters : ScriptableObject
{
    public Sprite Icon;
    [SerializeField] private string _itemName;
    [SerializeField] private int _maximumAmount;
    [SerializeField] private float _weight;
    [SerializeField] private ItemType _itemType;

}
