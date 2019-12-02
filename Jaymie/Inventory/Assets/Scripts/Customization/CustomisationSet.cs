using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomisationSet : MonoBehaviour
{
    #region Variables
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
    [Header("Renderer")]
    public Renderer character;
    [Header("Max Index")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, clothesMax, armourMax;
    [Header("Character Name")]
    public static string characterName;
    public static string tempCharacterName;
    [Header("Stats")]
    public CharacterClass charClass;
    public CharacterRace charRace;
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];
    public int points = 10;
    public CharacterClass charClsas = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedIndex = 0;

    public GameObject customizer;
    public GameObject classText, pointText, str, dex, cont, wis, inte, chari;
    public GameObject[] decreaseStats = new GameObject[6];
    public GameObject[] increaseStats = new GameObject[6];
    public GameObject inputName;
    public GameObject player, warrior;
    public DataToSend dataToSend;
    #endregion

    #region General
    public void Start()
    {
        //player = GameObject.FindWithTag("Player");
        warrior = GameObject.Find("Warrior");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        selectedClass = new string[] { "Barbarian", "Bard", "Druid", "Monk", "Paladin", "Ranger", "Scorcerer", "Warlock", };

        #region for loop to pull textures from file
        for (int i = 0; i < skinMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(temp);
        }
        #endregion
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Eyes", eyesIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
        SetTexture("Armour", armourIndex = 0);
    }

    public void Update()
    {
        Debug.Log("Character Name:" + characterName);
        classText.GetComponent<Text>().text = selectedClass[selectedIndex];
        pointText.GetComponent<Text>().text = "Points: " + points;
        tempCharacterName = inputName.GetComponent<InputField>().text;
        str.GetComponent<Text>().text = "Strength: " + (stats[0] + tempStats[0]);
        dex.GetComponent<Text>().text = "Dexterity: " + (stats[1] + tempStats[1]);
        cont.GetComponent<Text>().text = "Constitution: " + (stats[2] + tempStats[2]);
        wis.GetComponent<Text>().text = "Wisdom: " + (stats[3] + tempStats[3]);
        inte.GetComponent<Text>().text = "Intelligence: " + (stats[4] + tempStats[4]);
        chari.GetComponent<Text>().text = "Charisma: " + (stats[5] + tempStats[5]);

        if (points >= 10)
        {
            for (int i = 0; i < decreaseStats.Length; i++)
            {
                decreaseStats[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < decreaseStats.Length; i++)
            {
                decreaseStats[i].SetActive(true);
            }
        }
        if (points <= 0)
        {
            for (int i = 0; i < increaseStats.Length; i++)
            {
                increaseStats[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < increaseStats.Length; i++)
            {
                increaseStats[i].SetActive(true);
            }
        }
    }
    #endregion

    #region SetTexture
    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        #region Switch Material
        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;
        }
        #endregion
        #region OutSide Switch
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        Material[] mat = character.materials;
        mat[matIndex].mainTexture = textures[index];
        character.materials = mat;
        #endregion
        #region Set Material Switch
        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
        }
        #endregion
    }
    #endregion

    #region Save
    public void Save()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] += tempStats[i];
        }
    }
    #endregion

    #region Canvas - Buttons
    #region Textures
    #region Set
    #region Skin
    public void IncreaseSkin()
    {
        SetTexture("Skin", 1);
    }

    public void DecreaseSkin()
    {
        SetTexture("Skin", -1);
    }
    #endregion

    #region Hair
    public void IncreaseHair()
    {
        SetTexture("Hair", 1);
    }

    public void DecreaseHair()
    {
        SetTexture("Hair", -1);
    }
    #endregion

    #region Eyes
    public void IncreaseEyes()
    {
        SetTexture("Eyes", 1);
    }

    public void DecreaseEyes()
    {
        SetTexture("Eyes", -1);
    }
    #endregion

    #region Mouth
    public void IncreaseMouth()
    {
        SetTexture("Mouth", 1);
    }

    public void DecreaseMouth()
    {
        SetTexture("Mouth", -1);
    }
    #endregion

    #region Clothes
    public void IncreaseClothes()
    {
        SetTexture("Clothes", 1);
    }

    public void DecreaseClothes()
    {
        SetTexture("Clothes", -1);
    }
    #endregion

    #region Armour
    public void IncreaseArmour()
    {
        SetTexture("Armour", 1);
    }

    public void DecreaseArmour()
    {
        SetTexture("Armour", -1);
    }
    #endregion
    #endregion

    #region Random | Reset
    public void SetRandom()
    {
        SetTexture("Skin", Random.Range(0, skinMax - 1));
        SetTexture("Hair", Random.Range(0, hairMax - 1));
        SetTexture("Mouth", Random.Range(0, mouthMax - 1));
        SetTexture("Eyes", Random.Range(0, eyesMax - 1));
        SetTexture("Clothes", Random.Range(0, clothesMax - 1));
        SetTexture("Armour", Random.Range(0, armourMax - 1));
    }

    public void Reset()
    {
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Eyes", eyesIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
        SetTexture("Armour", armourIndex = 0);
    }
    #endregion

    #region Save
    public void SaveSettings()
    {
        Save();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        customizer.SetActive(false);
        warrior.SetActive(false);
        player.SetActive(true);
    }
    #endregion
    #endregion

    #region Stats
    #region Class
    public void UpClass()
    {
        selectedIndex++;
        if (selectedIndex > selectedClass.Length - 1)
        {
            selectedIndex = 0;
        }
        ChooseClass(selectedIndex);
    }

    public void DownClass()
    {
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = selectedClass.Length - 1;
        }
        ChooseClass(selectedIndex);
    }
    #endregion

    #region Points
    public void PointsIncrease(int x)
    {
        if (points > 0)
        {
            points--;
            tempStats[x]++;
        }
    }
    public void PointsDecrease(int x)
    {
        if (points < 10 && tempStats[x] > 0)
        {
            points++;
            tempStats[x]--;
        }
    }
    #endregion
    #endregion
    #endregion
    void ChooseClass(int className)
    {
        switch (className)
        {
            case 0:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Barbarian;
                break;
            case 1:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                charClass = CharacterClass.Bard;
                break;
            case 2:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 10;
                charClass = CharacterClass.Druid;
                break;
            case 3:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 15;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Monk;
                break;
            case 4:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 15;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 10;
                charClass = CharacterClass.Paladin;
                break;
            case 5:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Ranger;
                break;
            case 6:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Scorcerer;
                break;
            case 7:
                stats[0] = 5;
                stats[1] = 5;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 15;
                stats[5] = 15;
                charClass = CharacterClass.Warlock;
                break;
        }
    }  
}

public enum CharacterRace
{
	Dragonborn,
	Dwarf,
	Elf,
	Gnome,
	Half_Elf,
	Half_Orc,
	Halfling,
	Human,
	Tiefling	
}

public enum CharacterClass
{
	Barbarian,
    Bard,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Scorcerer,
    Warlock
}
