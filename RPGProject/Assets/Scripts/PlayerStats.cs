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
    //private int inventorySpotsLeft = 15;                

    public float GetDamage(int indexInItemLineup)
    {

        //Uses indexes to access damage of item
        return GetItemDamage(itemLineup[indexInItemLineup]);

    }

    //This function is intended to work with the 2D collider that detects when an item is attempted to be picked up. If it can fit it in the inventory it will pick up the item, otherwise it will leave it on the ground.
    public void PutItemInInventory(int newItemID, Collider2D other) {
        //If items ID is 0, 1, or 2, it adds to the coin purse because it is a coin
        if (newItemID >= 0 && newItemID <= 2) {
            switch(newItemID) {
                case 0:
                    coinPurse[0]++;
                    Destroy(other.gameObject);
                    break;
                case 1:

                    coinPurse[1]++;
                    Destroy(other.gameObject);
                    if (coinPurse[1] >= itemStacks[1]) {
                        coinPurse[1] -= itemStacks[1];
                        coinPurse[0]++;
                        
                    }
                    break;
                case 2:
                    coinPurse[2]++;
                    Destroy(other.gameObject);
                    if (coinPurse[2] >= itemStacks[2]) {
                        coinPurse[2] -= itemStacks[2];
                        coinPurse[1]++;
                    }
                    break;            
            }
            Debug.Log(coinPurse[0]);
        } else {//Otherwise it will check the inventory if that item already exists, then sees if it can add it to the stack. Otherwise it will add it to the first available spot
            for (int i = 0; i < inventory.Length; i++) {
                if (inventory[i] == newItemID) {
                    if (inventoryStacks[i] < itemStacks[newItemID]) {
                        inventoryStacks[i]++;
                    } else  {

                    }
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
                PutItemInInventory(itemID, other);
                itemID = -1;
                
            }
        }
    }
}
