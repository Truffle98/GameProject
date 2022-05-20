using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeOpener : MonoBehaviour
{
    private bool isOpen;
    private GameObject abilityParent;

    void Start()
    {
        isOpen = false;
        abilityParent = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)){
            if (isOpen){
                setActivation(false);
                isOpen = false;
            }
            else{
                setActivation(true);
                isOpen = true;
            }
        }
        //abilityParent.GetComponent<ArmorEquipScript>().UpdateArmorEquip();
    }

    void setActivation(bool wantToBeActive){
        if(wantToBeActive){
            abilityParent.SetActive(true);
        }
        else{
            abilityParent.SetActive(false);
        }
    }

    public bool AbilityTreeOpenState()
    {
        return isOpen;
    }
}
