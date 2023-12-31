using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Web : MonoBehaviour
{

    void Start()
    {
        //StartCoroutine(GetData("http://localhost/UnityBackendTutorial/GetData.php"));
        //StartCoroutine(GetData("http://localhost/UnityBackendTutorial/GetUsers.php"));
        //StartCoroutine(Login("rasoul", "1234"));
       // StartCoroutine(RegisterUser("navid", "3499"));
    }

    //System.Action<string> _createItemsCallback;
    public void ShowUserItems()
    {
        //StartCoroutine(GetItemsIDs(Main.Instance.userInfo.userID, _createItemsCallback));
    }

    IEnumerator GetData(string uri)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.
            Post("http://localhost/UnityBackendTutorial/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);

                Main.Instance.userInfo.SetCredentials(username, password);
                Main.Instance.userInfo.SetID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("Wrong Credentials")
                    || www.downloadHandler.text.Contains("Username does not exists"))
                {

                }
                else
                {   
                    // if we logged in correctly
                    Main.Instance.UserProfile.SetActive(true);
                    Main.Instance.Login.SetActive(false);



                }
            }
        }
    }

    IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.
            Post("http://localhost/UnityBackendTutorial/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItemsIDs.php", form))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                // call callback function to pass results
                callback(jsonArray);
            }
        }
    }


    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItem.php", form))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // call callback function to pass results
                callback(jsonArray);
            }
        }
    }

    public IEnumerator SellItem(string itemID, string userId, string userItemID)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID", userId);
        form.AddField("userItemID", userItemID);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/SellItem.php", form))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                // call callback function to pass results
                //callback(jsonArray);
            }
        }
    }

}
