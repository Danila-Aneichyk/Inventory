using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Items/Projectile")]
public class Ammo : Item
{
    [SerializeField] private ProjectileType _projectileType;
}