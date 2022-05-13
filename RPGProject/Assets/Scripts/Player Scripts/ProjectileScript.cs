using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed, lifespan, angle, damage, multiplier = 1;
    public int itemID;
    private Vector3 shootDirection;
    private PlayerStats playerStats;
    
    public float GetProjectileDamage() {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Determines direction of fireball and sets lifespan

        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection.normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

        //transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
        //GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        damage = playerStats.GetItemDamage(itemID) * multiplier;
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //Checks for collisions. If fireball projectile collides with walls it destroys itself
        if(other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
