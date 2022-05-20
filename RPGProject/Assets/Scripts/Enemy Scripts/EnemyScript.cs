using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameObject player, newObject, target;
    public GameObject enemyProjectile;
    private float currentHealth, speed, AOEDamage, angle, distanceToTarget;
    private int timer = 0, cooldown, findNewLocationTimer, AOECooldown = 0, itemDrop = -1, itemDropChoice, itemBound = 0, startBound = 0, turnedDuration = 0;
    private int[] scaledItemDropList = new int[100];
    private bool count = true, targetSpotted, turned = false;
    public int cooldownMax, maxHealth = 100, experience, lastSeenTarget = 0;
    //Drop list should contain item ids and item drop percentages list should contain the percentages in integer from (90 for 90% for example)
    public int[] itemDropList;
    public int[] itemDropPercentagesList;
    public float engageDistance, shootDistance, maxSpeed, fleeDistance;
    private ProjectileScript projectileStats;
    private MeleeScript meleeWeaponStats;
    private Vector3 shootDirection, direction;
    private Items itemsList;
    private Rigidbody2D rb;
    private Vector2 movement;
    private NavMeshAgent agent;
    private NavMeshHit hit;
    private AOEScript AOEScript;
    //public bool shouldRotate = false;

    public HealthBar1 healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = this.GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();
        player = GameObject.FindWithTag("Character");
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (count) {
            timer++;
        }

        if (cooldown > 0 && Time.timeScale == 1) {
            cooldown--;
        }

        if (AOECooldown > 0 && Time.timeScale == 1) {
            AOECooldown--;
        }

        if (findNewLocationTimer > 0) {
            findNewLocationTimer--;
        }

        if (!agent.Raycast(target.transform.position, out hit)) {
            lastSeenTarget = 1250;
        } else {
            lastSeenTarget--;
        }

        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        
        if (distanceToTarget < engageDistance && Time.timeScale == 1 && lastSeenTarget > 0) {
            targetSpotted = true;
        } else {
            targetSpotted = false;
        }
        MovementDirection(targetSpotted);

        if (targetSpotted) {
            
            if (distanceToTarget < shootDistance && Time.timeScale == 1) {
                if (cooldown <= 0 && !agent.Raycast(target.transform.position, out hit)) {
                    ShootTarget();
                }
            }
            
        }

        if (turned) {
            if (turnedDuration > 0) {
                turnedDuration--;
            } else {
                target = player;
                turned = false;
            }
        }
    }

    void FixedUpdate() {

        if (targetSpotted) {
            if (distanceToTarget > shootDistance - 1) {
                agent.SetDestination(target.transform.position);
                //MoveCharacter(movement);
            } else if (distanceToTarget < fleeDistance) {
                agent.SetDestination(transform.position - target.transform.position);
            } else if (distanceToTarget < shootDistance - 1 && !agent.Raycast(target.transform.position, out hit)) {
                agent.ResetPath();
            }
               
        } else if (Time.timeScale == 1) {
            agent.ResetPath();
            MoveCharacter(movement);
        }

        //transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //On collision with something, it checks if its a projectile. If it is, then it gets the damage from the projectile's script
        if(other.gameObject.CompareTag("Projectile")) {
            TakeDamage(other.GetComponent<ProjectileScript>().GetProjectileDamage());
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Sword"))
        {
            TakeDamage(other.GetComponent<MeleeScript>().GetDamage());
        }
        else if (other.gameObject.CompareTag("Turned Enemy Projectile")) 
        {
            TakeDamage(other.GetComponent<EnemyProjectileScript>().GetProjectileDamage());
            Destroy(other.gameObject);
        }
        /*if(other.gameObject.CompareTag("Wave")) {

            rb.isKinematic = false;

        }*/
    }

    void OnTriggerStay2D(Collider2D other) {

        if (other.gameObject.CompareTag("AOE")) {

            if (AOECooldown <= 0) {

                AOEScript = other.GetComponent<AOEScript>();
                AOEDamage = AOEScript.GetAOEDamage();
                TakeDamage(AOEDamage);
                AOECooldown = 175;

            }
        }
    }

    /*void OnTriggerExit2D(Collider2D other) {

        if(other.gameObject.CompareTag("Wave")) {

            rb.isKinematic = true;

        }

    }*/

    private void MovementDirection(bool spotted) {
        
        if (spotted == true) {
            direction = target.transform.position - transform.position;
            speed = maxSpeed;
        } else if (findNewLocationTimer <= 0) {
            direction = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
            speed = maxSpeed/2;
            findNewLocationTimer = 100;
        }
        /*if (shouldRotate = true) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 90;
        }*/
        direction.Normalize();
        movement = direction;
        return;

    }

    private void MoveCharacter (Vector2 direction) {

        rb.MovePosition((Vector2)transform.position + (direction * maxSpeed * Time.deltaTime));
        return;

    }

    private void ShootTarget () {
        shootDirection = target.transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection-transform.position;
        shootDirection = shootDirection.normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

        newObject = Instantiate(enemyProjectile, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
        newObject.GetComponent<EnemyProjectileScript>().angle = angle;
        if (turned) {
            newObject.tag = "Turned Enemy Projectile";
        }
        cooldown = cooldownMax;
        return;
    }

    public void Die ()
    {
        for (int index = 0; index < itemDropList.Length; index++)
        {
            itemBound = itemDropPercentagesList[index];
            for (int index2 = startBound; index2 < itemBound+startBound; index2++)
            {
                scaledItemDropList[index2] = itemDropList[index];
            }
            startBound += itemBound;
        }
        itemDropChoice = Random.Range(0, 100);
        itemDrop = scaledItemDropList[itemDropChoice];

        Instantiate(itemsList.GetItemObject(itemDrop), transform.position, new Quaternion(0, 0, 0, 0));
        playerStats.GetExperience(experience);
        Destroy(gameObject);

    }

    public void TakeDamage(float damageTaken) {

        currentHealth -= damageTaken;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void ChangeSides() {

        turned = true;
        turnedDuration = 1000;
        float lowestDistance = Mathf.Infinity;
        GameObject potentialTarget = null;
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (potentialTargets.Length > 0) {
            for (int i = 0; i < potentialTargets.Length; i++) {
                if (Vector3.Distance(transform.position, potentialTargets[i].transform.position) < lowestDistance) {
                    potentialTarget = potentialTargets[i];
                }
            }
            target = potentialTarget;
        }
        Debug.Log("yes");
    }
}
