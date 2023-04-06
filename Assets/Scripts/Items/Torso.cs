using UnityEngine;

[CreateAssetMenu(fileName = "Torso", menuName = "Inventory/Items/Outerwear")]
public class Torso : ItemParameters
{
    [SerializeField] private float _protection;
}