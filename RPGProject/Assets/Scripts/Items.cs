using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private string[] itemNames = new string[] {"Gold Coin", "Silver Coin", "Copper Coin"};
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itemNames[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
