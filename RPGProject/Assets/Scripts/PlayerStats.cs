using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseClass
{
    private MenuTest menuTest;
    private float damage;
    private int decision;

    public float GetDamage() {

        return damage;

    }
    // Start is called before the first frame update
    void Start()
    {
        menuTest = GameObject.Find("Test").GetComponent<MenuTest>();
        decision = menuTest.ReturnDecision();
        if (decision == 0) {
            damage = 4 * baseDamage;
        } else if (decision == 1) {
            damage = 8 * baseDamage;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
