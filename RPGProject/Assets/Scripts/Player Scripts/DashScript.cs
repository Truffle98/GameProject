using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private GameObject player;
    private Mage mageScript;
    private int dashCooldown = 0, dashCount = 0;
    private float angle;
    private Items itemsList;
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
        player.GetComponent<SpriteRenderer>().enabled = false;
        mageScript = player.GetComponent<Mage>();
        mageScript.dashing = true;
        mageScript.body.velocity = new Vector2(0, 0);

        itemsList = GameObject.Find("ItemObjectList").GetComponent<Items>();
        mageScript.cooldown1 = itemsList.GetCooldown(9); mageScript.itemCooldowns[0].SetActive(true); mageScript.itemCooldowns[0].GetComponent<CooldownUI>().SetMaxCooldown(itemsList.GetCooldown(9));
        mageScript.cooldown2 = itemsList.GetCooldown(9); mageScript.itemCooldowns[1].SetActive(true); mageScript.itemCooldowns[1].GetComponent<CooldownUI>().SetMaxCooldown(itemsList.GetCooldown(9));
        mageScript.cooldown3 = itemsList.GetCooldown(9); mageScript.itemCooldowns[2].SetActive(true); mageScript.itemCooldowns[2].GetComponent<CooldownUI>().SetMaxCooldown(itemsList.GetCooldown(9));
        mageScript.cooldown4 = itemsList.GetCooldown(9); mageScript.itemCooldowns[3].SetActive(true); mageScript.itemCooldowns[3].GetComponent<CooldownUI>().SetMaxCooldown(itemsList.GetCooldown(9));
    }

    // Update is called once per frame
    void Update()
    {
        if (mageScript.dashing == false || dashCount == 24) {
            mageScript.dashing = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
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
