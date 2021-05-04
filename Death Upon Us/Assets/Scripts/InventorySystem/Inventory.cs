using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Arrows, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Arrows, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1});
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
