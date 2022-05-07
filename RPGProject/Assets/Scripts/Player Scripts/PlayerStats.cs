using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Items
{
    private ItemID itemIDClass;
    private MenuTest menuTest;
    private float damage;
    private int decision, itemID;
    private string itemName;

    //Lists for four usable items [id] in your 'item lineup,' items [id] in armor lineup, and all items [id] in inventory
    //Indexes 0 and 1 are reserved for 'on-hand' [left-click] and 'off-hand' [right-click] items. Index 0 is therefore 'fireball' on mage class
    private int[] itemLineup = { 3, -1, -1, -1};
    private int[] armorLinup = { -1, -1, -1, -1 };
    public int[] inventory = {-1, -1, -1, -1, -1,
                               -1, -1, -1, -1, -1,
                               -1, -1, -1, -1, -1};
    public int[] inventoryStacks = {0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0};   
    private int[] coinPurse = {0, 0, 0};   
    private int inventoryFailedPickupCooldown = 0, messageCooldown = 0;

    public int GetEquippedItem(int indexInItemLineup)
    {

        return itemLineup[indexInItemLineup];

    }
    public int GetItemInInventory(int indexInInventory){
        return inventory[indexInInventory];
    }
    public float GetDamage(int indexInItemLineup)
    {

        //Uses indexes to access damage of item
        return GetItemDamage(itemLineup[indexInItemLineup]);

    }

    //This function is intended to work with the 2D collider that detects when an item is attempted to be picked up. If it can fit it in the inventory it will pick up the item, otherwise it will leave it on the ground.
    public void PutItemInInventory(int newItemID, Collider2D other) {
        itemName = itemNames[itemID];
        //If items ID is 0, 1, or 2, it adds to the coin purse because it is a coin
        if (newItemID >= 0 && newItemID <= 2) {
            switch(newItemID) {
                case 0:
                    coinPurse[0]++;
                    Destroy(other.gameObject);
                    return;
                case 1:
                    coinPurse[1]++;
                    Destroy(other.gameObject);
                    if (coinPurse[1] >= itemStacks[1]) {
                        coinPurse[1] -= itemStacks[1];
                        coinPurse[0]++;
                        
                    }
                    return;
                case 2:
                    coinPurse[2]++;
                    Destroy(other.gameObject);
                    if (coinPurse[2] >= itemStacks[2]) {
                        coinPurse[2] -= itemStacks[2];
                        coinPurse[1]++;
                    }
                    return;           
            }
        } else {//Otherwise it will check the inventory if that item already exists, then sees if it can add it to the stack. Otherwise it will add it to the first available spot
            for (int i = 0; i < inventory.Length; i++) {
                if (inventory[i] == newItemID) {
                    if (inventoryStacks[i] < itemStacks[newItemID]) {
                        inventoryStacks[i]++;
                        Destroy(other.gameObject);
                        Debug.Log($"Picked up {itemName} in stack");
                        return;
                    }
                }
            }
            //This searches for an empty spot to make a new stack.
            for (int i = 0; i < inventory.Length; i++) {
                if (inventory[i] == -1) {
                    inventory[i] = newItemID;
                    inventoryStacks[i] = 1;
                    Destroy(other.gameObject);
                    Debug.Log($"Picked up {itemName} in new spot");
                    return;
                }
            }
        }
        inventoryFailedPickupCooldown = 2000;
        Debug.Log($"Could not pick up {itemName}");
        return;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dropped Item")){
            itemIDClass = other.GetComponent<ItemID>();
            itemID = itemIDClass.GetItemID();

            if (Input.GetKey(KeyCode.E))
            {
                if (inventoryFailedPickupCooldown <= 0) {
                    PutItemInInventory(itemID, other);
                    itemID = -1;
                } else if (messageCooldown <= 0) {
                    Debug.Log("inventory pickup failed, currently on cooldown");
                    messageCooldown = 500;
                }
                
            }
        }
    }

    private void Update() {
        if (inventoryFailedPickupCooldown > 0) {
            inventoryFailedPickupCooldown--;
            messageCooldown--;
        }
    }
}
