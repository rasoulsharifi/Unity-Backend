using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Web web;
    public UserInfo userInfo;
    public GameObject Login;
    public GameObject UserProfile;

    public static Main Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        
    }

}
