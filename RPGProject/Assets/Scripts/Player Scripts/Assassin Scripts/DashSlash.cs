using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSlash : MonoBehaviour
{
    public float damage;
    public GameObject player;
    private List<GameObject> hits = new List<GameObject> {};

    void Start()
    {
        Destroy(gameObject, 0.25f);
    }

    void Update()
    {
        transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), 1500 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool hit = true;
        if (other.CompareTag("Enemy")) {

            if (hits.Count > 0) {
                for (int i = 0; i < hits.Count; i++) {
                    if (other.gameObject == hits[i]) {
                        hit = false;
                    }
                }
                if (hit == true) {
                    other.GetComponent<EnemyScript>().TakeDamage(damage);
                    hits.Add(other.gameObject);
                }
            } else {
                other.GetComponent<EnemyScript>().TakeDamage(damage);
                hits.Add(other.gameObject);
            }

        }
    }
}
