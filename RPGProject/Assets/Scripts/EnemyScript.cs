using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private float maxHealth = 100, playerDamage, currentHealth;
    private int timer = 0;
    private bool count = true;
    public int itemDrop;
    private ProjectileScript projectileStats;
    //private Items itemsScript;
    private Items itemsList;

    public HealthBar1 healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (count) {
            timer++;
        }

        if (timer > 100) {
            playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
            itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();
            count = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        //On collision with something, it checks if its a projectile. If it is, then it gets the damage from the projectile's script
        if(other.gameObject.CompareTag("Projectile")) {
            projectileStats = other.GetComponent<ProjectileScript>();
            playerDamage = projectileStats.GetProjectileDamage();
            Destroy(other.gameObject);
            currentHealth -= playerDamage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0) {
                Instantiate(itemsList.GetItemObject(itemDrop), transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
        }
    }
}
