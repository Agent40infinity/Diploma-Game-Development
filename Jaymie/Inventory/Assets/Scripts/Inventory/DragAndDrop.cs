using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    #region Variables
    //Inventory:
    [Header("Inventory")]
    public bool showInv;
    public List<Item> inv = new List<Item>();
    public int slotX, slotY;
    public Rect inventorySize;

    //Dragging:
    [Header("Dragging")]
    public bool isDragging;
    public int draggedFrom;
    public Item draggedItem;
    public GameObject droppedItem;

    //Tool Tips:
    [Header("ToolTip")]
    public int toolTipItem;
    public bool showToolTip;
    public Rect toolTipRect;

    //References:
    [Header("References")]
    public Vector2 scrt;
    #endregion

    #region Clamp to Screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion

    #region ToolTip
    #region ToopTipContent
    private string toolTipText(int index)
    {
        string toolTipText = inv[index].Name + "\n" + inv[index].Description + "\nValue: " + inv[index].Value;
        return toolTipText;
    }
    #endregion

    #region ToolTipWindow
    public void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scrt.x * 6, scrt.y * 2), toolTipText(toolTipItem));
    }
    #endregion 
    #endregion

    #region Add Item
    public void AddItem(int itemID)
    {
        for (int i = 0; i < inv.Count; i++)
        {
            if (inv[i].Name == null)
            {
                inv[i] = ItemData.CreateItem(itemID);
                Debug.Log("Addde Item: " + inv[i].Name);
                return;
            }
        }
    }
    #endregion

    #region Drop Item
    public void DropItem()
    {
        droppedItem = draggedItem.ItemModel;
        droppedItem = Instantiate(droppedItem, transform.position + transform.position * 3, Quaternion.identity);
        droppedItem.name = draggedItem.Name;
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        droppedItem = null;
    }
    #endregion

    #region Draw Item
    public void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scrt.x * 0.5f, scrt.y * 0.5f), draggedItem.Icon);
        }
    }
    #endregion

    #region Toggle Inventory
    public void ToggleInv()
    {
        if (showInv)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            showInv = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showInv = true;
        }
    }
    #endregion

    #region Drag Inventory
    public void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, scrt.y * 0.25f, scrt.x * 6, scrt.y * 0.5f), "Banner");
        GUI.Box(new Rect(0, scrt.y * 4.25f, scrt.x * 6, scrt.y * 0.5f), "Gold Display");
        showToolTip = false;

        #region Nested For Loop
        int i = 0;
        Event e = Event.current;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scrt.x * 0.125f + x * (scrt.x * 0.75f), scrt.y * 0.75f + y * (scrt.y * 0.65f), scrt.x * 0.75f, scrt.y * 0.65f);
                GUI.Box(slotLocation, "");

                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inv[i];
                    inv[i] = new Item();
                    isDragging = true;
                    draggedFrom = i;
                    Debug.Log("Currently Dragging: " + draggedItem.Name);

                }
                #endregion
                
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name != null)
                {
                    Debug.Log("Swapped your item " + draggedItem.Name + "with " + inv[i].Name);
                    inv[draggedFrom] = inv[i];
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion

                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name == null)
                {
                    Debug.Log("Placing your item " + draggedItem.Name);
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion

                #region Return Item
                //if ()
                #endregion
                
                #region Draw Item Icon
                if (inv[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inv[i].Icon);
                    if (slotLocation.Contains(e.mousePosition) && !isDragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                }
                #endregion
                i++;
            }
        }
        #endregion

        #region Drag Points
        GUI.DragWindow(new Rect(0, scrt.y * 0.25f, scrt.x * 0.25f, scrt.y * 3.75f)); //Left
        GUI.DragWindow(new Rect(scrt.x * 5.5f, scrt.y * 0.25f, scrt.x * 0.25f, scrt.y * 3.75f)); //Right
        GUI.DragWindow(new Rect(0, 0, scrt.x * 6, scrt.y * 0.25f)); //Up
        GUI.DragWindow(new Rect(0, scrt.y * 4, scrt.x * 6, scrt.y * 0.25f)); //Down
        #endregion
    }
    #endregion

    #region General
    public void Start()
    {
        AdjustScreen();
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inv.Add(new Item());
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInv();
        }

        if (scrt.x != Screen.width / 16)
        {
            AdjustScreen();
        }

        Debug.Log(draggedItem);
    }

    public void AdjustScreen()
    {
        scrt.x = Screen.width / 16;
        scrt.y = Screen.height / 9;
        inventorySize = new Rect(scrt.x, scrt.y, scrt.x * 6, scrt.y * 4.5f);
    }
    #endregion

    #region onGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region Inventory when true
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "Player Inventory"));
            #region ToolTipDisplay
            if (showToolTip)
            {
                toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, scrt.x * 6, scrt.y * 2);
                GUI.Window(2, toolTipRect, DrawToolTip, "");
            }
            #endregion
            #region Drop Item
            if ((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !showInv))
            {
                DropItem();
                Debug.Log("Dropped Item" + draggedItem.Name);
                draggedItem = new Item();
                isDragging = false;
            }
            #endregion

            #region Draw Item on Mouse
            if (isDragging)
            {
                if (draggedItem != null)
                {
                    Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scrt.x * 0.5f, scrt.y * 0.5f);
                    GUI.Window(10, mouseLocation, DrawItem, "");
                }
            }
            #endregion
        }
        #endregion
    }
    #endregion
}