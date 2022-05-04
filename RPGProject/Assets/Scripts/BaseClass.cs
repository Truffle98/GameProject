using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BaseClass : MonoBehaviour
{
    protected int baseDamage = 5;
    protected int baseSpeed = 6;
    protected int baseHealth = 100;

    public float GetBaseDamage()
    {
        return baseDamage;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }
    
}
