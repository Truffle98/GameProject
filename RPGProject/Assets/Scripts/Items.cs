using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Fireball", "Stick", "Long Sword" };
    protected int[,] itemStats = new int[6, 3] { {0,0,0}, {0,100,0}, {0,100,0}, {10,1,20}, {0,3,0}, {5,1,5}};

    public float GetItemDamage(int itemID)
    {
        return (float)itemStats[itemID, 0]*baseDamage;
    }
    public float GetManaCost(int itemID)
    {
        return (float)itemStats[itemID, 2];
    }
    public GameObject GetItemObject(int itemID) {
        return gameItems[itemID];
    }
}
