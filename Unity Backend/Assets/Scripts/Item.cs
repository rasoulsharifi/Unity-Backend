using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI itemDescription;
    public Button sellBtn;


    public void SetItemInfo(string name, string price, string description)
    {
        itemName.text = name;
        itemPrice.text = price;
        itemDescription.text = description;
    }

    public void SetSellBtnAction<T>(UnityEngine.Events.UnityAction action)
    {
        sellBtn.onClick.AddListener(action);
    }

}
