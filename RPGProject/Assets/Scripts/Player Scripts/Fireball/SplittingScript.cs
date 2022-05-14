using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingScript : MonoBehaviour
{
    public float angle, speed;
    private Vector3 shootDirection;
    private bool moving = false;
    void Update() {
        if (moving == false) {
            shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
            GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
            moving = true;
        }
        
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        //transform.Translate(shootDirection * speed * Time.deltaTime);
    }*/

}
