using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeecherScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EnemyScript enemyScript;
    private Mage mageScript;
    private int cooldown = 0;
    public GameObject player, target;
    public float lifespan, damage;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.Find("MageClass(Clone)");
        mageScript = player.GetComponent<Mage>();
        enemyScript = target.GetComponent<EnemyScript>();

        lineRenderer.SetPosition(0, player.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, player.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

        if (cooldown == 0) {

            enemyScript.TakeDamage(damage);
            mageScript.Heal(damage);
            cooldown = 250;

        }
        cooldown--;
    }
}
