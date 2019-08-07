using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemCollectEvent : UnityEvent<Item> { }

public class Item : MonoBehaviour
{
    public int score = 1;
    public ItemCollectEvent onCollect = new ItemCollectEvent();
    

    public void Collect()
    {
        // Run collect event
        onCollect.Invoke(this);
        // Destroy item
        Destroy(gameObject);
    }
}
