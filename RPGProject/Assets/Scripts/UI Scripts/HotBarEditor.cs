using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarEditor : MonoBehaviour
{
    public Button button;
    public Sprite spotHolder;
    public int hotBarSlot;
    private int itemID, itemClass, cooldown = 0;
    private PlayerStats playerStats;
    private InventoryOpener inventoryOpener;
    private Image img;
    private bool inventoryIsOpen = false;

    public void SetSprite(Sprite itemSprite, int ID, int iClass){
        img.sprite = itemSprite;
        itemID = ID;
        itemClass = iClass;
    }

    public string getHotBarSprite() {
        return img.sprite.name;
    }

    void Start()
    {
        img = gameObject.GetComponent<Image>();
        img.enabled = true;
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        inventoryOpener = GameObject.Find("InventoryOpener").GetComponent<InventoryOpener>();
    }

    void Update()
    {
        inventoryIsOpen = inventoryOpener.InventoryOpenState();
        gameObject.GetComponent<Button>().onClick.AddListener( () => 
        {
            if (img.sprite.name != spotHolder.name && cooldown==0 && inventoryIsOpen){
                img.sprite = spotHolder;
                playerStats.switchItemToInventory(hotBarSlot, itemID, itemClass, "hot bar");
                cooldown = 50;
            }
        });

        if (cooldown>0)
        {
            cooldown--;
        }
    }

}
