using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
        [SerializeField] private Transform _slotsParentObject;
        [SerializeField] private List<Slot> _slots = new List<Slot>();

        private void Start()
        {
                for (int i = 0; i < _slotsParentObject.childCount; i++)
                {
                        if (_slotsParentObject.GetChild(i).GetComponent<Slot>() != null)
                        {
                                _slots.Add(_slotsParentObject.GetChild(i).GetComponent<Slot>());
                        }
                }
        }
}