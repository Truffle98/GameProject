using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEScript : MonoBehaviour
{
    public int itemID;
    private PlayerStats playerStats;
    private float damage;
    
    public float GetAOEDamage() {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        damage = playerStats.GetItemDamage(itemID) * 0.2f;
        Destroy(gameObject, 2);
    }

}
