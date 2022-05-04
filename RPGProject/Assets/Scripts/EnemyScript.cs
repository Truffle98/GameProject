using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private float health = 100;
    private float playerDamage;
    private int timer = 0;
    private bool count = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == true) {
            timer++;
        }

        if (timer > 100) {
            playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
            playerDamage = playerStats.GiveDamage();
            count = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Projectile")) {
            health -= playerDamage;
            Debug.Log(health);
            if(health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
