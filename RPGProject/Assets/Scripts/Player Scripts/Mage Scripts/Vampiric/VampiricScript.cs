using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricScript : MonoBehaviour
{

    private float damage, multiplier;
    private int variation = 1, itemID = 6;
    public GameObject leecher;
    private GameObject newLeecher;
    //private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<ProjectileScript>().damage;
        //playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        //variation = playerStats.GetSpecialEffects(itemID);
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") ) {

            if (other.gameObject.CompareTag("Wall")) {
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Enemy")) {
                if (variation == 0) {
                    GameObject.Find("MageClass(Clone)").GetComponent<Mage>().Heal(damage * 0.125f);
                } else if (variation == 1) {
                    newLeecher = Instantiate(leecher, other.gameObject.transform.position + new Vector3 (0, 2, 0), Quaternion.Euler(0, 0, 0));
                    newLeecher.transform.parent = other.gameObject.transform;
                    newLeecher.GetComponent<LeecherScript>().target = other.gameObject;
                    newLeecher.GetComponent<LeecherScript>().damage = damage * 0.2f;
                }
                
            }
        }
    }
}
