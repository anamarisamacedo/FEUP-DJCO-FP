using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 2});
        AddItem(new Item { itemType = Item.ItemType.Arrows, amount = 20});
        AddItem(new Item { itemType = Item.ItemType.BlueMonsterDrop, amount = 2});
        AddItem(new Item { itemType = Item.ItemType.OrangeMonsterDrop, amount = 3});
        AddItem(new Item { itemType = Item.ItemType.PurpleMonsterDrop, amount = 10});
        AddItem(new Item { itemType = Item.ItemType.Bow, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Knife, amount = 1});
    }

    public void AddItem(Item item)
    {
        bool itemInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount += item.amount;
                itemInInventory = true;
            }
        }
        if (!itemInInventory)
            itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
