using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
        public ItemParameters ItemParameters;
        public int Amount;
        public bool IsEmpty;
        public TMP_Text TextAmount; 
        public GameObject _icon;

        private void Awake()
        {
                _icon = transform.GetChild(0).GetChild(0).gameObject;
                TextAmount = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        }

        public void SetIcon(Sprite icon)
        {
                _icon.GetComponent<Image>().color = new Color(1,1,1,1);
                _icon.GetComponent<Image>().sprite = icon;
        }
}