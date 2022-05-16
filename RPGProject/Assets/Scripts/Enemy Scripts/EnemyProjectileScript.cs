using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public float speed, lifespan, damage, angle;
    private Vector3 shootDirection;
    private GameObject player;
    
    public float GetProjectileDamage() {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Determines direction of fireball and sets lifespan

        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
        GetComponent<Rigidbody2D>().velocity = shootDirection * speed;

        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(shootDirection * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //Checks for collisions. If fireball projectile collides with walls it destroys itself
        if(other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
