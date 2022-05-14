using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEquipOpener : MonoBehaviour
{
    private bool isOpen;
    private GameObject armorParent;

    void Start()
    {
        isOpen = false;
        armorParent = gameObject.transform.GetChild(0).gameObject;
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
        armorParent.GetComponent<ArmorEquipScript>().UpdateArmorEquip();
    }

    void setActivation(bool wantToBeActive){
        if(wantToBeActive){
            armorParent.SetActive(true);
        }
        else{
            armorParent.SetActive(false);
        }
    }
}
