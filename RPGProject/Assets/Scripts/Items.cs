using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Fireball", "Stick" };
    protected int[,] itemStats = new int[5, 3] { {0,0,0}, {0,100,0}, {0,100,0}, {10,1,20}, {0,3,0}};
    protected int[] itemStacks = new int[] {0, 100, 100, 1, 3};

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
