using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEScript : MonoBehaviour
{
    public int classType;
    public int itemID;
    public float lifespan;
    private PlayerStats playerStats;
    private Assassin assassin;
    private Mage mage;
    private float damage;
    
    public float GetAOEDamage() {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        classType = 1;
        if (classType == 0)
        {
            mage = GameObject.Find("Mage(Clone)").GetComponent<Mage>();
            damage = mage.GetAbilityDamage(itemID) * 0.2f;
        }
        else if (classType == 1)
        {
            assassin = GameObject.Find("Assassin(Clone)").GetComponent<Assassin>();
            damage = assassin.GetAbilityDamage(itemID) * 0.2f;
        }
        else
        {
            playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
            damage = playerStats.GetItemDamage(itemID) * 0.2f;
        }
        Destroy(gameObject, lifespan);
    }

}
