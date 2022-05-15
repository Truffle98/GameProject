using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Mage : BaseClass
{
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana, angle, manaRegenerationSpeed;
    private Vector3 shootDirection;
    private int cooldown1 = 0, cooldown2 = 0, cooldown3 = 0, cooldown4 = 0, item1, item2, item3, item4, itemType;
    public Rigidbody2D body;
    private Animator anim;
    private bool movingUp, movingDown, inventoryOrArmorEquipOpen;
    private PlayerStats playerStats;
    private InventoryOpener inventoryOpener;
    private ArmorEquipOpener armorEquipOpener;
    private Items itemsList;
    public GameObject[] itemCooldowns;
    private CooldownUI cooldownUI;
    private GameObject item1Object, item2Object, item3Object, item4Object, newMelee, newProjectile;
    public ManaBar manaBar;
    public bool dashing = false;
    private EnemyProjectileScript enemyProjectileScript;
    private float mageArmorMultiplier = baseArmorMultiplier + 0.5f;

    public HealthBar1 healthbar;

    public void Heal (float healthHealed) {
        currentHealth += healthHealed;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        healthbar.SetHealth(currentHealth);
    }

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
        inventoryOpener = GameObject.Find("InventoryOpener").GetComponent<InventoryOpener>();
        armorEquipOpener = GameObject.Find("Armor Equip Opener").GetComponent<ArmorEquipOpener>();

        item1 = playerStats.GetEquippedItem(0);
        item1Object = itemsList.GetItemObject(item1);
        item2 = playerStats.GetEquippedItem(1);
        item2Object = itemsList.GetItemObject(item2);
        item3 = playerStats.GetEquippedItem(2);
        item2Object = itemsList.GetItemObject(item3);
        item3 = playerStats.GetEquippedItem(3);
        item2Object = itemsList.GetItemObject(item4);
        //Right now we know that the second item is a melee item. However, in the future if we don't know what type an object is [melee, projectile, etc] we might need to
        //add another script to the item game object that returns the item type [GetItemTypeScript for example]

        manaRegenerationSpeed = .02f;

        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        manaBar.SetMaxMana(maxMana);
        currentMana = maxMana;
        
        for (int index = 0; index < itemCooldowns.Length; index++)
        {
            itemCooldowns[index].SetActive(false);
        }
        
    }

    //Runs every frame
    private void Update()
    {

        inventoryOrArmorEquipOpen = inventoryOpener.InventoryOpenState() || armorEquipOpener.ArmorEquipOpenState();

        if (!dashing) {
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
        }

        item1 = playerStats.GetEquippedItem(0);
        if (item1>0)
        {
            item1Object = itemsList.GetItemObject(item1);
        }
        
        item2 = playerStats.GetEquippedItem(1);
        if (item2>0)
        {
            item2Object = itemsList.GetItemObject(item2);
        }

        item3 = playerStats.GetEquippedItem(2);
        if (item3>0)
        {
            item3Object = itemsList.GetItemObject(item3);
        }

        item4 = playerStats.GetEquippedItem(3);
        if (item4>0)
        {
            item4Object = itemsList.GetItemObject(item4);
        }

        if (!inventoryOrArmorEquipOpen)
        {
            Attack();
        }

    }

    public float GetArmorMultiplier()
    {
        return mageArmorMultiplier;
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
        if (Input.GetMouseButtonDown(0) && cooldown1 == 0 && Time.timeScale == 1) 
        {
            if (playerStats.GetEquippedItem(0)>-1)
            {
                UseItemInHotbar(0, item1, item1Object);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetMouseButtonDown(1) && cooldown2 == 0 && Time.timeScale == 1)
        {
            if (playerStats.GetEquippedItem(1)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
                {
                    UseItemInHotbar(1, item2, item2Object);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q) && cooldown3 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(2)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(2)))
                {
                    UseItemInHotbar(2, item3, item3Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.E) && cooldown4 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(3)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(3)))
                {
                    UseItemInHotbar(3, item4, item4Object);
                }
            }

        }

        if (currentMana<maxMana && Time.timeScale == 1) 
        {
            currentMana += manaRegenerationSpeed;
        }

        if (cooldown1 > 0 && Time.timeScale == 1) {
            cooldown1--;
            cooldownUI = itemCooldowns[0].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown1);
        }
        if (cooldown2 > 0 && Time.timeScale == 1) {
            cooldown2--;
            cooldownUI = itemCooldowns[1].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown2);
        }
        if (cooldown3 > 0 && Time.timeScale == 1) {
            cooldown3--;
            cooldownUI = itemCooldowns[2].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown3);
        }
        if (cooldown4 > 0 && Time.timeScale == 1) {
            cooldown4--;
            cooldownUI = itemCooldowns[3].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown4);
        }

        manaBar.SetMana(currentMana);
    }

    void OnTriggerEnter2D(Collider2D other) {
        //On collision with something, it checks if its a projectile. If it is, then it gets the damage from the projectile's script
        if(other.gameObject.CompareTag("Enemy Projectile") && !dashing) {
            enemyProjectileScript = other.GetComponent<EnemyProjectileScript>();

            enemyDamage = enemyProjectileScript.GetProjectileDamage() - playerStats.GetArmor();
            Destroy(other.gameObject);
            currentHealth -= enemyDamage;

            healthbar.SetHealth(currentHealth);

            if(currentHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Wall") && dashing) {
            dashing = false;
        }
    }

    void UseItemInHotbar(int itemSlot, int item, GameObject itemObject) {

        if (currentMana > itemsList.GetManaCost(item)) {
            itemType = itemsList.GetItemType(playerStats.GetEquippedItem(itemSlot));

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;
            angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

            if (itemType == 0) {

                newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                newMelee.transform.parent = gameObject.transform;

            } else if (itemType == 1) {

                newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                newProjectile.GetComponent<ProjectileScript>().angle = angle;

            }
            
            switch(itemSlot)
            {
                case 0: cooldown1 = itemsList.GetCooldown(item); itemCooldowns[0].SetActive(true); itemCooldowns[0].GetComponent<CooldownUI>().SetMaxCooldown(cooldown1); break;
                case 1: cooldown2 = itemsList.GetCooldown(item); itemCooldowns[1].SetActive(true); itemCooldowns[1].GetComponent<CooldownUI>().SetMaxCooldown(cooldown2); break;
                case 2: cooldown3 = itemsList.GetCooldown(item); itemCooldowns[2].SetActive(true); itemCooldowns[2].GetComponent<CooldownUI>().SetMaxCooldown(cooldown3); break;
                case 3: cooldown4 = itemsList.GetCooldown(item); itemCooldowns[3].SetActive(true); itemCooldowns[3].GetComponent<CooldownUI>().SetMaxCooldown(cooldown4); break;
            }
            currentMana -= itemsList.GetManaCost(item);
        }
    }
}
