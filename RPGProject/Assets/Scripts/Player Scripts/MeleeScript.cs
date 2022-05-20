using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public int itemID;
    public int abilityClass; //-1: classless melee item, 0: mage, 1: assassin
    public float damage;
    private int classDecision;
    private Vector3 shootDirection, startingPosition;
    private PlayerStats playerStats;
    private GameObject player;
    private Mage mage;
    private Assassin assassin;

    public float GetDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
    
        player = GameObject.FindWithTag("Character");
        /*
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        classDecision = 1;
        if (classDecision == 0)
        {
            mage = GameObject.Find("MageClass(Clone)").GetComponent<Mage>();
        }
        else if (classDecision == 1)
        {
            assassin = GameObject.Find("Assassin(Clone)").GetComponent<Assassin>();
        }

        if (abilityClass == 0)
        {
            damage = mage.GetAbilityDamage(itemID);
        }
        else if (abilityClass == 1)
        {
            damage = assassin.GetAbilityDamage(itemID);
        }
        else 
        {
            damage = playerStats.GetItemDamage(itemID);
        }*/

        /*shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection.normalized;*/
        //startingPosition = player.transform.position;

        Destroy(gameObject, 0.15f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(shootDirection * 10 * Time.deltaTime);
        transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 500 * Time.deltaTime);

    }
}
