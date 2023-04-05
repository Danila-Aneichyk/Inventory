using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "Inventory/Items/Hats")]
public class Hat : Item
{
    [SerializeField] private float _protection; 
}