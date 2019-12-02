using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataToSend dataRetrieve;

    public void Start()
    {
        dataRetrieve.LoadData();
    }
}
