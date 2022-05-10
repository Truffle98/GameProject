using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarScript : MonoBehaviour
{
    public GameObject[] slots;
    private HotBarEditor hotBarEditor;
    private PlayerStats playerStats;
    private Items items;
    private int itemID;
    private Sprite itemSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int hotBarIndex = 0; hotBarIndex<4; hotBarIndex++){
            itemID = playerStats.GetEquippedItem(hotBarIndex);
            if (itemID > 0){
                itemSprite = items.GetItemObject(itemID).GetComponent<SpriteRenderer>().sprite;
                hotBarEditor = slots[hotBarIndex].GetComponent<HotBarEditor>();
                hotBarEditor.SetSprite(itemSprite, itemID);
            }
        }
    }
}
