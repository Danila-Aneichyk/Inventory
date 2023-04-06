using UnityEngine;
using UnityEngine.UI;

public class ItemParameters : ScriptableObject
{
    public Sprite Icon;
    public int _maximumAmount;
    [SerializeField] private string _itemName;
    [SerializeField] private float _weight;
    [SerializeField] private ItemType _itemType;

}
