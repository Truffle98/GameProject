using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    private int timePassed = 0;
    private float rotationSpeed = 1.5f;
    public Vector3 point;
    public float damage;
    public int itemSlot, variation;

    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,rotationSpeed * timePassed);
        transform.RotateAround(point, new Vector3 (0,0,1), Time.deltaTime * 150);
        timePassed++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Character"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyScript>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Character"))
            {
                if (timePassed > 250 && variation == 1) 
                {
                    other.GetComponent<Assassin>().cooldowns[itemSlot] -= 400;
                    if (other.GetComponent<Assassin>().cooldowns[itemSlot] < 0)
                    {
                        other.GetComponent<Assassin>().cooldowns[itemSlot] = 0;
                    }
                    Destroy(gameObject);
                }
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
