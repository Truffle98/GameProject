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
    private int itemID, itemClass;
    private Sprite itemSprite;
    public Mage mage;
    public Assassin assassin;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int hotBarIndex = 0; hotBarIndex<slots.Length; hotBarIndex++){
            itemID = playerStats.GetEquippedItem(hotBarIndex);
            itemClass = playerStats.GetEquippedItemClass(hotBarIndex);
            if (itemID >= 0){
                if (itemClass == 0)
                {
                    itemSprite = mage.GetAbilityObject(itemID).GetComponent<SpriteRenderer>().sprite;

                }
                else if (itemClass == 1)
                {
                    assassin = GameObject.Find("Assassin(Clone)").GetComponent<Assassin>();
                    itemSprite = assassin.GetAbilityObject(itemID).GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    itemSprite = items.GetItemObject(itemID).GetComponent<SpriteRenderer>().sprite;
                }
                hotBarEditor = slots[hotBarIndex].GetComponent<HotBarEditor>();
                hotBarEditor.SetSprite(itemSprite, itemID, itemClass);
            }
        }
    }
}
