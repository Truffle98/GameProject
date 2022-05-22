using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeapon : MonoBehaviour
{
    private GameObject newObject;
    public GameObject effect;
    public float damage;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            newObject = Instantiate(effect, other.transform.position, Quaternion.Euler(0,0,0));
            newObject.transform.parent = other.transform;
            newObject.AddComponent<BuffWeaponAttach>();
            newObject.GetComponent<BuffWeaponAttach>().damage = damage;
            newObject.GetComponent<BuffWeaponAttach>().enemyScript = other.GetComponent<EnemyScript>();
        }
    }
}
