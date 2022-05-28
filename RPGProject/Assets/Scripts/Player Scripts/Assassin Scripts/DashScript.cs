using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private GameObject player;
    private Assassin assassinScript;
    private PlayerStats playerStats;
    private int dashCooldown = 0, dashCount = 0;
    private float angle;
    private Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection = shootDirection.normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

        player = GameObject.Find("Assassin(Clone)");
        player.GetComponent<SpriteRenderer>().enabled = false;
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        assassinScript = player.GetComponent<Assassin>();
        assassinScript.dashing = true;
        assassinScript.body.velocity = new Vector2(0, 0);

        assassinScript.cooldowns[0] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(0)) assassinScript.itemCooldowns[0].SetActive(true); assassinScript.itemCooldowns[0].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[1] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(1)) assassinScript.itemCooldowns[1].SetActive(true); assassinScript.itemCooldowns[1].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[2] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(2)) assassinScript.itemCooldowns[2].SetActive(true); assassinScript.itemCooldowns[2].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[3] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(3)) assassinScript.itemCooldowns[3].SetActive(true); assassinScript.itemCooldowns[3].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[4] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(4)) assassinScript.itemCooldowns[4].SetActive(true); assassinScript.itemCooldowns[4].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[5] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(5)) assassinScript.itemCooldowns[5].SetActive(true); assassinScript.itemCooldowns[5].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[6] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(6)) assassinScript.itemCooldowns[6].SetActive(true); assassinScript.itemCooldowns[6].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        assassinScript.cooldowns[7] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(7)) assassinScript.itemCooldowns[7].SetActive(true); assassinScript.itemCooldowns[7].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (assassinScript.dashing == false || dashCount == 24) {
            assassinScript.dashing = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyScript>().lastSeenTarget = 0;
            }
            Destroy(gameObject);
        }
        if (dashCooldown <= 0) {
            player.transform.position = new Vector3 (Mathf.Cos(angle) * 0.2f, Mathf.Sin(angle) * 0.2f, 0) + player.transform.position;
            dashCount++;
            dashCooldown = 3;
        }

        dashCooldown--;
    }
}
