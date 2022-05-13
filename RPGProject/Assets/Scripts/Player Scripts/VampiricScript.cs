using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricScript : MonoBehaviour
{

    private float damage, multiplier;
    // Start is called before the first frame update
    void Start()
    {
        //damage = GetComponent
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        /*if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy") ) {
            
            if (variation == 1) {
                Instantiate(pool, transform.position, new Quaternion(0, 0, 0, 0));
            }
            if (other.gameObject.CompareTag("Wall")) {
                Destroy(gameObject);
            }
            /*if (other.gameObject.CompareTag("Enemy")) {
                CreateFragments();
            }
        }
    */}
}
