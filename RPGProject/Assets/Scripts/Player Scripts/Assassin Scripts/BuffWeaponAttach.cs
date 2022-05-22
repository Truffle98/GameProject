using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeaponAttach : MonoBehaviour
{
    public float damage;
    public EnemyScript enemyScript;
    private int cooldown = 0;

    void Update()
    {
        if (cooldown == 0)
        {
            enemyScript.TakeDamage(damage);
            cooldown = 250;
        }
        cooldown--;
    }
}
