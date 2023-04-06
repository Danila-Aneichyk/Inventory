using UnityEngine;

[CreateAssetMenu(fileName = "Hat", menuName = "Inventory/Items/Hats")]
public class Hat : ItemParameters
{
    [SerializeField] private float _protection; 
}