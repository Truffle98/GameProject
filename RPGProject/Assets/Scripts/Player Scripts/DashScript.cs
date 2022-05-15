using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private GameObject player;
    private Mage mageScript;
    private int dashCooldown = 0, dashCount = 0;
    private float angle;
    private Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection = shootDirection.normalized;
        angle = Mathf.Atan2(shootDirection.y, shootDirection.x);

        player = GameObject.Find("Mage(Clone)");
        mageScript = player.GetComponent<Mage>();
        mageScript.dashing = true;
        mageScript.body.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (mageScript.dashing == false || dashCount == 24) {
            mageScript.dashing = false;
            Destroy(gameObject);
        }
        if (dashCooldown <= 0) {
            player.transform.position = new Vector3 (Mathf.Cos(angle) * 0.2f, Mathf.Sin(angle) * 0.2f, 0) + player.transform.position;
            dashCount++;
            dashCooldown = 3;
        }

        dashCooldown--;
    }
}
