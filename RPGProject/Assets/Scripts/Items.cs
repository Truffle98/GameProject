using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Fireball", "Stick" };
    protected float[] itemDamages = new float[] { 0, 0, 0, 4, 0};
    protected int[] itemStacks = new int[] {0, 100, 100, 1, 3};

    public float GetItemDamage(int itemID)
    {
        return  itemDamages[itemID]*baseDamage;
    }
    public GameObject GetItemObject(int itemID) {
        return gameItems[itemID];
    }
}
