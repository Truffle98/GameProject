using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caltrop : MonoBehaviour
{
    public float damage;

    void Start () {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyScript>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
