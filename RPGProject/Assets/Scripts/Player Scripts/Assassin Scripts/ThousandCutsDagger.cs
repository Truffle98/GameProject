using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThousandCutsDagger : MonoBehaviour
{
    public float damage;

    void Start() {
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }

}
