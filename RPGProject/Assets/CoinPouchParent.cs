using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPouchParent : MonoBehaviour
{
    public GameObject[] coinStacks;
    private textEditorScript textEditor;
    private PlayerStats playerStats;
    private Items items;
    private int coinStackNum, count = 0;
    private string coinStackString;
    private Sprite itemSprite;

    void Update()
    {
        if (count == 0)
        {
            gameObject.SetActive(false);
            count++;
        }
    }

    public void UpdateCoinPouch(){
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        
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
