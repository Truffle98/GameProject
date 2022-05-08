using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameObject player;
    public GameObject enemyProjectile;
    private float playerDamage, currentHealth, speed;
    private int timer = 0, cooldown, findNewLocationTimer;
    private bool count = true, playerSpotted;
    public int itemDrop = -1, cooldownMax, maxHealth = 100, movementPattern;
    public float engageDistance, shootDistance, maxSpeed;
    private ProjectileScript projectileStats;
    private MeleeScript1 meleeWeaponStats;
    private Vector3 shootDirection, direction;
    private Items itemsList;
    private Rigidbody2D rb;
    private Vector2 movement;
    //public bool shouldRotate = false;

    public HealthBar1 healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = this.GetComponent<Rigidbody2D>();
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
            player = GameObject.FindWithTag("Character");
            count = false;
        }

        if (cooldown > 0 && Time.timeScale == 1) {
            cooldown--;
        }

        if (findNewLocationTimer > 0) {
            findNewLocationTimer--;
        }

        if (!count) {
            
            if (Vector3.Distance(player.transform.position, transform.position) < shootDistance && Time.timeScale == 1) {
                playerSpotted = true;
            } else {
                playerSpotted = false;
            }
            MovementDirection(playerSpotted);
        }

        if (playerSpotted) {
            
            if (cooldown <= 0) {
                ShootPlayer();
            }
            
        }
    }

    void FixedUpdate() {

        if (playerSpotted) {
            if (movementPattern == 0) {
                MoveCharacter(movement);
            } else if (movementPattern == 1) {
                MoveCharacter(-movement);
            }
        } else if (Time.timeScale == 1) {
            MoveCharacter(movement);
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
        else if (other.gameObject.CompareTag("Long Sword"))
        {
            meleeWeaponStats = other.GetComponent<MeleeScript1>();
            playerDamage = meleeWeaponStats.GetDamage();
            Destroy(other.gameObject);
            currentHealth -= playerDamage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0) {
                Instantiate(itemsList.GetItemObject(itemDrop), transform.position, new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
        }
    }

    void MovementDirection(bool spotted) {
        
        if (spotted == true) {
            direction = player.transform.position - transform.position;
            speed = maxSpeed;
        } else if (findNewLocationTimer <= 0) {
            direction = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
            speed = maxSpeed/2;
            findNewLocationTimer = 500;
        }
        /*if (shouldRotate = true) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 90;
        }*/
        direction.Normalize();
        movement = direction;
        return;

    }

    void MoveCharacter (Vector2 direction) {

        rb.MovePosition((Vector2)transform.position + (direction * maxSpeed * Time.deltaTime));
        return;

    }

    void ShootPlayer () {
        shootDirection = player.transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection-transform.position;
        shootDirection = shootDirection.normalized;

        Instantiate(enemyProjectile, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
        cooldown = cooldownMax;
        return;
    }
}
