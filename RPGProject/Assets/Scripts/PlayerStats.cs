using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseClass
{
    private MenuTest menuTest;
    private float damage;
    private int decision;

    //Lists for four usable items [id] in your 'item lineup,' items [id] in armor linup, and all items [id] in inventory
    private int[] itemLineup = { 0, 0, 0, 0 };
    private int[] armorLinup = { 0, 0, 0 };
    private int[] inventory = {0, 0, 0, 0, 0,
                               0, 0, 0, 0, 0,
                               0, 0, 0, 0, 0,};

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
