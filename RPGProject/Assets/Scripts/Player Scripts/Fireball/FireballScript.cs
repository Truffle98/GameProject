using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public int lifespan;
    private int variation = 1, itemID = 3, count = 0;
    private PlayerStats playerStats;
    public GameObject pool;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        //variation = playerStats.GetSpecialEffects(itemID);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1) {
            count++;
        }
        if (count >= lifespan) {

            if (variation == 2 && count >= lifespan / 2) {
                //Instantiate(pool, transform.position, new Quaternion(0, 0, 0, 0));
            }

            if (variation == 1) {
                Instantiate(pool, transform.position, new Quaternion(0, 0, 0, 0));
            }
            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") ) {
            
            if (variation == 1) {
                Instantiate(pool, transform.position, new Quaternion(0, 0, 0, 0));
            }
            if (other.gameObject.CompareTag("Wall")) {
                Destroy(gameObject);
            }
        }
    }
}
