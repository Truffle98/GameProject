using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseClass
{
    private MenuTest menuTest;
    private float cringeDamage;
    private int decision;

    public float GiveDamage() {

        return cringeDamage;

    }
    // Start is called before the first frame update
    void Start()
    {
        menuTest = GameObject.Find("Test").GetComponent<MenuTest>();
        decision = menuTest.ReturnDecision();
        if (decision == 0) {
            cringeDamage = 15;
        } else if (decision == 1) {
            cringeDamage = 25;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
