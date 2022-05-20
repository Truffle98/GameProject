using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Items
{
    private ItemID itemIDClass;
    private MenuTest menuTest;
    private float damage, armor = 0, mageArmorMultiplier = 1.5f;
    private int decision, itemID, itemArmorType, experienceTotal, IAClass;
    private string itemName;
    private Mage mage;
    //private Assassin assassin;

    public int[,] itemLineup = new int[8,2]{ {0, 0}, {-1, 5}, {0, 1}, {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1} };
    public int[,] armorLineup = new int[5,2] { {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1} };
    public int[,] inventory = new int[15, 2] { {0, 2}, {0, 3}, {-1, -1}, {-1, -1}, {-1, -1},
                               {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1},
                               {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1}, {-1, -1} };
    public int[] inventoryStacks = {1, 1, 1, 0, 0,
                                     0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0};
    private int[] coinPurse = {0, 0, 0};
    private int inventoryFailedPickupCooldown = 0, messageCooldown = 0;

    void Start()
    {
        mage = GameObject.Find("MageClass(Clone)").GetComponent<Mage>();
    }

    public bool isWeapon(int index, int itemClass)
    {
        if (itemClass >= 0)
        {
            return true;
        }
        else if (itemStats[index, 4] >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isArmor(int index, int itemClass)
    {
        if (itemClass<0 && itemStats[index, 5] >= 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public int GetEquippedItemClass(int index)
    {
        return itemLineup[index, 0];
    }
    public int GetEquippedItem(int indexInItemLineup)
    {
        return itemLineup[indexInItemLineup, 1];
    }
    public int GetInventoryItemClass(int index)
    {
        return inventory[index, 0];
    }
    public int GetItemInInventory(int indexInInventory){
        return inventory[indexInInventory, 1];
    }
    public int GetArmorItemClass(int index)
    {
        return armorLineup[index, 0];
    }
    public int GetItemInArmorLineup(int indexInArmorLineup)
    {
        return armorLineup[indexInArmorLineup, 1];
    }
    public int GetCoinStack(int coinType)
    {
        return coinPurse[coinType];
    }
    public float GetDamage(int indexInItemLineup)
    {
        //Uses indexes to access damage of item
        return GetItemDamage(itemLineup[indexInItemLineup, 1]);
    }
    public int GetItemStack(int indexInInventory)
    {
        return inventoryStacks[indexInInventory];
    }
    public void GetExperience(int experience) {
        experienceTotal += experience;
    }
    public void switchItemToInventory(int index, int itemID, int IAClass, string fromWhere)
    {
        itemName = itemNames[itemID];
        for (int i = 0; i < inventory.Length; i++) 
        {
            if (inventory[i,0] == -1 && inventory[i,1] == -1) {
                inventory[i,0] = IAClass;
                inventory[i, 1] = itemID;
                inventoryStacks[i] = 1;
                Debug.Log($"Picked up {itemName} in new spot {i+1}");
                if (fromWhere == "hot bar")
                {
                    itemLineup[index, 0] = -1;
                    itemLineup[index, 1] = -1;
                }
                else if (fromWhere == "armor")
                {
                    armorLineup[index, 0] = -1;
                    armorLineup[index, 1] = -1;
                }
                return;
            }
        }
    }
    public void switchItemToHotBar(int indexInInventory, int itemID, int IAClass)
    {
        if (IAClass<0)
        {
            itemName = itemNames[itemID];
        }
        else if (IAClass == 0)
        {
            itemName = mage.GetAbilityName(itemID);
        }
        else if (IAClass == 1)
        {
            //itemName =
        }
        for (int i = 0; i < itemLineup.Length; i++) 
        {
            if (itemLineup[i,0] == -1 && itemLineup[i,1] == -1) {
                itemLineup[i,0] = IAClass;
                itemLineup[i,1] = itemID;
                inventoryStacks[indexInInventory] -= 1;
                Debug.Log($"Picked up {itemName} in hot bar slot {i+1}");
                inventory[indexInInventory, 0] = -1;
                inventory[indexInInventory, 1] = -1;
                return;
            }
        }
    }
    public void SwitchItemToArmorEquip(int indexInInventory, int itemID, int IACLass)
    {
        if (IAClass<0)
        {
            itemName = itemNames[itemID];
        }
        else if (IAClass == 0)
        {
            itemName = mage.GetAbilityName(itemID);
        }
        else if (IAClass == 1)
        {
            //itemName =
        }
        itemArmorType = GetItemArmorType(itemID);
        if (IAClass == -1)
        {
            if (itemArmorType < 3)
            {
                if (armorLineup[itemArmorType, 0] == -1 && armorLineup[itemArmorType, 1] == -1) {
                    armorLineup[itemArmorType, 0] = IAClass;
                    armorLineup[itemArmorType, 1] = itemID;
                    inventoryStacks[indexInInventory] -= 1;
                    Debug.Log($"Picked up {itemName} in armor slot {itemArmorType+1}");
                    inventory[indexInInventory, 0] = -1;
                    inventory[indexInInventory, 1] = -1;
                    return;
                }
            }
            else
            {
                if (armorLineup[3,0] == -1 && armorLineup[3, 1] == -1) {
                    armorLineup[3,0] = IAClass;
                    armorLineup[3,1] = itemID;
                    inventoryStacks[indexInInventory] -= 1;
                    Debug.Log($"Picked up {itemName} in armor slot {itemArmorType+1}");
                    inventory[indexInInventory,0] = -1;
                    inventory[indexInInventory,1] = -1;
                    return;
                }
                else if (armorLineup[4,0] == -1 && armorLineup[4,1] == -1)
                {
                    armorLineup[4,0] = IAClass;
                    armorLineup[4,1] = itemID;
                    inventoryStacks[indexInInventory] -= 1;
                    Debug.Log($"Picked up {itemName} in armor slot {itemArmorType+1}");
                    inventory[indexInInventory,0] = -1;
                    inventory[indexInInventory,1] = -1;
                    return;
                }
            }
        }
    }
    public float GetArmor()
    {
        armor = 0;
        for (int armorIndex = 0; armorIndex<5; armorIndex++)
        {
            if (armorLineup[armorIndex, 1] != -1)
            {
                armor += GetItemArmor(armorLineup[armorIndex, 1]);
            }
        }
        return (float)armor*mageArmorMultiplier;
    }

    //This function is intended to work with the 2D collider that detects when an item is attempted to be picked up. If it can fit it in the inventory it will pick up the item, otherwise it will leave it on the ground.
    public void PutItemInInventory(int newItemID, int IAClass, Collider2D other) {
        if (IAClass<0)
        {
            itemName = itemNames[itemID];
        }
        else if (IAClass == 0)
        {
            itemName = mage.GetAbilityName(itemID);
        }
        else if (IAClass == 1)
        {
            //itemName =
        }
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
                    if (coinPurse[1] >= itemStats[1,1]) {
                        coinPurse[1] -= itemStats[1,1];
                        coinPurse[0]++;
                        
                    }
                    return;
                case 2:
                    coinPurse[2]++;
                    Destroy(other.gameObject);
                    if (coinPurse[2] >= itemStats[2,1]) {
                        coinPurse[2] -= itemStats[2,1];
                        coinPurse[1]++;
                    }
                    return;           
            }
        } else {//Otherwise it will check the inventory if that item already exists, then sees if it can add it to the stack. Otherwise it will add it to the first available spot
            for (int i = 0; i < 15; i++) {
                if (inventory[i,0] == IAClass && inventory[i,1] == newItemID) {
                    if (inventoryStacks[i] < itemStats[newItemID,1]) {
                        inventoryStacks[i]++;
                        Destroy(other.gameObject);
                        Debug.Log($"Picked up {itemName} in stack");
                        return;
                    }
                }
            }
            //This searches for an empty spot to make a new stack.
            for (int i = 0; i < 15; i++) {
                if (inventory[i,0] == -1 && inventory[i,1] == -1) {
                    inventory[i,0] = IAClass;
                    inventory[i,1] = newItemID;
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
            IAClass = itemIDClass.GetAbilityType();

            if (Input.GetKey(KeyCode.F))
            {
                if (inventoryFailedPickupCooldown <= 0) {
                    PutItemInInventory(itemID, IAClass, other);
                    itemID = -1;
                } else if (messageCooldown <= 0) {
                    Debug.Log("Inventory pickup failed, currently on cooldown");
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
