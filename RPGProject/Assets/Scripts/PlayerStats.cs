using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Items
{
    private ItemID itemIDClass;
    private MenuTest menuTest;
    private float damage;
    private int decision, itemID;

    //Lists for four usable items [id] in your 'item lineup,' items [id] in armor lineup, and all items [id] in inventory
    //Indexes 0 and 1 are reserved for 'on-hand' [left-click] and 'off-hand' [right-click] items. Index 0 is therefore 'fireball' on mage class
    private int[] itemLineup = { 3, 0, 0, 0, 0, 0 };
    private int[] armorLinup = { 0, 0, 0, 0 };
    private int[] inventory = {0, 0, 0, 0, 0,
                               0, 0, 0, 0, 0,
                               0, 0, 0, 0, 0,};
    private int[] inventoryStacks = {0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0,};   
    private int[] coinPurse = {0, 0, 0};                   

    public float GetDamage(int indexInItemLineup)
    {

        //Uses indexes to access damage of item
        return GetItemDamage(itemLineup[indexInItemLineup]);

    }

    public void PutItemInInventory(int newItemID) {

        if (newItemID >= 0 && newItemID <= 2) {
            switch(newItemID) {
                case 0:
                    coinPurse[0]++;
                    break;
                case 1:

                    coinPurse[1]++;
                    if (coinPurse[1] >= itemStacks[1]) {
                        coinPurse[1] -= itemStacks[1];
                        coinPurse[0]++;
                    }
                    break;
                case 2:
                    coinPurse[2]++;
                    if (coinPurse[2] >= itemStacks[2]) {
                        coinPurse[2] -= itemStacks[2];
                        coinPurse[1]++;
                    }
                    break;            
            }
            Debug.Log(coinPurse[0]);
        } else {
            foreach (int ID in inventory) {
                if (ID == newItemID) {

                }

            }
        }
        

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dropped Item")){
            if (Input.GetKey(KeyCode.E))
            {
                itemIDClass = other.GetComponent<ItemID>();
                itemID = itemIDClass.GetItemID();
                PutItemInInventory(itemID);
                itemID = -1;
                Destroy(other.gameObject);
            }
        }
    }
}
