using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items : BaseClass
{
    public string[] itemNames = new string[] { "Fireball", "Gold Coin", "Silver Coin", "Copper Coin" };
    public float[] itemDamages = new float[] { 1, 0, 0, 0 };

    public float GetItemDamage(int itemID)
    {
        Debug.Log(itemDamages[itemID]);
        return  itemDamages[itemID]*baseDamage;
    }
}
