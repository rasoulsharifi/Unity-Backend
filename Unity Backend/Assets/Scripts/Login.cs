using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button loginButton;    
    

    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.web.Login(usernameInput.text, passwordInput.text));
        });
    }

    
}
