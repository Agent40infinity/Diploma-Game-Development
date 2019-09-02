using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject main, create, forget;
    public GameObject forgotMain, forgotCode;

    public void ForgotPassword()
    {
        main.SetActive(false);
        forget.SetActive(true);
    }

    public void CreateAccount()
    {
        main.SetActive(false);
        create.SetActive(true);
    }

    public void Back()
    {
        create.SetActive(false);
        forget.SetActive(false);
        main.SetActive(true);
    }

    public void EnterCode(bool isMain)
    {
        if (isMain)
        {
            forgotMain.SetActive(false);
            forgotCode.SetActive(true);
        }
        else
        {
            forgotMain.SetActive(true);
            forgotCode.SetActive(false);
        }
    }
}
