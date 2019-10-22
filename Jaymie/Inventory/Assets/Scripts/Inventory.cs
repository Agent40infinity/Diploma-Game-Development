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
        public string[] sortList;
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
            sortList = System.Enum.GetNames(typeof(ItemType));
        }

        private void Update()
        { 
            if (Input.GetKey(KeyCode.Alpha1))
            {
                CheckForDuplicates();
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
                for (int i = 0; i < sortList.Length; i++) //For each type within Item
                {
                    if (GUI.Button(new Rect((i + 4.25f) * scrt.x, 0, scrt.x, 0.25f * scrt.y), sortList[i])) //Creates a button bsaed on the Item Type
                    {
                        sortType = sortList[i]; //Sets the type based on the Index
                    }
                }
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
            if (!(sortType == "All" || sortType == ""))
            {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
                int a = 0; //amount
                int s = 0; //slot
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        a++;
                    }
                }
                if (a <= inventorySize)
                {
                    for (int i = 0; i < inv.Count; i++) //for loop to display each item within the inventory outside of the nrmal size
                    {
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * scrt.x, 0 * scrt.y + i * (0.25f * scrt.y), 3 * scrt.x, 0.25f * scrt.y), inv[i].Name)) //Button to display the item name
                            {
                                selectedItem = inv[i]; //Selects the item if clicked
                            }
                        }
                    }
                }
                else //Checks if the page can be scrolled 
                {
                    scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scrt.y, 3.75f * scrt.x, 8.5f * scrt.y), scrollPos, new Rect(0, 0, 0, 8.5f * scrt.y + ((inv.Count - inventorySize) * (0.25f * scrt.y))), false, true); //Creates a vertical scroll bar

                    for (int i = 0; i < inv.Count; i++) //for loop to display each item within the inventory outside of the nrmal size
                    {
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * scrt.x, 0 * scrt.y + i * (0.25f * scrt.y), 3 * scrt.x, 0.25f * scrt.y), inv[i].Name)) //Button to display the item name
                            {
                                selectedItem = inv[i]; //Selects the item if clicked
                            }
                        }
                        s++;
                    }
                    GUI.EndScrollView(); //Ends the ability to scroll
                }
            }
            else
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
        }

        public void CheckForDuplicates()
        {
            List<Item> itemsToAdd = new List<Item>(); //Creates a list to store the items being added to the inventory
            for (int i = 0; i < (int)Random.Range(0, 10); i++) //For statement for finding all items being added
            {
                itemsToAdd.Add(ItemData.CreateItem((int)Random.Range(0, 10) * 100)); //Adds the items to the list
            }
            for (int i = 0; i < itemsToAdd.Count; i++) //Checks for each item within the list
            {
                if (inv.Count != 0) //Checks if the inventory is full to allow for the first item to be added.
                {
                    for (int j = 0; j < inv.Count; j++) //Used to load each item within the inventory to allow it to be checked
                    {
                        if (inv[j].Name == itemsToAdd[i].Name && inv[j].Type != ItemType.Armour && inv[j].Type != ItemType.Weapon) //Used to check if the name of the item in the inventory is the same as the item attempting to be added to allow for the removal of duplicate items
                        {
                            inv[j].Amount++; //Adds the duplicate item to the existing item's count
                            itemsToAdd[i] = null; //Sets the item to null to remove it from the list
                            j = inv.Count; //Jumps to the null item to end the for loop
                        }
                        else if (j == inv.Count - 1) //Checks if j has reached the last item within the list of items to add so that it can create a new non-duplicate entry
                        {
                            inv.Add(itemsToAdd[i]); //Adds the new item to the inventory
                            itemsToAdd[i] = null; //Sets the  item to null to remove it from the list
                            j = inv.Count; //Jumps to the null item to end the for loop
                        }
                        Debug.Log(i);
                    }
                }
                else //Used to add the first item in the inventory
                {
                    inv.Add(itemsToAdd[i]); //Adds the item to the inventory
                    itemsToAdd[i] = null; //Sets the item to null to remove it from the list
                }
            }
        }

        public void ItemUse(ItemType type)
        {
            GUI.skin = invSkin;
            int slotIndex = 0;
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
                    slotIndex = 1;
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

            if (equipmentSlots[slotIndex].curItem == null || selectedItem.Name != equipmentSlots[slotIndex].curItem.name)
            {
                if (GUI.Button(new Rect(4.1f * scrt.x, 8 * scrt.y, 1.5f * scrt.x, 0.4f * scrt.y), "Equip"))
                {
                    if (equipmentSlots[slotIndex].curItem != null)
                    {
                        Destroy(equipmentSlots[slotIndex].curItem);
                    }
                    GameObject curItem = Instantiate(selectedItem.ItemModel, equipmentSlots[slotIndex].location);
                    equipmentSlots[slotIndex].curItem = curItem;
                    curItem.name = selectedItem.Name;
                }
            }
            else
            {
                if (GUI.Button(new Rect(4.1f * scrt.x, 8 * scrt.y, 1.5f * scrt.x, 0.4f * scrt.y), "Unequip"))
                {
                    Destroy(equipmentSlots[slotIndex].curItem);
                }
            }

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
                if (selectedItem.Amount != 0) //Checks if there is more than one item of that same type in your inventory
                {
                    selectedItem.Amount--; //Deducts item's amount
                }
                else 
                {
                    inv.Remove(selectedItem); //Removes the item entry completely
                    selectedItem = null; //Sets selectedItem to null
                }
            }
            GUI.skin = null;
        }
    }
}
