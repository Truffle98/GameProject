using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorEditor : MonoBehaviour
{
    public Button button;
    public Sprite spotHolder;
    public int armorEquipSlot;
    private int itemID, cooldown = 0, itemType;
    private PlayerStats playerStats;
    private Image img;

    public void SetSprite(Sprite itemSprite, int ID, int type){
        img.sprite = itemSprite;
        itemID = ID;
        itemType = type;
    }

    void Start()
    {
        img = gameObject.GetComponent<Image>();
        img.enabled = true;
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
    }

    void Update()
    {
        gameObject.GetComponent<Button>().onClick.AddListener( () => 
        {
            if (img.sprite.name != spotHolder.name && cooldown==0)
            {
                img.sprite = spotHolder;
                playerStats.switchItemToInventory(armorEquipSlot, -1, itemID, "armor");
                cooldown = 50;
            }
        });

        if (cooldown>0)
        {
            cooldown--;
        }
    }
}
