using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeaponAttach : MonoBehaviour
{
    public float damage;
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
            enemyScript.TakeDamage(damage);
            cooldown = 250;
            count++;
        }
        cooldown--;
    }
}
