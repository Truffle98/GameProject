using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Mage : BaseClass
{
    public float manaRegenerationSpeed;
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana;
    private Vector3 shootDirection;
    private int cooldown = 0, item1, item2;
    private Rigidbody2D body;
    private Animator anim;
    private bool movingUp;
    private bool movingDown;
    private PlayerStats playerStats;
    private Items itemsList;
    private GameObject item1Object, item2Object;
    public ManaBar manaBar;
    private EnemyProjectileScript enemyProjectileScript;
    private MeleeScript1 meleeScript;

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
        meleeScript = item2Object.GetComponent<MeleeScript1>();

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
            gameObject.transform.GetChild(3).gameObject.transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -.01f)
        {
            transform.localScale = Vector3.one * 6;
            gameObject.transform.GetChild(3).gameObject.transform.localScale = new Vector3(-1, 1, 1);
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

        else if (Input.GetMouseButtonDown(1) && cooldown == 0 && Time.timeScale == 1)
        {
            if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
            {
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
                shootDirection = Input.mousePosition;
                shootDirection.z = 0.0f;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = shootDirection-transform.position;
                shootDirection = shootDirection.normalized;

                Instantiate(item2Object, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
                //item2Object.SetActive(false);

                cooldown = 50;
                currentMana -= itemsList.GetManaCost(playerStats.GetEquippedItem(1));
            }
        }
        if (cooldown == 25)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (currentMana<maxMana) 
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
