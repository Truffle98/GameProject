using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private ItemID itemIDClass;
    private int itemID;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dropped Item")){
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("worked");
                //itemIDClass = other.GetComponent<ItemID>();
                //itemID = itemIDClass.GetItemID();
                Destroy(other.gameObject);
            }
        }
    }
}
