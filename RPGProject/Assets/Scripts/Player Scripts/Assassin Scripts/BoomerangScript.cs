using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    private int timePassed = 0;
    private float rotationSpeed = 1.5f;
    public Vector3 point;
    public float damage;

    void Start()
    {
        Destroy(gameObject, 2.25f);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,rotationSpeed * timePassed);
        transform.RotateAround(point, new Vector3 (0,0,1), Time.deltaTime * 150);
        timePassed++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyScript>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
