using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public UnityEvent onItemCollected, onEmpty;

    // Count of how many items have been spawned
    private List<Item> items = new List<Item>();

    // Use this for initialization
    void Start()
    {
        // Populates the item list
        PopulateItems();
    }

    void PopulateItems()
    {
        // Get all transforms attached to GameObject
        Transform[] children = GetComponentsInChildren<Transform>();
        // Loop over each child
        foreach (var child in children)
        {
            // If a child is an item
            Item item = child.GetComponent<Item>();
            if (item)
            {
                // Tell item to call "ItemCollected" function when Player collects it
                item.onCollect.AddListener(ItemCollected);
                // Add the item to the list
                items.Add(item);
            }
        }
    }

    void ItemCollected(Item item)
    {
        // Count down items
        items.Remove(item);
        // Item Collected 
        onItemCollected.Invoke();
        // If there is no more items
        if(items.Count <= 0)
        {
            // Run onEmpty event
            onEmpty.Invoke();
        }
    }

    public Item GetItem(int index)
    {
        // Check if index is out of range
        if (index < 0 || index >= items.Count)
        {
            // Error, invalid index
            return null;
        }

        // Return the selected index
        return items[index];
    }
}
