using UnityEngine;

public class Item
{
    #region Variables
    //General:
    private int id;
    private string name;
    private string description;
    private int amount;
    private int _value;
    private int damage;
    private int armour;
    private int heal;
    private ItemType type;

    //References:
    private Texture2D icon;
    private GameObject mesh;
    #endregion

    #region Public Properties
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Armour
    {
        get { return armour; }
        set { armour = value; }
    }

    public int Heal
    {
        get { return heal; }
        set { heal = value; }
    }

    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public GameObject ItemModel
    {
        get { return mesh; }
        set { mesh = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    #endregion
}

public enum ItemType
{
    All,
    Ingrediant,
    Potions,
    Scrolls,
    Food, 
    Armour,
    Weapon,
    Craftable, 
    Money,
    Quest,
    Misc
}
