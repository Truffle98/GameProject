using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : BaseClass
{
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana, angle, manaRegenerationSpeed;
    private Vector3 shootDirection;
    private int item1, item2, item3, item4, item5, item6, item7, item8, item1Type, item2Type, item3Type, item4Type, item5Type, item6Type, item7Type, item8Type, empowerWeaponTimer = 0;
    public int cooldown1 = 0, cooldown2 = 0, cooldown3 = 0, cooldown4 = 0, cooldown5 = 0, cooldown6 = 0, cooldown7 = 0, cooldown8 = 0;
    public Rigidbody2D body;
    private Animator anim;
    private bool movingUp, movingDown, inventoryOrArmorEquipOpen, empowerWeapon = false;
    private PlayerStats playerStats;
    private InventoryOpener inventoryOpener;
    private ArmorEquipOpener armorEquipOpener;
    private Items itemsList;
    public GameObject[] itemCooldowns;
    private CooldownUI cooldownUI;
    private GameObject item1Object, item2Object, item3Object, item4Object, item5Object, item6Object, item7Object, item8Object, newMelee, newProjectile, newObject;
    public ManaBar manaBar;
    public HealthBar1 healthbar;
    private EnemyProjectileScript enemyProjectileScript;
    private SoundEffects soundEffects;
    public bool dashing = false;
    private float assassinArmorMultiplier = baseArmorMultiplier + 0.25f;

    //Nested array: 0: damage, 1: mana cost, 2: cooldown, 3: ability variation, 4: item type
    protected string[] assassinAbilityNames = {"Dash", "Thorn", "Empower Weapon", "Caltrops", "Thousand Cuts" };
    protected int[,] assassinAbilityStats = new int[5, 5] { {0, 10, 100, 0, 2}, {15, 25, 2500, 0, 2}, {5, 15, 1000, 0, 2}, {5, 30, 500, 0, 2}, {5, 20, 750, 0, 2} };
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

        item1Type = playerStats.GetEquippedItemClass(0);
        item1 = playerStats.GetEquippedItem(0);
        if (item1>=0)
        {
            if (item1Type == 1)
            {
                item1Object = assassinAbilityObjects[item1];
            }
            else 
            {
                item1Object = itemsList.GetItemObject(item1);
            }
        }
        
        item2Type = playerStats.GetEquippedItemClass(1);
        item2 = playerStats.GetEquippedItem(1);
        if (item2>=0)
        {
            if (item2Type == 1)
            {
                item2Object = assassinAbilityObjects[item2];
            }
            else 
            {
                item2Object = itemsList.GetItemObject(item2);
            }
        }

        item3Type = playerStats.GetEquippedItemClass(2);
        item3 = playerStats.GetEquippedItem(2);
        if (item3>=0)
        {
            if (item3Type == 1)
            {
                item3Object = assassinAbilityObjects[item3];
            }
            else 
            {
                item3Object = itemsList.GetItemObject(item3);
            }
        }

        item4Type = playerStats.GetEquippedItemClass(3);
        item4 = playerStats.GetEquippedItem(3);
        if (item4>=0)
        {
            if (item4Type == 1)
            {
                item4Object = assassinAbilityObjects[item4];
            }
            else 
            {
                item4Object = itemsList.GetItemObject(item4);
            }
        }

        item5Type = playerStats.GetEquippedItemClass(4);
        item5 = playerStats.GetEquippedItem(4);
        if (item5>=0)
        {
            if (item5Type == 1)
            {
                item5Object = assassinAbilityObjects[item5];
            }
            else 
            {
                item5Object = itemsList.GetItemObject(item5);
            }
        }
        
        item6Type = playerStats.GetEquippedItemClass(5);
        item6 = playerStats.GetEquippedItem(5);
        if (item6>=0)
        {
            if (item6Type == 1)
            {
                item6Object = assassinAbilityObjects[item6];
            }
            else 
            {
                item6Object = itemsList.GetItemObject(item6);
            }
        }

        item7Type = playerStats.GetEquippedItemClass(6);
        item7 = playerStats.GetEquippedItem(6);
        if (item7>=0)
        {
            if (item7Type == 1)
            {
                item7Object = assassinAbilityObjects[item7];
            }
            else 
            {
                item7Object = itemsList.GetItemObject(item7);
            }
        }

        item8Type = playerStats.GetEquippedItemClass(7);
        item8 = playerStats.GetEquippedItem(7);
        if (item8>=0)
        {
            if (item8Type == 1)
            {
                item8Object = assassinAbilityObjects[item8];
            }
            else 
            {
                item8Object = itemsList.GetItemObject(item8);
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
        if (Input.GetMouseButtonDown(0) && cooldown1 == 0 && Time.timeScale == 1) 
        {
            if (item1>-1)
            {
                UseItemInHotbar(0, item1Type, item1, item1Object);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetMouseButtonDown(1) && cooldown2 == 0 && Time.timeScale == 1)
        {
            if (item2>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
                {
                    UseItemInHotbar(1, item2Type, item2, item2Object);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q) && cooldown3 == 0 && Time.timeScale == 1) {

            if (item3>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(2)))
                {
                    UseItemInHotbar(2, item3Type, item3, item3Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.E) && cooldown4 == 0 && Time.timeScale == 1) {

            if (item4>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(3)))
                {
                    UseItemInHotbar(3, item4Type, item4, item4Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && cooldown5 == 0 && Time.timeScale == 1) 
        {
            if (item5>-1)
            {
                UseItemInHotbar(4, item5Type, item5, item5Object);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cooldown6 == 0 && Time.timeScale == 1)
        {
            if (item6>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(5)))
                {
                    UseItemInHotbar(5, item6Type, item6, item6Object);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && cooldown7 == 0 && Time.timeScale == 1) {

            if (item7>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(6)))
                {
                    UseItemInHotbar(6, item7Type, item7, item7Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && cooldown8 == 0 && Time.timeScale == 1) {

            if (item8>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(7)))
                {
                    UseItemInHotbar(7, item8Type, item8, item8Object);
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
        if (cooldown5 > 0 && Time.timeScale == 1) {
            cooldown5--;
            cooldownUI = itemCooldowns[4].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown5);
        }
        if (cooldown6 > 0 && Time.timeScale == 1) {
            cooldown6--;
            cooldownUI = itemCooldowns[5].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown6);
        }
        if (cooldown7 > 0 && Time.timeScale == 1) {
            cooldown7--;
            cooldownUI = itemCooldowns[6].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown7);
        }
        if (cooldown8 > 0 && Time.timeScale == 1) {
            cooldown8--;
            cooldownUI = itemCooldowns[7].GetComponent<CooldownUI>();
            cooldownUI.SetCooldown(cooldown8);
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
                        newMelee.GetComponent<BuffWeapon>().damage = assassinAbilityStats[2, 0];
                        newMelee.GetComponent<BuffWeapon>().effect = assassinAbilityObjects[2];
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
                    if (empowerWeapon)
                    {
                        newMelee.GetComponent<CaltropParent>().damage = assassinAbilityStats[3, 0];
                        newMelee.GetComponent<CaltropParent>().effect = assassinAbilityObjects[2];
                        newMelee.GetComponent<CaltropParent>().effectDamage = assassinAbilityStats[2, 0];
                        empowerWeaponTimer -= 2500;
                    }
                    
                }
                else if (item == 4) 
                {
                    newMelee = Instantiate(itemObject, transform.position, Quaternion.Euler(0, 0, 0));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<ThousandCutsParent>().angle = angle;
                    newMelee.GetComponent<ThousandCutsParent>().damage = assassinAbilityStats[4, 0];
                }
            }
            else 
            {
                if (itemsList.GetItemType(item) == 0)
                {
                    newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                    newMelee.transform.parent = gameObject.transform;
                    newMelee.GetComponent<MeleeScript>().damage = playerStats.GetItemDamage(item);
                    if (empowerWeapon) {
                        newObject = Instantiate(assassinAbilityObjects[2], (new Vector3(Mathf.Cos(angle - 0.6f) * 1.5f, Mathf.Sin(angle - 0.6f) * 1.5f, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 135));
                        newObject.transform.parent = newMelee.transform;
                        newMelee.AddComponent<BuffWeapon>();
                        newMelee.GetComponent<BuffWeapon>().damage = assassinAbilityStats[2, 0];
                        newMelee.GetComponent<BuffWeapon>().effect = assassinAbilityObjects[2];
                        empowerWeaponTimer -= 500;
                    }
                    soundEffects.Swing();
                }
                else 
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                    newProjectile.GetComponent<ProjectileScript>().angle = angle;
                    newProjectile.GetComponent<ProjectileScript>().damage = playerStats.GetItemDamage(item);
                }
            }
            
            switch(itemSlot)
            {
                case 0:
                    if (item1Type == 1)
                    {
                        cooldown1 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown1 = itemsList.GetCooldown(item);
                    }; itemCooldowns[0].SetActive(true); itemCooldowns[0].GetComponent<CooldownUI>().SetMaxCooldown(cooldown1); break;
                case 1: 
                    if (item2Type == 1)
                    {
                        cooldown2 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown2 = itemsList.GetCooldown(item);
                    }; itemCooldowns[1].SetActive(true); itemCooldowns[1].GetComponent<CooldownUI>().SetMaxCooldown(cooldown2); break;
                case 2: 
                    if (item3Type == 1)
                    {
                        cooldown3 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown3 = itemsList.GetCooldown(item);
                    }; itemCooldowns[2].SetActive(true); itemCooldowns[2].GetComponent<CooldownUI>().SetMaxCooldown(cooldown3); break;
                case 3: 
                    if (item4Type == 1)
                    {
                        cooldown4 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown4 = itemsList.GetCooldown(item);
                    }; itemCooldowns[3].SetActive(true); itemCooldowns[3].GetComponent<CooldownUI>().SetMaxCooldown(cooldown4); break;
                case 4: 
                    if (item5Type == 1)
                    {
                        cooldown5 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown5 = itemsList.GetCooldown(item);
                    }; itemCooldowns[4].SetActive(true); itemCooldowns[4].GetComponent<CooldownUI>().SetMaxCooldown(cooldown5); break;
                case 5: 
                    if (item6Type == 1)
                    {
                        cooldown6 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown6 = itemsList.GetCooldown(item);
                    }; itemCooldowns[5].SetActive(true); itemCooldowns[5].GetComponent<CooldownUI>().SetMaxCooldown(cooldown6); break;
                case 6: 
                    if (item7Type == 1)
                    {
                        cooldown7 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown7 = itemsList.GetCooldown(item);
                    }; itemCooldowns[6].SetActive(true); itemCooldowns[6].GetComponent<CooldownUI>().SetMaxCooldown(cooldown7); break;
                case 7: 
                    if (item8Type == 1)
                    {
                        cooldown8 = assassinAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown8 = itemsList.GetCooldown(item);
                    }; itemCooldowns[7].SetActive(true); itemCooldowns[7].GetComponent<CooldownUI>().SetMaxCooldown(cooldown8); break;
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

    public int GetAbilityDamage(int index)
    {
        return assassinAbilityStats[index, 0] * baseDamage;
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
}
