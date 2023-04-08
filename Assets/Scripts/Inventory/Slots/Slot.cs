using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
        public ItemParameters ItemParameters;
        public string ItemType;
        public int Amount;
        public bool IsEmpty;
        public TMP_Text TextAmount; 
        public GameObject Icon;

        private void Awake()
        { 
                
                Icon = transform.GetChild(0).GetChild(0).gameObject;
                TextAmount = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        }

        public void SetIcon(Sprite icon)
        {
                Icon.GetComponent<Image>().color = new Color(1,1,1,1);
                Icon.GetComponent<Image>().sprite = icon;
        }
}