using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class DataToSend : MonoBehaviour
{
    //General:
    public string username = "";
    public string charName = "";

    //Cosmetics:
    public int skinIndex;
    public int hairIndex;
    public int eyesIndex;
    public int mouthIndex;
    public int clothesIndex;
    public int armourIndex;

    //Stats:
    public int strength;
    public int dexterity;
    public int contitution;
    public int wisdom;
    public int intelligence;
    public int charisma;

    //Reference:
    public CustomisationSet customizer;

    public void SaveData()
    {
        username = InventoryCanvas.loggedInUsername;
        charName = CustomisationSet.characterName;

        skinIndex = customizer.skinIndex;
        hairIndex = customizer.hairIndex;
        eyesIndex = customizer.eyesIndex;
        mouthIndex = customizer.mouthIndex;
        clothesIndex = customizer.clothesIndex;
        armourIndex = customizer.armourIndex;

        strength = customizer.stats[0];
        dexterity = customizer.stats[1];
        contitution = customizer.stats[2];
        wisdom = customizer.stats[3];
        intelligence = customizer.stats[4];
        charisma = customizer.stats[5];
    }

    public IEnumerator SendData() //Used to create a new user
    {
        SaveData();
        string createUserURL = "http://localhost/nsirpg/InsertUser.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("charName", charName);
        form.AddField("skinIndex", skinIndex);
        form.AddField("hairIndex", hairIndex);
        form.AddField("eyesIndex", eyesIndex);
        form.AddField("mouthIndex", mouthIndex);
        form.AddField("clothesIndex", clothesIndex);
        form.AddField("armourIndex", armourIndex);
        form.AddField("strength", strength);
        form.AddField("dexterity", dexterity);
        form.AddField("constitution", contitution);
        form.AddField("wisdom", wisdom);
        form.AddField("intelligence", intelligence);
        form.AddField("charisma", charisma);
        UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest);
    }

    public IEnumerator RetrieveData() //Used to create a new user
    {
        SaveData();
        string createUserURL = "http://localhost/nsirpg/InsertUser.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("charName", charName);
        form.AddField("skinIndex", skinIndex);
        form.AddField("hairIndex", hairIndex);
        form.AddField("eyesIndex", eyesIndex);
        form.AddField("mouthIndex", mouthIndex);
        form.AddField("clothesIndex", clothesIndex);
        form.AddField("armourIndex", armourIndex);
        form.AddField("strength", strength);
        form.AddField("dexterity", dexterity);
        form.AddField("constitution", contitution);
        form.AddField("wisdom", wisdom);
        form.AddField("intelligence", intelligence);
        form.AddField("charisma", charisma);
        UnityWebRequest webRequest = UnityWebRequest.Post(createUserURL, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest);
    }
}
