using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    protected string[] itemNames = new string[] { "Gold Coin", "Silver Coin", "Copper Coin", "Fireball" };
    protected float[] itemDamages = new float[] { 0, 0, 0, 4 };
    protected int[] itemStacks = new int[] {0, 100, 100, 1};

    public float GetItemDamage(int itemID)
    {
        return  itemDamages[itemID]*baseDamage;
    }
}
