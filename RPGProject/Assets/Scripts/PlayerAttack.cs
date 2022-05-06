using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector3 shootDirection;
    private int cooldown = 0;
    private int item1;
    private PlayerStats playerStats;
    private GameObject item1Object;
    private Items itemsList;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Stats").GetComponent<PlayerStats>();
        itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();
        item1 = playerStats.GetEquippedItem(0);
        item1Object = itemsList.GetItemObject(item1);
    }

    // Update is called once per frame
    void Update()
    {
        //Creates a fireball and puts it on a 2 second cooldown
        if (Input.GetMouseButtonDown(0) && cooldown == 0 && Time.timeScale == 1) {

            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection-transform.position;
            shootDirection = shootDirection.normalized;

            Instantiate(item1Object, (new Vector3(shootDirection.x, shootDirection.y, 0) + transform.position), Quaternion.Euler(new Vector3(0,0,0)));
            cooldown = 100;

        }

        if (cooldown > 0 && Time.timeScale == 1) {
            cooldown--;
        }
    }
}
