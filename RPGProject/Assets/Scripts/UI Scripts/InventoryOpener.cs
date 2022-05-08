using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpener : MonoBehaviour
{

    private bool isOpen;
    private GameObject inventory;

    void Start(){
        isOpen = false;
        inventory = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.I)){
            if (isOpen){
                setActivation(false);
                isOpen = false;
            }
            else{
                setActivation(true);
                isOpen = true;
            }
        }
        inventory.GetComponent<InventoryScript>().UpdateInventory();
    }
    void setActivation(bool wantToBeActive){
        if(wantToBeActive){
            inventory.SetActive(true);
        }
        else{
            inventory.SetActive(false);
        }
    }
}
