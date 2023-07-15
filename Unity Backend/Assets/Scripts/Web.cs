using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Web : MonoBehaviour
{




    void Start()
    {
        StartCoroutine(GetData("http://localhost/UnityBackendTutorial/GetData.php"));
        StartCoroutine(GetData("http://localhost/UnityBackendTutorial/GetUsers.php"));
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


            //string[] pages = uri.Split('/');
            //int page = pages.Length - 1;

            //switch (www.result)
            //{
            //    case UnityWebRequest.Result.ConnectionError:
            //    case UnityWebRequest.Result.DataProcessingError:
            //        Debug.LogError(pages[page] + ": Error: " + www.error);
            //        break;
            //    case UnityWebRequest.Result.ProtocolError:
            //        Debug.LogError(pages[page] + ": HTTP Error: " + www.error);
            //        break;
            //    case UnityWebRequest.Result.Success:
            //        Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
            //        break;
            //}
        }
    }



}
