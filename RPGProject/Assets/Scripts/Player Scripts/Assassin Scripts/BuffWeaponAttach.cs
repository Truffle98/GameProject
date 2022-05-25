using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeaponAttach : MonoBehaviour
{
    public float damage;
    public int type;
    public EnemyScript enemyScript;
    private GameObject[] effects;
    private Transform[] transforms;
    private int cooldown = 0, count = 0;

    void Start() 
    {
        transforms = transform.parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transforms.Length; i++) 
        {
            if (transforms[i].gameObject.TryGetComponent<BuffWeaponAttach>(out BuffWeaponAttach b) && transforms[i].gameObject != gameObject)
            {
                Destroy(transforms[i].gameObject);
            }
        }
    }

    void Update()
    {  
        if (count == 5) {
            Destroy(gameObject);
        }
        if (cooldown == 0)
        {  
            if (type == 0 || type == 2) {
                enemyScript.TakeDamage(damage);
                cooldown = 250;
                count++;
                if (type == 2) {
                    enemyScript.ReduceDamage(0.25f, 750);
                    type = 0;
                }
            } else if (type == 1) {
                enemyScript.Slow(0.5f, 750);
            }
        }
        if (cooldown == -750) {
            Destroy(gameObject);
        }
        cooldown--;
    }
}
