using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] stacks;
    private SlotEditor slotEditor;
    private textEditorScript textEditor;
    private PlayerStats playerStats;
    private Items items;
    private string stackNum;
    private int itemID, itemClass, count = 0;
    private Sprite itemSprite;
    public Mage mage;
    private Assassin assassin;

    void Update()
    {
        if (count == 0)
        {
            gameObject.SetActive(false);
            count++;
        }
    }

    public void UpdateInventory(){
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
        
        for (int inventoryIndex = 0; inventoryIndex < 15; inventoryIndex++){
            itemID = playerStats.GetItemInInventory(inventoryIndex);
            itemClass = playerStats.GetInventoryItemClass(inventoryIndex);

            if (itemID>=0){
                if (itemClass == 0)
                {
                    itemSprite = mage.GetAbilityObject(itemID).GetComponent<SpriteRenderer>().sprite;

                }
                else if (itemClass == 1)
                {
                    assassin = GameObject.Find("Assassin").GetComponent<Assassin>();
                    itemSprite = assassin.GetAbilityObject(itemID).GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    itemSprite = items.GetItemObject(itemID).GetComponent<SpriteRenderer>().sprite;
                }

                slotEditor = slots[inventoryIndex].GetComponent<SlotEditor>();
                slotEditor.SetSprite(itemSprite, itemID, itemClass);

                stackNum = playerStats.GetItemStack(inventoryIndex).ToString();
                textEditor = stacks[inventoryIndex].GetComponent<textEditorScript>();
                textEditor.ChangeStackNumber(stackNum);
            }
        }
    }
}
