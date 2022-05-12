using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPouchParent : MonoBehaviour
{
    public GameObject[] coinStacks;
    private SlotEditor slotEditor;
    private textEditorScript textEditor;
    private PlayerStats playerStats;
    private Items items;
    private int coinStackNum, itemID;
    private string coinStackString;
    private Sprite itemSprite;

    public void UpdateCoinPouch(){
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        items = GameObject.Find("ItemObjectList").GetComponent<Items>();
        
        for (int coinPouchIndex = 0; coinPouchIndex < 3; coinPouchIndex++){
            coinStackNum = playerStats.GetCoinStack(coinPouchIndex);
            if (coinStackNum>0){
                coinStackString = coinStackNum.ToString();
                textEditor = coinStacks[coinPouchIndex].GetComponent<textEditorScript>();
                textEditor.ChangeStackNumber(coinStackString);
            }
        }
    }
}
