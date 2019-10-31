using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ItemButton : MonoBehaviour
{
    public InventoryCanvas inventory;

    int GetNumberFromString(string word) //Allows for the trasnlation of strings into integers.
    {
        string number = Regex.Match(word, @"\d+").Value;

        int result;
        if (int.TryParse(number, out result))
        {
            return result;
        }
        return -1;
    }

    public void Start()
    {
        inventory = gameObject.GetComponentInParent<InventoryCanvas>();
    }

    public void DisplayItem()
    {
        int index = GetNumberFromString(gameObject.name);
        inventory.selectedItem = inventory.inv[index];
    }
}
