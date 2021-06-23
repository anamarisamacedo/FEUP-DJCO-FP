using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public class Item
{
    public int blueFoodValue = 500*2;
    public int orangeFoodValue = 750*2;
    public int purpleFoodValue = 1000*2;
    public int medKitValue = 40;
    float nextAttackTime = 0f;
    float nextArrowTime = 0f;



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
            case ItemType.Arrows:
            case ItemType.KeyHouse1:
                return false;
            case ItemType.Knife: 
            case ItemType.Bow: 
            case ItemType.Medkit:
            case ItemType.BlueMonsterDrop: 
            case ItemType.OrangeMonsterDrop: 
            case ItemType.PurpleMonsterDrop: 
                return true;
        }
    }

    public void Use(Character character){
        switch (itemType)
        {
            case ItemType.BlueMonsterDrop: 
                character.TakeHunger(blueFoodValue);
                return;
            case ItemType.OrangeMonsterDrop:
                character.TakeHunger(orangeFoodValue);
                return; 
            case ItemType.PurpleMonsterDrop:
                character.TakeHunger(purpleFoodValue);
                return;
            case ItemType.Medkit: 
                character.Heal(medKitValue);
                return;
            case ItemType.Knife:
                if (Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / PlayerAttackRate;
                    character.Attack();
                }
                return;
            case ItemType.Arrows:
                if (Time.time >= nextArrowTime)
                {
                    nextArrowTime = Time.time + 1f / PlayerAttackRate;
                    character.gameObject.GetComponent<Shoot>().ShootArrow();
                }
                return;
            default:
                return;
        }
    }
    public int getAmount()
    {
        return amount;
    }

}
