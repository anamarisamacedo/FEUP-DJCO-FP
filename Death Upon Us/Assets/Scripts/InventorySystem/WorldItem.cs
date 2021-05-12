using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private string itemType;
    [SerializeField] private int amount;
    private Item item;
    private float speed = 60f;
    
    void Start()
    {
        Item.ItemType type = Item.ItemType.Medkit;
        switch (itemType) 
        {
            case "Medkit":
                type = Item.ItemType.Medkit;
                break;
            case "Arrows":
                type = Item.ItemType.Arrows;
                break;
            case "Bow":
                type = Item.ItemType.Bow;
                break;
            case "BlueMonsterDrop":
                type = Item.ItemType.BlueMonsterDrop;
                break;
            case "OrangeMonsterDrop":
                type = Item.ItemType.OrangeMonsterDrop;
                break;
            case "PurpleMonsterDrop":
                type = Item.ItemType.PurpleMonsterDrop;
                break;
            case "Knife":
                type = Item.ItemType.Knife;
                break;
            case "keyHouse1":
                type = Item.ItemType.KeyHouse1;
                break;
        }
        item = new Item { itemType = type, amount = amount};
    }

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
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
