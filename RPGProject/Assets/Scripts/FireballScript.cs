using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed, lifespan;
    private Vector3 shootDirection;
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
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shootDirection / 500 * speed);
    }

    void OnTriggerEnter2D(Collider2D other) {

        //Checks for collisions. If fireball projectile collides with game objects walls or enemies it destorys itself
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }

}
