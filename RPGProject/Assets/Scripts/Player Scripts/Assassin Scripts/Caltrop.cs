using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caltrop : MonoBehaviour
{
    public float damage, slow = 0;

    void Start () {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyScript>().TakeDamage(damage);
            if (slow > 0) {
                other.GetComponent<EnemyScript>().Slow(slow, 500);
            }
            Destroy(gameObject);
        }

    }
}
