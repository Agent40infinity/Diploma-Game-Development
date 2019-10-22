using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int ItemId)
    {
        string name = "";
        string description = "";
        int amount = 0;
        int _value = 0;
        int damage = 0;
        int armour = 0;
        int heal = 0;
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;

        switch (ItemId)
        {
            #region 0-99 Ingrediants
            case 0:
                name = "Mushroom";
                description = "Literally just a boring mushroom.";
                amount = 1;
                _value = 1;
                damage = 0;
                armour = 0;
                heal = 1;
                icon = "mushroom";
                mesh = "Sword";
                type = ItemType.Ingrediant;
                break;
            #endregion
            #region 100-199 Potions
            case 100:
                name = "Healing Potion";
                description = "Literally just heals you.";
                amount = 1;
                _value = 50;
                damage = 0;
                armour = 0;
                heal = 25;
                icon = "Potion";
                mesh = "Sword";
                type = ItemType.Potions;
                break;
            #endregion
            #region 200-299 Scrolls
            case 200:
                name = "Magic Scroll";
                description = "Does magic and shit.";
                amount = 1; 
                _value = 100;
                damage = 10;
                armour = 0;
                heal = 0;
                icon = "Scroll";
                mesh = "Sword";
                type = ItemType.Scrolls;
                break;
            #endregion
            #region 300-399 Food
            case 300:
                name = "Apple";
                description = "It fell from a tree.";
                amount = 1;
                _value = 5;
                damage = 0;
                armour = 0;
                heal = 2;
                icon = "Apple";
                mesh = "Sword";
                type = ItemType.Food;
                break;
            #endregion
            #region 400-499 Armour
            case 400:
                name = "Cloths";
                description = "Stops you from taking as much damage.";
                amount = 1;
                _value = 125;
                damage = 0;
                armour = 2;
                heal = 0;
                icon = "Armour";
                mesh = "Armour";
                type = ItemType.Armour;
                break;
            #endregion
            #region 500-599 Weapons
            case 500:
                name = "Sword";
                description = "Does damage. That's it. It does damage.";
                amount = 1;
                _value = 250;
                damage = 25;
                armour = 0;
                heal = 0;
                icon = "Sword";
                mesh = "Sword";
                type = ItemType.Weapon;
                break;
            #endregion
            #region 600-699 Craftable
            case 600:
                name = "Stick";
                description = "It's a stick. Trust me.";
                amount = 0;
                _value = 0;
                damage = 0;
                armour = 0;
                heal = 0;
                icon = "Stick";
                mesh = "Sword";
                type = ItemType.Craftable;
                break;
            #endregion
            #region 700-799 Money
            case 700:
                name = "Gold";
                description = "It's gold that's actually worth money.";
                amount = 1;
                _value = 1000;
                damage = 0;
                armour = 0;
                heal = 0;
                icon = "Gold";
                mesh = "Sword";
                type = ItemType.Money;
                break;
            #endregion
            #region 800-899 Quest
            case 800:
                name = "God Tier Item ''Pebble''";
                description = "Holy stone of fuck off.";
                amount = 1;
                _value = 100000;
                damage = 100;
                armour = 100;
                heal = 100;
                icon = "Stone";
                mesh = "Sword";
                type = ItemType.Quest;
                break;
            #endregion
            #region 900-999 Misc
            case 900:
                name = "Gem";
                description = "Looks valuable, try putting it in your mouth.";
                amount = 1;
                _value = 1000;
                damage = 0;
                armour = 0;
                heal = 0;
                icon = "Gem";
                mesh = "Sword";
                type = ItemType.Misc;
                break;
            #endregion
            #region Default
            default:
                name = "nullReference";
                description = "Where did you get this?";
                amount = 1;
                _value = 0;
                damage = 0;
                armour = 0;
                heal = 0;
                icon = "Apple";
                mesh = "Sword";
                type = ItemType.Misc;
                break;
            #endregion
        }

        Item temp = new Item
        {
            ID = ItemId,
            Name = name,
            Description = description,
            Amount = amount,
            Value = _value,
            Damage = damage,
            Armour = armour,
            Heal = heal,
            Icon = Resources.Load("Icons/" + icon) as Texture2D,
            ItemModel = Resources.Load("Mesh/" + mesh) as GameObject,
            Type = type
        };
        return temp;
    }
}
