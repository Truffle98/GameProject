using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerStats
{
    public Rigidbody2D fireball;
    private Vector3 shootDirection;
    int cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Creates a fireball and puts it on a 2 second cooldown
        if (Input.GetMouseButtonDown(0) && cooldown == 0) {

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;

            Rigidbody2D fireballInstance = Instantiate(fireball, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
            cooldown = 100;

        }
        if (cooldown > 0) {
            cooldown--;
        }
    }
}
