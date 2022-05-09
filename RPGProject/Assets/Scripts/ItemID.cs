using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID : MonoBehaviour
{
    public int itemID;

    public int GetItemID()
    {
        return itemID;
    }

    void Update() {
        transform.RotateAround(transform.position, Vector3.up, 50 * Time.deltaTime);
    }
    
}
