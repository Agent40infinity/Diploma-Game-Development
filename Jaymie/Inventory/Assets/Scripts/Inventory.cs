using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        //General:
        public List<Item> inv = new List<Item>();
        public Item selectedItem;
        public bool isShown;
        public Vector2 scrt;
        public Vector2 scrollPos;
        public int money;
        public string sortType = "";
        public int inventorySize = 34;

        //References:
        public Transform dropLocation;
        public GUISkin invSkin;

        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        }

        public equipment[] equipmentSlots;
        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CheckForDuplicates();
                //for (int i = 0; i < 10; i++)
                //{
                //    inv.Add(ItemData.CreateItem(i * 100));
                //}
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isShown)
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    isShown = false;
                }
                else
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    isShown = true;
                }
            }
        }

        private void OnGUI()
        {
            if (isShown)
            {
                scrt.x = Screen.width / 16;
                scrt.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scrt.x * 8, Screen.height), ""); //Background
                Display();

                if (selectedItem != null) //Displays the selected Item's information
                {
                    GUI.skin = invSkin; //Sets the skin to overlay the GUI
                    GUI.Box(new Rect(4.125f * scrt.x, 0 * scrt.y, 3.5f * scrt.x, 3.5f * scrt.y), ""); //Displays a box behind the Selected Item's Icon
                    GUI.DrawTexture(new Rect(4.865f * scrt.x, 0.65f * scrt.y, 2 * scrt.x, 2 * scrt.y), selectedItem.Icon); //Displays the Selected Item's Icon
                    GUI.Box(new Rect(4.125f * scrt.x, 3.25f * scrt.y, 3.5f * scrt.x, 0.4f * scrt.y), "Name: " + selectedItem.Name); //Displays the Selected Item's Name
                    GUI.Box(new Rect(4.125f * scrt.x, 3.6f * scrt.y, 3.5f * scrt.x, scrt.y), selectedItem.Description); //Displays the Selected Item's Description
                    GUI.Box(new Rect(4.125f * scrt.x, 4.6f * scrt.y, 3.5f * scrt.x, 0.4f * scrt.y), "Amount: " + selectedItem.Amount); //Displays the Selected Item's Count
                    GUI.Box(new Rect(4.125f * scrt.x, 5 * scrt.y, 3.5f * scrt.x, 0.4f * scrt.y), "Price: " + selectedItem.Value); //Displays the Selected Item's Value
                    GUI.Box(new Rect(4.125f * scrt.x, 5.35f * scrt.y, 3.5f * scrt.x, 0.4f * scrt.y), "Category: " + selectedItem.Type); //Displays the Selected Item's categorised type
                    GUI.skin = null; //returns the skin to null
                    ItemUse(selectedItem.Type);
                }
                else { return; }
            }
        }

        public void Display()
        {
            if (inv.Count <= inventorySize) //Checks if the page is full
            {
                for (int i = 0; i < inv.Count; i++) //for loop to display each item within the inventory
                {
                    if (GUI.Button(new Rect(0.5f * scrt.x, 0.25f * scrt.y + i * (0.25f * scrt.y), 3 * scrt.x, 0.25f * scrt.y), inv[i].Name)) //Button to display the item name
                    {
                        selectedItem = inv[i]; //Selects the item if clicked
                    }
                }
            }
            else //Checks if the page can be scrolled 
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scrt.y, 3.75f * scrt.x, 8.5f * scrt.y), scrollPos, new Rect(0, 0, 0, 8.5f * scrt.y + ((inv.Count - inventorySize) * (0.25f * scrt.y))), false, true); //Creates a vertical scroll bar

                for (int i = 0; i < inv.Count; i++) //for loop to display each item within the inventory outside of the nrmal size
                {
                    if (GUI.Button(new Rect(0.5f * scrt.x, 0 * scrt.y + i * (0.25f * scrt.y), 3 * scrt.x, 0.25f * scrt.y), inv[i].Name)) //Button to display the item name
                    {
                        selectedItem = inv[i]; //Selects the item if clicked
                    }
                }

                GUI.EndScrollView(); //Ends the ability to scroll
            }
        }

        public void CheckForDuplicates()
        {
            //bool beenAdded = false;
            List<Item> itemsToAdd = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                itemsToAdd.Add(ItemData.CreateItem(i * 100));
            }
            for (int i = 0; i < itemsToAdd.Count; i++)
            {
                if (inv.Count != 0 /*&& beenAdded == false*/)
                {
                    for (int j = 0; j < inv.Count; j++)
                    {
                        if (inv[j].Name == itemsToAdd[i].Name)
                        {
                            inv[j].Amount++;
                            itemsToAdd[i] = null;
                            j = inv.Count;
                            //beenAdded = true;
                        }
                        else 
                        {
                            inv.Add(itemsToAdd[i]);
                            itemsToAdd[i] = null;
                            j = inv.Count;
                            //beenAdded = true;
                        }
                        i++;
                    }
                }
                else
                {
                    inv.Add(itemsToAdd[i]);
                    itemsToAdd[i] = null;
                    i++;
                    //beenAdded = true;
                }
                //beenAdded = false;
            }
            //itemsToAdd = null;
        }

        public void ItemUse(ItemType type)
        {
            switch (type)
            {
                case ItemType.Ingrediant:
                    break;
                case ItemType.Potions:
                    break;
                case ItemType.Scrolls:
                    break;
                case ItemType.Food:
                    break;
                case ItemType.Armour:
                    break;
                case ItemType.Weapon:
                    break;
                case ItemType.Craftable:
                    break;
                case ItemType.Money:
                    break;
                case ItemType.Quest:
                    break;
                case ItemType.Misc:
                    break;
            }

            GUI.skin = invSkin;
            if (GUI.Button(new Rect(6f * scrt.x, 8 * scrt.y, 1.5f * scrt.x, 0.4f * scrt.y), "Discard")) //Creates a button to discard items
            {
                if (equipmentSlots[1].curItem != null && selectedItem.ItemModel.name == equipmentSlots[1].name) //Checks whether or not the item is a peice of armour and is equip
                {
                    Destroy(equipmentSlots[1].curItem); //Distroys the GameObject attached to the player
                }
                if (selectedItem.ItemModel != null)
                {
                    GameObject droppedItem = Instantiate(selectedItem.ItemModel, dropLocation.position, Quaternion.identity) as GameObject; //Creates and spawns a new GameObject to represent the dropped item\
                    droppedItem.name = selectedItem.Name; //Matches the name of the selectedItem with the name of the new GameObject
                    droppedItem.AddComponent<Rigidbody>().useGravity = true; //Attaches a Rigidbody to the droppedItem GameObject
                    droppedItem.AddComponent<BoxCollider>(); //Attaches a BoxCollider to the droppedItem GameObject
                }
                inv.Remove(selectedItem);
                selectedItem = null;
            }
            GUI.skin = null;
        }
    }
}
