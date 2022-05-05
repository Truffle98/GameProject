using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Items
{
    private MenuTest menuTest;
    private float damage;
    private int decision;

    //Lists for four usable items [id] in your 'item lineup,' items [id] in armor linup, and all items [id] in inventory
    //Indexes 0 and 1 are reserved for 'on-hand' [left-click] and 'off-hand' [right-click] items. Index 0 is therefore 'fireball' on mage class
    private int[] itemLineup = { 0, 0, 0, 0, 0, 0 };
    private int[] armorLinup = { 0, 0, 0 };
    private int[] inventory = {0, 0, 0, 0, 0,
                            0, 0, 0, 0, 0,
                            0, 0, 0, 0, 0,};

    public float GetDamage(int indexInItemLineup)
    {

        //Uses indexes to access damage of item
        return GetItemDamage(itemLineup[indexInItemLineup]);

    }
}
