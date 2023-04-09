using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Items/Ammo")]
public class Ammo : ItemParameters
{
    [SerializeField] private AmmoType _ammoType;
}