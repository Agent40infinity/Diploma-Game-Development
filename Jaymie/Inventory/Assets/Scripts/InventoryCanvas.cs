using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace Linear
{
    public class InventoryCanvas : MonoBehaviour
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
        public GameObject inventory;
        public ScrollRect scrollview;
        public GameObject invButton;
        public RectTransform content;
        public GameObject selected, selectedIcon, selectedName, selectedDescription, selectedAmount, selectedValue, selectedType, selectedEquip, selectedDiscard;

        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        }

        public equipment[] equipmentSlots;

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
        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            sortList = System.Enum.GetNames(typeof(ItemType));
        }

        private void Update()
        {
            content.sizeDelta = new Vector2(0, 30 * inv.Count);

            if (Input.GetKey(KeyCode.Alpha1))
            {
                int invStart = inv.Count;
                CheckForDuplicates();
                int invFinish = inv.Count;
                if (invFinish > invStart)
                {
                    SpawnButton(invStart);
                }
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

                inventory.SetActive(true); //Background
                for (int i = 0; i < sortList.Length; i++) //For each type within Item
                {
                    if (GUI.Button(new Rect((i + 4.25f) * scrt.x, 0, scrt.x, 0.25f * scrt.y), sortList[i])) //Creates a button bsaed on the Item Type
                    {
                        sortType = sortList[i]; //Sets the type based on the Index
                    }
                }

                if (selectedItem != null) //Displays the selected Item's information
                {
                    selected.SetActive(true);
                    selectedIcon.GetComponent<Image>().sprite = selectedItem.Icon; //Displays the Selected Item's Icon
                    selectedName.GetComponentInChildren<Text>().text = selectedItem.Name; //Displays the Selected Item's Name
                    selectedDescription.GetComponentInChildren<Text>().text = selectedItem.Description; //Displays the Selected Item's Description
                    selectedAmount.GetComponentInChildren<Text>().text = selectedItem.Amount.ToString(); //Displays the Selected Item's Count
                    selectedValue.GetComponentInChildren<Text>().text = selectedItem.Value.ToString(); //Displays the Selected Item's Value
                    //string type = System.Enum.GetName(typeof(ItemType), selectedItem.Type);
                    selectedType.GetComponentInChildren<Text>().text = selectedItem.Type.ToString(); //Displays the Selected Item's categorised type
                    ItemUse(selectedItem.Type);
                }
                else
                {
                    selected.SetActive(false);
                }
            }
            else
            {
                inventory.SetActive(false);
            }
        }

        public void DisplayItem()
        {
            int index = GetNumberFromString(gameObject.name);
            selectedItem = inv[index];
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

        void SpawnButton(int invS)
        {
            for (int i = invS; i < inv.Count; i++)
            {
                GameObject item = Instantiate(invButton, content);
                item.name = (inv[i].Name + " " + i);
                item.GetComponentInChildren<Text>().text = inv[i].Name;
            }            
        }

        public void ItemUse(ItemType type)
        {
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
        }
    }
}
