using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject[] slots;
    private SlotEditor slotEditor;
    private PlayerStats playerStats;
    private Items items;
    private int itemID;
    private Sprite itemSprite;

    public void UpdateInventory(){
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
        
        for (int inventoryIndex = 0; inventoryIndex < 15; inventoryIndex++){
            itemID = playerStats.GetItemInInventory(inventoryIndex);
            if (itemID>0){
                itemSprite = items.GetItemObject(itemID).GetComponent<SpriteRenderer>().sprite;
                slotEditor = slots[inventoryIndex].GetComponent<SlotEditor>();
                slotEditor.SetSprite(itemSprite);
            }
        }
    }
}
