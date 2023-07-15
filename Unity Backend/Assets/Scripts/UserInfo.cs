using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userID { get; private set; }
    string username;
    string userPassword;
    string level;
    string coins;

    public void SetCredentials(string username, string userPassword)
    {
        this.username = username;
        this.userPassword = userPassword;
    }

    public void SetID(string userID)
    {
        this.userID = userID;
    }

}
