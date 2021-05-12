using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Medkit, 
        Arrows,
        Bow,
        Knife,
        BlueMonsterDrop,
        OrangeMonsterDrop,
        PurpleMonsterDrop,
        KeyHouse1
    }

    public ItemType itemType;
    public int amount;
    public int position;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit: 
                return ItemAssets.Instance.medKitSprite;
            case ItemType.Arrows: 
                return ItemAssets.Instance.arrowSprite;
            case ItemType.Bow: 
                return ItemAssets.Instance.bowSprite;
            case ItemType.Knife: 
                return ItemAssets.Instance.knifeSprite;
            case ItemType.BlueMonsterDrop: 
                return ItemAssets.Instance.blueMonsterDropSprite;
            case ItemType.OrangeMonsterDrop: 
                return ItemAssets.Instance.orangeMonsterDropSprite;
            case ItemType.PurpleMonsterDrop: 
                return ItemAssets.Instance.purpleMonsterDropSprite;
            case ItemType.KeyHouse1: 
                return ItemAssets.Instance.keySprite;
        }
    }

    public bool IsUsable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Knife: 
            case ItemType.Arrows: 
                return false;
            case ItemType.Bow: 
            case ItemType.Medkit:
            case ItemType.BlueMonsterDrop: 
            case ItemType.OrangeMonsterDrop: 
            case ItemType.PurpleMonsterDrop: 
                return true;
        }
    }
    public int getAmount()
    {
        return amount;
    }

}
