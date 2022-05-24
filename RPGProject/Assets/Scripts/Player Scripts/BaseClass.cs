using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BaseClass : MonoBehaviour
{
    protected static int baseDamage = 1;
    protected static int baseSpeed = 6;
    protected static int baseHealth = 100;
    protected static int baseMana = 50;
    protected static float baseArmorMultiplier = 1;
    public GameObject[] gameItems;

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

    public float GetBaseArmorMultiplier()
    {
        return baseArmorMultiplier;
    }
    
}
