using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


public class GameManager : MonoBehaviour
{
    public Transform player;
    public string fileName = "GameData.xml";
    private GameData data = new GameData();

    private void Start()
    {
        string path = Application.dataPath + "/" + fileName;
        Load(path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player = FindObjectOfType<Player>().transform;
            data.position = player.position;
            data.rotation = player.rotation;
            data.Dialogue = new string[]
            {
                "Hello",
                "World"
            };
            Save(Application.dataPath + "/" + fileName);
        }
    }

    public void Load(string path)
    {

    }

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved");
    }
}
