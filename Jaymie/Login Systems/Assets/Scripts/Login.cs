using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Login : MonoBehaviour
{
    #region Variables
    //Create New User:
    public InputField usernameInput;
    public InputField emailInput;
    public InputField passwordInput;

    //Login:
    public InputField usernameInputLogin;
    public InputField passwordInputLogin;

    //Forgot Password:
    public InputField emailInputForgot;
    public GameObject forgotContent;
    public GameObject checkCode;
    public GameObject newPassword;
    public InputField codeInputForgot;

    //Notification:
    public Text notification;
    public GameObject notificationParent;

    //Code Management:
    private string characters = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string code = "";
    private string savedCode = "temp";
    public string _username;
    #endregion

    #region Create User
    IEnumerator CreateUser(string username, string email, string password)
    {
        string createUserURL = "http://localhost/nsirpg/InsertUser.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);
        UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest);
        StartCoroutine("Notification");
        notification.text = "Account Created";
    }

    public void CreateNewUser()
    {
        StartCoroutine(CreateUser(usernameInput.text, emailInput.text, passwordInput.text));
    }
    #endregion

    #region Login
    IEnumerator UserLogin(string username, string password)
    {
        string createUserURL = "http://localhost/nsirpg/UserLogin.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        if (webRequest.downloadHandler.text == "Logged In")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine("Notification");
            notification.text = webRequest.downloadHandler.text;
        }
    }

    public void LoginUser()
    {
        StartCoroutine(UserLogin(usernameInputLogin.text, passwordInputLogin.text));
    }
    #endregion

    #region forgot User
    IEnumerator ForgotUser(string email)
    {
        string forgotURL = "http://localhost/nsirpg/checkemail.php";
        WWWForm form = new WWWForm();
        form.AddField("email_Post", email);
        UnityWebRequest webRequest = UnityWebRequest.Post(forgotURL, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        if (webRequest.downloadHandler.text == "User Not Found")
        {
            StartCoroutine("Notification");
            notification.text = webRequest.downloadHandler.text;
        }
        else
        {
            _username = webRequest.downloadHandler.text;
            SendEmail(email);
        }
    }

    public void SubmitForgotUser()
    {
        StartCoroutine(ForgotUser(emailInputForgot.text));
    }
    #endregion

    #region Send Email
    void SendEmail(string _email)
    {
        CreateCode();
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(_email);
        mail.Subject = "NSIRPG Password Reset";
        mail.Body = "Hello " + _username + "\nReset using this code: " + code;

        //connect to google
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        //be able to send through ports
        smtpServer.Port = 25; //80 25 
        //login to google
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        { return true; };
        //send message
        smtpServer.Send(mail);
        Debug.Log("Sending Email");
        StartCoroutine("Notification");
        notification.text = "Email Sent";
    }

    void CreateCode()
    {
        for (int i = 0; i < 20; i++)
        {
            int a = UnityEngine.Random.Range(0, characters.Length);
            code = code + characters[a];
            savedCode = code;
        }
        Debug.Log(code);
    }
    #endregion

    #region Reset Password
    public IEnumerator CheckCode(string _code)
    {
        _code = codeInputForgot.text;
        if (savedCode == _code)
        {
            newPassword.SetActive(true);
            checkCode.SetActive(false);
        }
        else
        {
            StartCoroutine("Notification");
            notification.text = "Incorrect Code";
        }
        yield return null;
    }

    public void SubmitCodeForReset()
    {
        StartCoroutine(CheckCode(codeInputForgot.text));
    }

    public IEnumerator PasswordReset(string newPassword, string confirmPassword)
    {
        if (newPassword == confirmPassword)
        {
            string passwordResetURL = "http://localhost/nsirpg/UpdatePassword.php";
            WWWForm form = new WWWForm();
            form.AddField("password_Post", newPassword);
            form.AddField("username_Post", _username);

            UnityWebRequest webRequest = UnityWebRequest.Post(passwordResetURL, form);
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);
            //if (webRequest.downloadHandler.text == "User Not Found")
            //{
            //    StartCoroutine("Notification");
            //    notification.text = webRequest.downloadHandler.text;
            //}
         
        }
        else
        {
            StartCoroutine("Notification");
            notification.text = "Passwords do not Match";
        }
        yield return null;
    }

    public void SubmitPasswordReset()
    {
        StartCoroutine(PasswordReset(null, null));
    }
    #endregion

    #region Notification
    public IEnumerator Notification()
    {
        notificationParent.SetActive(true);
        yield return new WaitForSeconds(2f);
        notificationParent.SetActive(false);
    }
    #endregion
}
