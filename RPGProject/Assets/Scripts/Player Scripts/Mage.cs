using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Mage : BaseClass
{
    public float manaRegenerationSpeed;
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana, angle;
    private Vector3 shootDirection;
    private int cooldown = 0, item1, item2;
    private Rigidbody2D body;
    private Animator anim;
    private bool movingUp;
    private bool movingDown;
    private PlayerStats playerStats;
    private Items itemsList;
    private GameObject item1Object, item2Object, newSword;
    public ManaBar manaBar;
    private EnemyProjectileScript enemyProjectileScript;

    public HealthBar1 healthbar;

    public float GetMaxMana()
    {
        return maxMana;
    }

    private void Start()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();

        item1 = playerStats.GetEquippedItem(0);
        item1Object = itemsList.GetItemObject(item1);
        item2 = playerStats.GetEquippedItem(1);
        item2Object = itemsList.GetItemObject(item2);
        //Right now we know that the second item is a melee item. However, in the future if we don't know what type an object is [melee, projectile, etc] we might need to
        //add another script to the item game object that returns the item type [GetItemTypeScript for example]

        manaRegenerationSpeed = .02f;

        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        manaBar.SetMaxMana(maxMana);
        currentMana = maxMana;
    }

    //Runs every frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Sets character velocity
        body.velocity = new Vector2(horizontalInput * baseSpeed, verticalInput * baseSpeed);

        //Flips sprites and helps facilitate sprite transitions
        changeSprite(horizontalInput, verticalInput);

        //Set animator parameters
        anim.SetBool("Running", horizontalInput != 0);
        anim.SetBool("UpRunning", movingUp);
        anim.SetBool("DownRunning", movingDown);

        Attack();
    }

    //Handles animation changes
    private void changeSprite(float horizontalInput, float verticalInput)
    {
        if (horizontalInput > .01f)
        {
            transform.localScale = new Vector3(-6, 6, 6);
        }
        else if (horizontalInput < -.01f)
        {
            transform.localScale = Vector3.one * 6;
        }

        if (verticalInput > .01f)
        {
            movingUp = true;
        }
        else if (verticalInput == 0)
        {
            if (movingUp)
            {
                movingUp = false;
            }
            else if (movingDown)
            {
                movingDown = false;
            }
        }
        else if (verticalInput < -.01f)
        {
            movingDown = true;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && cooldown == 0 && Time.timeScale == 1) 
        {
            if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(0)))
            {
                shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection-transform.position;
                shootDirection = shootDirection.normalized;
                
                Instantiate(item1Object, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
                cooldown = 100;

                currentMana -= itemsList.GetManaCost(playerStats.GetEquippedItem(0));
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetMouseButtonDown(1) && cooldown == 0 && Time.timeScale == 1)
        {
            if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
            {
                //sets the sprite of the sword in mage as visable when you attack. the change in scale is messed up when you face one side vs the other

                shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection-transform.position;
                shootDirection.x *= 2.2f;
                shootDirection.y *= 2.2f;
                shootDirection = shootDirection.normalized;
                angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

                if (Mathf.Abs(shootDirection.x) < 0.3) {
                    shootDirection.x *= 2.5f;
                } else if (Mathf.Abs(shootDirection.x) < 0.5) {
                    shootDirection.x *= 1.5f;
                }

                if (Mathf.Abs(shootDirection.y) < 0.3) {
                    shootDirection.y *= 2.5f;
                } else if (Mathf.Abs(shootDirection.y) < 0.5) {
                    shootDirection.y *= 1.5f;
                }

                newSword = Instantiate(item2Object, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(0, 0, angle - 45));
                newSword.transform.parent = gameObject.transform;

                //less cooldown than a spell
                cooldown = 50;
                currentMana -= itemsList.GetManaCost(playerStats.GetEquippedItem(1));
            }
        }

        if (currentMana<maxMana && Time.timeScale == 1) 
        {
            currentMana += manaRegenerationSpeed;
        }

        if (cooldown > 0 && Time.timeScale == 1) {
            cooldown--;
        }

        manaBar.SetMana(currentMana);
    }

    void OnTriggerEnter2D(Collider2D other) {
        //On collision with something, it checks if its a projectile. If it is, then it gets the damage from the projectile's script
        if(other.gameObject.CompareTag("Enemy Projectile")) {
            enemyProjectileScript = other.GetComponent<EnemyProjectileScript>();
            enemyDamage = enemyProjectileScript.GetProjectileDamage();
            Destroy(other.gameObject);
            currentHealth -= enemyDamage;

            healthbar.SetHealth(currentHealth);

            if(currentHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
