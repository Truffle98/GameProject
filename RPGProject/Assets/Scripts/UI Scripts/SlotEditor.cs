using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEditor : MonoBehaviour
{
    public GameObject textMeshPro;
    public Button botton;
    public Sprite spotHolder;
    public int inventoryIndex;
    private int cooldown = 0, itemID, armorType, itemClass, itemType;
    private float damage, armor;
    private string stackNum;
    private Image img;
    private PlayerStats playerStats;
    [SerializeField] private Items items;
    private textEditorScript textEditor;

    public void SetSprite(Sprite itemSprite, int ID, int iClass)
    {
        img.enabled = true;
        img.sprite = itemSprite;
        itemID = ID;
        damage = items.GetItemDamage(itemID);
        armor = items.GetItemArmor(itemID);
        armorType = items.GetItemArmorType(itemID);
        itemClass = iClass;
    }

    void Start()
    {
        img = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
    }

    void Update()
    {
        gameObject.GetComponent<Button>().onClick.AddListener( () => 
        {
            if (img.enabled == true && cooldown==0 && playerStats.isWeapon(itemID, itemClass)){
                img.enabled = false;
                playerStats.switchItemToHotBar(inventoryIndex, itemID, itemClass);
                cooldown = 50;

                stackNum = playerStats.GetItemStack(inventoryIndex).ToString();
                textEditor = textMeshPro.GetComponent<textEditorScript>();
                textEditor.ChangeStackNumber(null);
            }

            else if (img.enabled == true && cooldown==0 && playerStats.isArmor(itemID, itemClass)){
                img.enabled = false;
                playerStats.SwitchItemToArmorEquip(inventoryIndex, itemID, itemClass);
                cooldown = 50;
                
                stackNum = playerStats.GetItemStack(inventoryIndex).ToString();
                textEditor = textMeshPro.GetComponent<textEditorScript>();
                textEditor.ChangeStackNumber(null);
            }
        });

        if (cooldown>0)
        {
            cooldown--;
        }
    }
}
