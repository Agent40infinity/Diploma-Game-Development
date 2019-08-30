using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasChecked = false;
    public bool doorCheck = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Door")
        {
            hasChecked = true;
            Debug.Log("Mhm");
        }
    }
}
