using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEditor : MonoBehaviour
{
    private Image img;

    public void SetSprite(Sprite itemSprite){

        img = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        img.enabled = true;
        img.sprite = itemSprite;
    }
}
