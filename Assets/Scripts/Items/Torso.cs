using UnityEngine;

[CreateAssetMenu(fileName = "Torso", menuName = "Inventory/Items/Outerwear")]
public class Torso : Item
{
    [SerializeField] private float _protection;
}