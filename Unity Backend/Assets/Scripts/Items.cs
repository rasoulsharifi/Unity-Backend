using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;

public class Items : MonoBehaviour
{
    [SerializeField] Item itemPrefab;
    [SerializeField] Transform itemsHolder;
    Action<string> _createItemsCallback;

    void Start()
    {
        _createItemsCallback = (jsonArray) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArray));
        };

        CreateItems();

    }

    public void CreateItems()
    {
        string userId = Main.Instance.userInfo.userID;
        StartCoroutine(Main.Instance.web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArraString)
    {            
        JSONArray jsonArray = JSON.Parse(jsonArraString) as JSONArray;        

        for (int i = 0; i < jsonArray.Count; i++)
        {
            // create local variables
            bool isDone = false;
            string itemID = jsonArray[i].AsObject["itemID"];
            string userItemsID = jsonArray[i].AsObject["ID"];            

            JSONObject itemInfoJson = new JSONObject();
            // create a callback to get the information from Web.cs
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArr = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArr[0].AsObject;
            };

            // wait until Web.cs calls the callback we passed as parameter 
            StartCoroutine(Main.Instance.web.GetItem(itemID, getItemInfoCallback));

            // wait until the callback is called from web (info finished downloading)
            yield return new WaitUntil(() => isDone == true);

            // Instantiate GameObject (item prefab)
            Item item = Instantiate(itemPrefab);
            item.transform.SetParent(itemsHolder);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            // Fill Info
            //item.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            //item.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            //item.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];
            item.SetItemInfo(itemInfoJson["price"], itemInfoJson["name"], itemInfoJson["description"]);

            // set sell button
            item.sellBtn.onClick.AddListener(() => {
                string iId = itemID;
                string uId = Main.Instance.userInfo.userID;
                StartCoroutine(Main.Instance.web.SellItem(iId, uId, userItemsID));
            });

            // continue to next item
        }

        yield return null;
    }

}
