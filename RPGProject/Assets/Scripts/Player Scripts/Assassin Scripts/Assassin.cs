using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : BaseClass
{
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana, angle, manaRegenerationSpeed,
    healAmount = 0, speed, damage, caltropSlow = 0.3f;
    private Vector3 shootDirection;
    private int empowerWeaponTimer = 0, healCount = 0, bloodRushTimer = 0;
    public int[] cooldowns = new int[8], itemTypes = new int[8], items = new int[8];
    public GameObject[] itemObjects = new GameObject[8];
    public Rigidbody2D body;
    private Animator anim;
    private bool movingUp, movingDown, inventoryOrArmorEquipOpen, empowerWeapon = false, healing = false;
    private PlayerStats playerStats;
    private InventoryOpener inventoryOpener;
    private ArmorEquipOpener armorEquipOpener;
    private Items itemsList;
    public GameObject[] itemCooldowns;
    private GameObject newMelee, newProjectile, newObject;
    private CooldownUI cooldownUI;
    public ManaBar manaBar;
    public HealthBar1 healthbar;
    private EnemyProjectileScript enemyProjectileScript;
    private SoundEffects soundEffects;
    public bool dashing = false;
    private float assassinArmorMultiplier = baseArmorMultiplier + 0.25f;

    //Nested array: 0: damage, 1: mana cost, 2: cooldown, 3: ability variation, 4: item type
    protected string[] assassinAbilityNames = {"Dash", "Thorn", "Empower Weapon", "Caltrops", "Thousand Cuts", "Blood Rush", "Boomerang" };
    protected int[,] assassinAbilityStats = new int[7, 5] { {0, 10, 100, 0, 2}, {15, 25, 2500, 0, 2}, {0, 15, 1000, 1, 2}, {5, 30, 500, 1, 2}, {5, 20, 750, 0, 2}, {0, 15, 1000, 0, 2}, {20, 5, 1000, 1, 2} };
    //array for empower weapon type 0 = {5, 15, 1000, 0, 2}
    public GameObject[] assassinAbilityObjects;

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
        soundEffects = gameObject.GetComponent<SoundEffects>();
       
        manaRegenerationSpeed = .02f;

        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        manaBar.SetMaxMana(maxMana);
        currentMana = maxMana;
        speed = baseSpeed;
        damage = baseDamage;
        
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
            body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
            //Flips sprites and helps facilitate sprite transitions
            changeSprite(horizontalInput, verticalInput);

            //Set animator parameters
            anim.SetBool("Running", horizontalInput != 0);
            anim.SetBool("UpRunning", movingUp);
            anim.SetBool("DownRunning", movingDown);
        }

        if (healing)
        {
            if (healCount < 500)
            {
                if (currentHealth+healAmount<=maxHealth)
                {
                    currentHealth += healAmount;
                }
                else
                {
                    currentHealth = maxHealth;
                }
                healthbar.SetHealth(currentHealth);
                healCount++;
            }
            else
            {
                healing = false;
                healCount = 0;
            }
        }

        for (int i = 0; i<8; i++)
        {
            itemTypes[i] = playerStats.GetEquippedItemClass(i);
            items[i] = playerStats.GetEquippedItem(i);
            if (itemTypes[i] >= 0)
            {
                itemObjects[i] = assassinAbilityObjects[items[i]];
            }
            else
            {
                itemObjects[i] = itemsList.GetItemObject(items[i]);
            }
        }

        if (!inventoryOrArmorEquipOpen)
        {
            Attack();
        }

        if (empowerWeapon && empowerWeaponTimer > 0) {
            empowerWeaponTimer--;
        } else if (empowerWeapon && empowerWeaponTimer <= 0) {
            empowerWeapon = false;
        } else if (!empowerWeapon && empowerWeaponTimer > 0) {
            empowerWeaponTimer = 0;
        }

        if (bloodRushTimer > 1) {
            bloodRushTimer--;
        } else if (bloodRushTimer == 1) {
            speed = baseSpeed;
            damage = baseDamage;
            bloodRushTimer--;
        }

    }

    public float GetArmorMultiplier()
    {
        return assassinArmorMultiplier;
    }

    //Handles animation changes
    private void changeSprite(float horizontalInput, float verticalInput)
    {
        if (horizontalInput > .01f)
        {
            //transform.localScale = new Vector3(-6, 6, 6);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput < -.01f)
        {
            //transform.localScale = Vector3.one * 6;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
        if (Input.GetMouseButtonDown(0) && cooldowns[0] == 0 && Time.timeScale == 1) 
        {
            if (items[0]>-1)
            {
                UseItemInHotbar(0, itemTypes[0], items[0], itemObjects[0]);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetMouseButtonDown(1) && cooldowns[1] == 0 && Time.timeScale == 1)
        {
            if (items[1]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
                {
                    UseItemInHotbar(1, itemTypes[1], items[1], itemObjects[1]);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q) && cooldowns[2] == 0 && Time.timeScale == 1) {

            if (items[2]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(2)))
                {
                    UseItemInHotbar(2, itemTypes[2], items[2], itemObjects[2]);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.E) && cooldowns[3] == 0 && Time.timeScale == 1) {

            if (items[3]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(3)))
                {
                    UseItemInHotbar(3, itemTypes[3], items[3], itemObjects[3]);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && cooldowns[4] == 0 && Time.timeScale == 1) 
        {
            if (items[4]>-1)
            {
                UseItemInHotbar(4, itemTypes[4], items[4], itemObjects[4]);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cooldowns[5] == 0 && Time.timeScale == 1)
        {
            if (items[5]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(5)))
                {
                    UseItemInHotbar(5, itemTypes[5], items[5], itemObjects[5]);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && cooldowns[6] == 0 && Time.timeScale == 1) {

            if (items[6]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(6)))
                {
                    UseItemInHotbar(6, itemTypes[6], items[6], itemObjects[6]);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && cooldowns[7] == 0 && Time.timeScale == 1) {

            if (items[7]>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(7)))
                {
                    UseItemInHotbar(7, itemTypes[7], items[7], itemObjects[7]);
                }
            }

        }

        if (currentMana<maxMana && Time.timeScale == 1) 
        {
            currentMana += manaRegenerationSpeed;
        }

        for (int cooldown = 0; cooldown<8; cooldown++)
        {
            if (cooldowns[cooldown] > 0 && Time.timeScale == 1)
            {
                cooldowns[cooldown]--;
                cooldownUI = itemCooldowns[cooldown].GetComponent<CooldownUI>();
                cooldownUI.SetCooldown(cooldowns[cooldown]);
            }
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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall") && dashing) {
            dashing = false;
        }
    }

    //item class = item ability class type (0 = mage, 1 = assassin, -1 = classless item)
    void UseItemInHotbar(int itemSlot, int itemClass, int item, GameObject itemObject) {
        float manaCost = 0;
        if (itemClass == 1)
        {
            manaCost = assassinAbilityStats[item, 1];
        }
        else
        {
            manaCost = itemsList.GetManaCost(item);
        }

        if (currentMana > manaCost) {

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;
            angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

            if (itemClass==1)
            {
                if (assassinAbilityStats[item, 4] == 0) 
                {
                    newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                    newMelee.transform.parent = gameObject.transform;
                }
                else if (assassinAbilityStats[item, 4] == 1) 
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                    newProjectile.GetComponent<ProjectileScript>().angle = angle;
                } 
                else if (item == 0) 
                {
                    Instantiate(itemObject, transform.position, Quaternion.Euler(0,0,0));
                } 
                else if (item == 1) 
                {
                    newMelee = Instantiate(itemObject, transform.position, Quaternion.Euler(0,0,0));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<AOEScript>().damage = GetAbilityDamage(1);
                    if (empowerWeapon) {
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(0, 1, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(0, -1, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newObject.transform.Rotate(new Vector3(0,0,180));
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(1, 0, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newObject.transform.Rotate(new Vector3(0,0,-90));
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(-1, 0, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newObject.transform.Rotate(new Vector3(0,0,90));
                        newMelee.AddComponent<BuffWeapon>();
                        newMelee.GetComponent<BuffWeapon>().damage = GetAbilityDamage(2);
                        newMelee.GetComponent<BuffWeapon>().effect = assassinAbilityObjects[2];
                        newMelee.GetComponent<BuffWeapon>().type = assassinAbilityStats[2, 3];
                        empowerWeaponTimer -= 2500;
                    }
                }
                else if (item == 2)
                {
                    empowerWeapon = true;
                    empowerWeaponTimer = 2500;
                }
                else if (item == 3)
                {
                    newMelee = Instantiate(itemObject, transform.position, Quaternion.Euler(0,0,0));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<CaltropParent>().damage = GetAbilityDamage(3);
                    if (empowerWeapon)
                    {
                        newMelee.GetComponent<CaltropParent>().effect = assassinAbilityObjects[2];
                        newMelee.GetComponent<CaltropParent>().effectDamage = GetAbilityDamage(2);
                        newMelee.GetComponent<CaltropParent>().effectType = assassinAbilityStats[2, 3];
                        empowerWeaponTimer -= 2500;
                    }
                    if (assassinAbilityStats[3, 3] == 1) {
                        newMelee.GetComponent<CaltropParent>().slow += caltropSlow;
                    }
                    
                }
                else if (item == 4) 
                {
                    newMelee = Instantiate(itemObject, transform.position, Quaternion.Euler(0, 0, 0));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<ThousandCutsParent>().angle = angle;
                    newMelee.GetComponent<ThousandCutsParent>().damage = assassinAbilityStats[4, 0];
                    if (empowerWeapon)
                    {
                        newMelee.GetComponent<ThousandCutsParent>().effect = assassinAbilityObjects[2];
                        newMelee.GetComponent<ThousandCutsParent>().effectDamage = GetAbilityDamage(2);
                        newMelee.GetComponent<ThousandCutsParent>().effectType = assassinAbilityStats[2, 3];
                        empowerWeaponTimer -= 2500;
                    }
                }
                else if (item == 5)
                {
                    if (currentHealth > 20) 
                    {
                        bloodRushTimer = 750;
                        if (assassinAbilityStats[5, 3] == 0) {
                            speed = baseSpeed * 1.5f;
                        } else {
                            speed = baseSpeed * 1.25f;
                            damage = baseDamage * 1.25f;
                        }
                        currentHealth -= (20);
                        healthbar.SetHealth(currentHealth);
                    }
                }
                else if (item == 6)
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.8f), Mathf.Sin(angle - 0.8f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 180));
                    newProjectile.GetComponent<BoomerangScript>().point = transform.position + new Vector3 (Mathf.Cos(angle) * 4, Mathf.Sin(angle) * 4, 0);
                    newProjectile.GetComponent<BoomerangScript>().damage = GetAbilityDamage(6);
                    newProjectile.GetComponent<BoomerangScript>().itemSlot = itemSlot;
                    newProjectile.GetComponent<BoomerangScript>().variation = assassinAbilityStats[6, 3];
                }
            }
            else 
            {
                if (itemsList.GetItemType(item) == 0)
                {
                    newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<MeleeScript>().damage = playerStats.GetItemDamage(item) * damage;
                    if (empowerWeapon) {
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(Mathf.Cos(angle - 0.6f) * 1.5f, Mathf.Sin(angle - 0.6f) * 1.5f, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newMelee.AddComponent<BuffWeapon>();
                        newMelee.GetComponent<BuffWeapon>().damage = GetAbilityDamage(2);
                        newMelee.GetComponent<BuffWeapon>().effect = assassinAbilityObjects[2];
                        newMelee.GetComponent<BuffWeapon>().type = assassinAbilityStats[2, 3];
                        empowerWeaponTimer -= 500;
                    }
                    soundEffects.Swing();
                }
                else 
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                    newProjectile.GetComponent<ProjectileScript>().angle = angle;
                    newProjectile.GetComponent<ProjectileScript>().damage = playerStats.GetItemDamage(item) * damage;
                }
            }

            for (int index = 0; index<8; index++)
            {
                if (itemSlot == index)
                {
                    if (itemTypes[index] == 1)
                    {
                        cooldowns[index] = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldowns[index] = itemsList.GetCooldown(item);
                    }
                    itemCooldowns[index].SetActive(true); 
                    itemCooldowns[index].GetComponent<CooldownUI>().
                    SetMaxCooldown(cooldowns[index]);
                }
            }
            
            if (itemClass==1)
            {
                currentMana -= assassinAbilityStats[item, 1];
            }
            else
            {
                currentMana -= itemsList.GetManaCost(item);
            }
        }
    }

    public float GetAbilityDamage(int index)
    {
        return assassinAbilityStats[index, 0] * damage;
    }

    public GameObject GetAbilityObject(int index)
    {
        return assassinAbilityObjects[index];
    }

    public string GetAbilityName(int index)
    {
        return assassinAbilityNames[index];
    }

    public int GetCooldown(int index)
    {
        return assassinAbilityStats[index, 2];
    }

    public void LevelUp(int level)
    {

    }

    public void Heal(int itemID)
    {
        healAmount = itemsList.GetHealAmount(itemID)/500f;
        healing = true;
    }

    public bool IsHealing()
    {
        return healing;
    }
}
