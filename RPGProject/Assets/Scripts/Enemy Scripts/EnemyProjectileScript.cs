using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public float speed, lifespan, damage;
    private Vector3 shootDirection;
    private GameObject player;
    
    public float GetProjectileDamage() {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Character");
        //Determines direction of fireball and sets lifespan

        shootDirection = player.transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection-transform.position;
        shootDirection.z = 0.0f;
        shootDirection = shootDirection.normalized;
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
