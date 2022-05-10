using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarEditor : MonoBehaviour
{
    public Button button;
    public Sprite spotHolder;
    public int hotBarSlot;
    private int itemID, cooldown = 0;
    private PlayerStats playerStats;
    private Image img;

    public void SetSprite(Sprite itemSprite, int ID){
        img.sprite = itemSprite;
        itemID = ID;
    }

    public string getHotBarSprite() {
        return img.sprite.name;
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
            if (img.sprite.name != spotHolder.name && cooldown==0){
                img.sprite = spotHolder;
                playerStats.switchItemToInventory(hotBarSlot, itemID);
                cooldown = 50;
            }
        });

        if (cooldown>0)
        {
            cooldown--;
        }
    }

}
