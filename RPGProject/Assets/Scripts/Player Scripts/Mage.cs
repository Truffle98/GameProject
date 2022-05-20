using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Mage : BaseClass
{
    private float horizontalInput, verticalInput, maxHealth = baseHealth * 2, currentHealth, enemyDamage, maxMana = baseMana * 2, currentMana, angle, manaRegenerationSpeed;
    private Vector3 shootDirection;
    private int item1, item2, item3, item4, item5, item6, item7, item8, item1Type, item2Type, item3Type, item4Type, item5Type, item6Type, item7Type, item8Type;
    public int cooldown1 = 0, cooldown2 = 0, cooldown3 = 0, cooldown4 = 0, cooldown5 = 0, cooldown6 = 0, cooldown7 = 0, cooldown8 = 0;
    public Rigidbody2D body;
    private Animator anim;
    private bool movingUp, movingDown, inventoryOrArmorEquipOpen;
    private PlayerStats playerStats;
    private InventoryOpener inventoryOpener;
    private ArmorEquipOpener armorEquipOpener;
    private Items itemsList;
    public GameObject[] itemCooldowns;
    private CooldownUI cooldownUI;
    private GameObject item1Object, item2Object, item3Object, item4Object, item5Object, item6Object, item7Object, item8Object, newMelee, newProjectile;
    public ManaBar manaBar;
    public bool dashing = false;
    private EnemyProjectileScript enemyProjectileScript;
    private float mageArmorMultiplier = baseArmorMultiplier + 0.5f;

    //Nested array: 0: damage, 1: mana cost, 2: cooldown, 3: ability variation, 4: ability type (melee or ranged)
    public GameObject[] mageAbilityObjects;
    protected string[] mageAbilityNames = {"Fireball", "Vampiric Bolt", "Wave", "Mind Control"};
    protected int[,] mageAbilityStats = new int[4, 5] { {10, 20, 100, 0, 1}, {10, 25, 125, 0, 1}, {8, 15, 120, 0, 1}, {0, 50, 2000, 0, 1} };

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
            if (item1Type == 0)
            {
                item1Object = mageAbilityObjects[item1];
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
            if (item2Type == 0)
            {
                item2Object = mageAbilityObjects[item2];
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
            if (item3Type == 0)
            {
                item3Object = mageAbilityObjects[item3];
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
            if (item4Type == 0)
            {
                item4Object = mageAbilityObjects[item4];
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
            if (item5Type == 0)
            {
                item5Object = mageAbilityObjects[item5];
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
            if (item6Type == 0)
            {
                item6Object = mageAbilityObjects[item6];
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
            if (item7Type == 0)
            {
                item7Object = mageAbilityObjects[item7];
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
            if (item8Type == 0)
            {
                item8Object = mageAbilityObjects[item8];
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
                UseItemInHotbar(0, item1Type, item1, item1Object);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetMouseButtonDown(1) && cooldown2 == 0 && Time.timeScale == 1)
        {
            if (playerStats.GetEquippedItem(1)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(1)))
                {
                    UseItemInHotbar(1, item2Type, item2, item2Object);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q) && cooldown3 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(2)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(2)))
                {
                    UseItemInHotbar(2, item3Type, item3, item3Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.E) && cooldown4 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(3)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(3)))
                {
                    UseItemInHotbar(3, item4Type, item4, item4Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && cooldown5 == 0 && Time.timeScale == 1) 
        {
            if (playerStats.GetEquippedItem(4)>-1)
            {
                UseItemInHotbar(4, item5Type, item5, item5Object);
            }
        }

        //Accesses second item in hotbar
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cooldown6 == 0 && Time.timeScale == 1)
        {
            if (playerStats.GetEquippedItem(5)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(5)))
                {
                    UseItemInHotbar(5, item6Type, item6, item6Object);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && cooldown7 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(6)>-1)
            {
                if(currentMana>itemsList.GetManaCost(playerStats.GetEquippedItem(6)))
                {
                    UseItemInHotbar(6, item7Type, item7, item7Object);
                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && cooldown8 == 0 && Time.timeScale == 1) {

            if (playerStats.GetEquippedItem(7)>-1)
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

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Wall") && dashing) {
            dashing = false;
        }
    }

    void UseItemInHotbar(int itemSlot, int itemClass, int item, GameObject itemObject) {

        if (currentMana > itemsList.GetManaCost(item)) {

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;
            angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

            if (itemClass==0)
            {
                if (mageAbilityStats[item, 4] == 0) 
                {

                    newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                    newMelee.transform.parent = gameObject.transform;

                }
                else
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                    newProjectile.GetComponent<ProjectileScript>().angle = angle;

                }
            }
            else
            {
                if (itemsList.GetItemType(item) == 0)
                {
                    newMelee = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle - 0.5f), Mathf.Sin(angle - 0.5f), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                    newMelee.transform.parent = gameObject.transform;
                }
                else 
                {
                    newProjectile = Instantiate(itemObject, (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + transform.position), Quaternion.Euler(0, 0, 0));
                    newProjectile.GetComponent<ProjectileScript>().angle = angle;
                }
            }
            /*
            else 
            {
                
            }
            */
            switch(itemSlot)
            {
                case 0:
                    if (item1Type == 0)
                    {
                        cooldown1 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown1 = itemsList.GetCooldown(item);
                    }; itemCooldowns[0].SetActive(true); itemCooldowns[0].GetComponent<CooldownUI>().SetMaxCooldown(cooldown1); break;
                case 1: 
                    if (item2Type == 0)
                    {
                        cooldown2 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown2 = itemsList.GetCooldown(item);
                    }; itemCooldowns[1].SetActive(true); itemCooldowns[1].GetComponent<CooldownUI>().SetMaxCooldown(cooldown2); break;
                case 2: 
                    if (item3Type == 0)
                    {
                        cooldown3 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown3 = itemsList.GetCooldown(item);
                    }; itemCooldowns[2].SetActive(true); itemCooldowns[2].GetComponent<CooldownUI>().SetMaxCooldown(cooldown3); break;
                case 3: 
                    if (item4Type == 0)
                    {
                        cooldown4 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown4 = itemsList.GetCooldown(item);
                    }; itemCooldowns[3].SetActive(true); itemCooldowns[3].GetComponent<CooldownUI>().SetMaxCooldown(cooldown4); break;
                case 5: 
                    if (item5Type == 0)
                    {
                        cooldown5 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown5 = itemsList.GetCooldown(item);
                    }; itemCooldowns[4].SetActive(true); itemCooldowns[4].GetComponent<CooldownUI>().SetMaxCooldown(cooldown5); break;
                case 6: 
                    if (item6Type == 0)
                    {
                        cooldown6 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown6 = itemsList.GetCooldown(item);
                    }; itemCooldowns[5].SetActive(true); itemCooldowns[5].GetComponent<CooldownUI>().SetMaxCooldown(cooldown6); break;
                case 7: 
                    if (item7Type == 0)
                    {
                        cooldown7 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown7 = itemsList.GetCooldown(item);
                    }; itemCooldowns[6].SetActive(true); itemCooldowns[6].GetComponent<CooldownUI>().SetMaxCooldown(cooldown7); break;
                case 8: 
                    if (item8Type == 0)
                    {
                        cooldown8 = mageAbilityStats[item, 2];
                    }
                    else
                    {
                        cooldown8 = itemsList.GetCooldown(item);
                    }; itemCooldowns[7].SetActive(true); itemCooldowns[7].GetComponent<CooldownUI>().SetMaxCooldown(cooldown8); break;
            }
            if (itemClass==0)
            {
                currentMana -= mageAbilityStats[item, 1];
            }
            else
            {
                currentMana -= itemsList.GetManaCost(item);
            }
        }
    }

    public int GetAbilityDamage(int index)
    {
        return mageAbilityStats[index, 0] * baseDamage;
    }

    public GameObject GetAbilityObject(int index)
    {
        return mageAbilityObjects[index];
    }

    public string GetAbilityName(int index)
    {
        return mageAbilityNames[index];
    }
}
