using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Fireball", "Stick", "Long Sword" };
    //Indices inside the imbeded array represent: 0:damage, 1:stack max, 2:mana, 3:cooldown, 4:item type (-1 = not weapon, 0 = melee, 1 = ranged), 5: special effects (for abilities),
    //6: armor value
    protected int[,] itemStats = new int[6, 6] { {0,0,0,0,-1,0}, {0,100,0,0,-1,0}, {0,100,0,0,-1,0}, {10,1,20,100,1,0}, {0,10,0,0,-1,0}, {5,1,0,50,0,0}};

    public float GetItemDamage(int itemID)
    {
        return (float)itemStats[itemID, 0]*baseDamage;
    }
    public float GetItemArmor(int itemID)
    {
        return (float)itemStats[itemID, 6];
    }
    public float GetManaCost(int itemID)
    {
        return (float)itemStats[itemID, 2];
    }
    public int GetCooldown(int itemID)
    {
        return itemStats[itemID, 3];
    }
    public GameObject GetItemObject(int itemID) {
        return gameItems[itemID];
    }
    public int GetItemType(int itemID)
    {
        return itemStats[itemID, 4];
    }
    public int GetSpecialEffects(int itemID) {
        return itemStats[itemID, 5];
    }
}
