using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarEditor : MonoBehaviour
{
    private Image img;
    public void SetSprite(Sprite itemSprite){

        img = gameObject.GetComponent<Image>();
        img.enabled = true;
        img.sprite = itemSprite;
    }
}
