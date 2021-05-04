using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Medkit, 
        Arrows,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit: 
                return ItemAssets.Instance.medKitSprite;
            case ItemType.Arrows: 
                return ItemAssets.Instance.arrowSprite;
        }
    }

}
