using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Stick", "Boots of Swiftness", "Long Sword", "Small Health Pot" };
    //Indices inside the imbeded array represent (-1 value = nothing): 0:damage, 1:stack max, 2:mana, 3:cooldown, 4:item type (-1 = not weapon, 0 = melee, 1 = ranged, 2 = consumable),
    //5: armor type (-1 = not armor, 0 = head, 1 = chest, 2 = boots/legs, 3 = ring one, 4 = ring two), 6: armor value, 7: heal amount
    protected int[,] itemStats = new int[7, 8] { {0,0,0,0,-1,-1,0,0}, {0,100,0,0,-1,-1,0,0}, {0,100,0,0,-1,-1,0,0}, {0,10,0,0,-1,-1,0,0}, 
                                                 {0,1,0,0,-1,2,2,0}, {5,1,0,50,0,-1,0,0}, {0,10,0,0,2,-1,0,20} };

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
    public int GetItemArmorType(int itemID)
    {
        return itemStats[itemID, 5];
    }
    public int GetHealAmount(int itemID)
    {
        return itemStats[itemID, 7];
    }
}
