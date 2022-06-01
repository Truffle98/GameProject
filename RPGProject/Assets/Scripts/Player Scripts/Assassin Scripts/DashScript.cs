using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private GameObject player, newObj;
    public GameObject slash;
    private Assassin assassinScript;
    private PlayerStats playerStats;
    private int dashCooldown = 0, dashCount = 0;
    public int variation;
    public float angle, damage;
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

        for (int i = 0; i < 8; i++)
        {
            assassinScript.cooldowns[i] = assassinScript.GetCooldown(0); if (playerStats.isItemInSlot(i)) assassinScript.itemCooldowns[i].SetActive(true); assassinScript.itemCooldowns[i].GetComponent<CooldownUI>().SetMaxCooldown(assassinScript.GetCooldown(0));
        }
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
            if (variation == 1) {
                newObj = Instantiate(slash, player.transform.position + new Vector3 (0, 2, 0), Quaternion.Euler(0,0,0));
                newObj.GetComponent<DashSlash>().damage = damage;
                newObj.GetComponent<DashSlash>().player = player;
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
