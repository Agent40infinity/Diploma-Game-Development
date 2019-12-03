using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCanvas : MonoBehaviour
{
    #region Variables
    //General:
    public List<Item> inv = new List<Item>();
    public Item selectedItem;
    public bool isShown;
    public bool isInv = true;
    public Vector2 scrt;
    public Vector2 scrollPos;
    public int money;
    public string sortType = "";
    public string[] sortList;
    public int inventorySize = 34;
    public bool setupName = false;

    public static string loggedInUsername = "";

    //Slots:
    public int slotIndex = 0;
    public ItemType type;

    //References:
    public Transform dropLocation;
    public GameObject inventory, character, menu;
    public ScrollRect scrollview;
    public GameObject invButton;
    public RectTransform content;
    public GameObject selected, selectedIcon, selectedName, selectedDescription, selectedAmount, selectedValue, selectedType, selectedEquip, selectedDiscard;
    public GameObject inputName;

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

    public void invSwitch(bool toggle)
    {
        isInv = toggle;
        if (isInv)
        {
            inventory.SetActive(true); //Background
            character.SetActive(false); //Character
        }
        else
        {
            inventory.SetActive(false); //Background
            character.SetActive(true); //Character
            if (setupName == false)
            {
                inputName.GetComponent<InputField>().text = CustomisationSet.characterName;
                setupName = true;
            }

        }
        menu.SetActive(true);
    }

    private void OnGUI()
    {
        if (isShown)
        {
            invSwitch(isInv);

            if (selectedItem != null) //Displays the selected Item's information
            {
                selected.SetActive(true);
                //selectedIcon.GetComponent<Image>().sprite = selectedItem.Icon; //Displays the Selected Item's Icon
                selectedName.GetComponentInChildren<Text>().text = selectedItem.Name; //Displays the Selected Item's Name
                selectedDescription.GetComponentInChildren<Text>().text = selectedItem.Description; //Displays the Selected Item's Description
                selectedAmount.GetComponentInChildren<Text>().text = selectedItem.Amount.ToString(); //Displays the Selected Item's Count
                selectedValue.GetComponentInChildren<Text>().text = selectedItem.Value.ToString(); //Displays the Selected Item's Value
                selectedType.GetComponentInChildren<Text>().text = selectedItem.Type.ToString(); //Displays the Selected Item's categorised type
                DisplayItemUse(selectedItem.Type);
            }
            else
            {
                selected.SetActive(false);
            }
        }
        else
        {
            inventory.SetActive(false);
            character.SetActive(false);
            menu.SetActive(false);
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

    void SpawnButton(int invS)
    {
        for (int i = invS; i < inv.Count; i++)
        {
            GameObject item = Instantiate(invButton, content);
            item.name = (inv[i].Name + " " + i);
            item.GetComponentInChildren<Text>().text = inv[i].Name;
        }
    }

    public void DisplayItemUse(ItemType iType)
    {
        slotIndex = 0;
        switch (iType)
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
            selectedEquip.GetComponentInChildren<Text>().text = "Equip";
        }
        else
        {
            selectedEquip.GetComponentInChildren<Text>().text = "Unequip";
        }
    }

    public void ItemEquip()
    {
        string name = selectedEquip.GetComponentInChildren<Text>().text;
        if (name == "Equip")
        {
            if (equipmentSlots[slotIndex].curItem != null)
            {
                Destroy(equipmentSlots[slotIndex].curItem);
            }
            GameObject curItem = Instantiate(selectedItem.ItemModel, equipmentSlots[slotIndex].location);
            equipmentSlots[slotIndex].curItem = curItem;
            curItem.name = selectedItem.Name;
        }
        else if (name == "Unequip")
        {
            Destroy(equipmentSlots[slotIndex].curItem);
        }
  }

  public void ItemDiscard()
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
        if (selectedItem.Amount > 1) //Checks if there is more than one item of that same type in your inventory
        {
            selectedItem.Amount--; //Deducts item's amount
        }
        else
        {
            inv.Remove(selectedItem); //Removes the item entry completely
            selectedItem = null; //Sets selectedItem to null
        }
    }

    public void SortItem()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), name);
        Debug.Log(type);
        GameObject[] entry = GameObject.FindGameObjectsWithTag("Item");
        if (type != ItemType.All)
        {
            Debug.Log("Some");
            for (int i = 0; i < inv.Count; i++)
            {
                if (inv[i].Type != type)
                {
                    Debug.Log("off");

                    entry[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < inv.Count; i++)
            {
                Debug.Log("all");

                entry[i].gameObject.SetActive(true);
            }
        }
    }
}