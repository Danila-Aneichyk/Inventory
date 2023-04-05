using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Items/Weapons")]
public class Weapon : Item
{
        [SerializeField] private float _damage;
        [SerializeField] private ProjectileType _projectileType;

}