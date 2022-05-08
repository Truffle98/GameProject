using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript1 : MonoBehaviour
{
    public float speed, lifespan;
    public int itemID;
    private float damage;
    private Vector3 shootDirection;
    private PlayerStats playerStats;

    public float GetDamage()
    {
        return damage;
    }

    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        damage = playerStats.GetItemDamage(itemID);

        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection.normalized;
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime);
    }
    
}
