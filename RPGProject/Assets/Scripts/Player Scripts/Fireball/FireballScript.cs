using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public int lifespan;
    private float angle, speed;
    private int variation = 2, itemID = 3, count = 0;
    //private PlayerStats playerStats;
    public GameObject pool, fragment;
    private GameObject newFragment;
    
    // Start is called before the first frame update
    void Start()
    {
        //playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        //variation = playerStats.GetSpecialEffects(itemID);
        angle = gameObject.GetComponent<ProjectileScript>().angle;
        speed = gameObject.GetComponent<ProjectileScript>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1) {
            count++;
        }
        if (count >= lifespan) {

            if (variation == 2 && count >= lifespan / 2) {
                CreateFragments();
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
            /*if (other.gameObject.CompareTag("Enemy")) {
                CreateFragments();
            }*/
        }
    }

    void CreateFragments() {

        newFragment = Instantiate(fragment, transform.position, Quaternion.Euler(0, 0, angle));
        newFragment.GetComponent<SplittingScript>().angle = angle;
        newFragment.GetComponent<SplittingScript>().speed = speed;
        //newFragment.GetComponent<Rigidbody2D>().velocity = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0) * speed;
        newFragment = Instantiate(fragment, transform.position, Quaternion.Euler(0, 0, angle));
        newFragment.GetComponent<SplittingScript>().angle = angle - 0.5f;
        newFragment.GetComponent<SplittingScript>().speed = speed;
        newFragment = Instantiate(fragment, transform.position, Quaternion.Euler(0, 0, angle));
        newFragment.GetComponent<SplittingScript>().angle = angle + 0.5f;
        newFragment.GetComponent<SplittingScript>().speed = speed;

    }
}
