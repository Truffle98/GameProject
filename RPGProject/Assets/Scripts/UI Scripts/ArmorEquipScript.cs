using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorEquipScript : MonoBehaviour
{
    public GameObject[] armorSlots;
    private ArmorEditor armorEditor;
    private PlayerStats playerStats;
    private Items items;
    private int itemID, count=0;
    private Sprite itemSprite;

    void Update()
    {
        if (count == 0)
        {
            gameObject.SetActive(false);
            count++;
        }
    }

    public void UpdateArmorEquip(){
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
        
        for (int armorEquipIndex = 0; armorEquipIndex < 5; armorEquipIndex++){
            itemID = playerStats.GetItemInArmorLineup(armorEquipIndex);
            if (itemID>0){
                itemSprite = items.GetItemObject(itemID).GetComponent<SpriteRenderer>().sprite;
                armorEditor = armorSlots[armorEquipIndex].GetComponent<ArmorEditor>();
                armorEditor.SetSprite(itemSprite, itemID);
            }
        }
    }
}
