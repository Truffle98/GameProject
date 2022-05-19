using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID : MonoBehaviour
{
    public int itemID;
    public int itemType;
    public int abilityType;

    public int GetItemID()
    {
        return itemID;
    }

    public int GetItemType()
    {
        return itemType;
    }

    public int GetAbilityType()
    {
        return abilityType;
    }

    void Update() {
        transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);
    }
    
}
