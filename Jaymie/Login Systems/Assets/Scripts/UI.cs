using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject Main, Create, Forget;

    public void ForgotPassword()
    {
        Main.SetActive(false);
        Forget.SetActive(true);
    }

    public void CreateAccount()
    {
        Main.SetActive(false);
        Create.SetActive(true);
    }

    public void Back()
    {
        Create.SetActive(false);
        Forget.SetActive(false);
        Main.SetActive(true);
    }
}
