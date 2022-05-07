using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector3 shootDirection;
    private float enemyDamage, currentHealth;
    private int item1, cooldown = 0;
    private PlayerStats playerStats;
    private EnemyProjectileScript enemyProjectileScript;
    private GameObject item1Object;
    private Items itemsList;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();
        item1 = playerStats.GetEquippedItem(0);
        item1Object = itemsList.GetItemObject(item1);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Creates a fireball and puts it on a 2 second cooldown
        if (Input.GetMouseButtonDown(0) && cooldown == 0 && Time.timeScale == 1) {

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;

            Instantiate(item1Object, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
            cooldown = 100;

        }

        if (cooldown > 0 && Time.timeScale == 1) {
            cooldown--;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        //On collision with something, it checks if its a projectile. If it is, then it gets the damage from the projectile's script
        if(other.gameObject.CompareTag("Enemy Projectile")) {
            enemyProjectileScript = other.GetComponent<EnemyProjectileScript>();
            enemyDamage = enemyProjectileScript.GetProjectileDamage();
            Destroy(other.gameObject);
            currentHealth -= enemyDamage;
            if(currentHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
