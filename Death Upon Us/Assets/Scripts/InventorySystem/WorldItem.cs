using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private string itemType;
    [SerializeField] private int amount;
    private Item item;
    // Start is called before the first frame update
    void Start()
    {
        Item.ItemType type = Item.ItemType.Medkit;
        switch (itemType) 
        {
            case "medKit":
                type = Item.ItemType.Medkit;
                break;
            case "arrow":
                type = Item.ItemType.Arrows;
                break;
        }
        item = new Item { itemType = type, amount = amount};
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
