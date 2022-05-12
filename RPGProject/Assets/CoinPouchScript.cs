using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPouchScript : MonoBehaviour
{
    private bool isOpen;
    private GameObject coinPouchParent;

    void Start()
    {
        isOpen = true;
        coinPouchParent = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)){
            if (isOpen){
                setActivation(false);
                isOpen = false;
            }
            else{
                setActivation(true);
                isOpen = true;
            }
        }
        coinPouchParent.GetComponent<CoinPouchParent>().UpdateCoinPouch();
    }

    void setActivation(bool wantToBeActive){
        if(wantToBeActive){
            coinPouchParent.SetActive(true);
        }
        else{
            coinPouchParent.SetActive(false);
        }
    }
}
