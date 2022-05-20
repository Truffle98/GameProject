using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed, lifespan, angle, damage;
    public int itemID;
    public int abilityClass; //-1: classless projectile item, 0: mage, 1: assassin
    private int classDecision;
    private Vector3 shootDirection;
    private PlayerStats playerStats;
    private Mage mage;
    private Assassin assassin;
    
    public float GetProjectileDamage() {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Determines direction of fireball and sets lifespan

        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
        GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
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
        }
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        //transform.Translate(shootDirection * speed * Time.deltaTime);
    }*/

    void OnTriggerEnter2D(Collider2D other) {

        //Checks for collisions. If fireball projectile collides with walls it destroys itself
        if(other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }

    /*void Move(float angle, float speed) {

        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
        GetComponent<Rigidbody2D>().velocity = shootDirection * speed;

    }*/
}
