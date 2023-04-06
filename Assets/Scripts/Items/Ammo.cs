using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Items/Projectile")]
public class Ammo : ItemParameters
{
    [SerializeField] private ProjectileType _projectileType;
}