using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Game Data Collection")]
public class GameData
{
    public Vector3 position;
    public Quaternion rotation;
    [XmlArray("Dialogue")]
    [XmlArrayItem("")]
    public string[] Dialogue;


}
