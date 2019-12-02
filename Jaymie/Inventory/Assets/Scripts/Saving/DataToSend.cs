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
    public int classIndex;
    public int points;
    public int strength;
    public int dexterity;
    public int contitution;
    public int wisdom;
    public int intelligence;
    public int charisma;

    //Reference:
    public CustomisationSet customizer;
    public CustomisationGet getcustomizer;

    public void SaveData()
    {
        username = InventoryCanvas.loggedInUsername;
        charName = CustomisationSet.tempCharacterName;
        CustomisationSet.characterName = CustomisationSet.tempCharacterName;

        skinIndex = customizer.skinIndex;
        hairIndex = customizer.hairIndex;
        eyesIndex = customizer.eyesIndex;
        mouthIndex = customizer.mouthIndex;
        clothesIndex = customizer.clothesIndex;
        armourIndex = customizer.armourIndex;

        classIndex = customizer.selectedIndex;
        points = customizer.points;
        strength = customizer.stats[0];
        dexterity = customizer.stats[1];
        contitution = customizer.stats[2];
        wisdom = customizer.stats[3];
        intelligence = customizer.stats[4];
        charisma = customizer.stats[5];
    }

    public void UpdateData(string pulledData)
    {
        string []data = pulledData.Split('|');
        int i = 0;
        CustomisationSet.characterName = data[0];
        CustomisationGet.SetTexture("Skin", int.Parse(data[1]));
        CustomisationGet.SetTexture("Hair", int.Parse(data[2]));
        CustomisationGet.SetTexture("Eyes", int.Parse(data[3]));
        CustomisationGet.SetTexture("Mouth", int.Parse(data[4]));
        CustomisationGet.SetTexture("Clothes", int.Parse(data[5]));
        CustomisationGet.SetTexture("Armour", int.Parse(data[6]));

        //customizer.skinIndex = int.Parse(data[1]);
        //customizer.hairIndex = int.Parse(data[2]);
        //customizer.eyesIndex = int.Parse(data[3]);
        //customizer.mouthIndex = int.Parse(data[4]);
        //customizer.clothesIndex = int.Parse(data[5]);
        //customizer.armourIndex = int.Parse(data[6]);

        customizer.selectedIndex = int.Parse(data[7]);
        customizer.points = int.Parse(data[8]);
        customizer.stats[0] = int.Parse(data[9]);
        customizer.stats[1] = int.Parse(data[10]);
        customizer.stats[2] = int.Parse(data[11]);
        customizer.stats[3] = int.Parse(data[12]);
        customizer.stats[4] = int.Parse(data[13]);
        customizer.stats[5] = int.Parse(data[14]);
    }

    public IEnumerator SendData() 
    {
        customizer.Save();
        SaveData();
        string sendData = "http://localhost/nsirpg/playerdatasend.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("charName", charName);
        form.AddField("skinIndex", skinIndex);
        form.AddField("hairIndex", hairIndex);
        form.AddField("eyesIndex", eyesIndex);
        form.AddField("mouthIndex", mouthIndex);
        form.AddField("clothesIndex", clothesIndex);
        form.AddField("armourIndex", armourIndex);
        form.AddField("class", classIndex);
        form.AddField("points", points);
        form.AddField("strength", strength);
        form.AddField("dexterity", dexterity);
        form.AddField("constitution", contitution);
        form.AddField("wisdom", wisdom);
        form.AddField("intelligence", intelligence);
        form.AddField("charisma", charisma);
        UnityWebRequest webRequest = UnityWebRequest.Post(sendData, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest);
    }

    public void SendDataButton()
    {
        StartCoroutine(SendData());
    }

    public IEnumerator RetrieveData() 
    {
        string pullData = "http://localhost/nsirpg/pullplayerdata.php";
        WWWForm form = new WWWForm();
        form.AddField("username", InventoryCanvas.loggedInUsername);
        UnityWebRequest webRequest = UnityWebRequest.Post(pullData, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        string pulledData = webRequest.downloadHandler.text;
        if (webRequest.downloadHandler.text == "User Not Found")
        {
            SendDataButton();
        }
        else
        {
            UpdateData(pulledData);
        }
    }

    public void LoadData()
    {
        if (InventoryCanvas.loggedInUsername != "")
        {
            StartCoroutine(RetrieveData());
        }
    }
}
//forloop to save the ID values to a string so it looks like 100|200|201|302|0|120|450|66|
