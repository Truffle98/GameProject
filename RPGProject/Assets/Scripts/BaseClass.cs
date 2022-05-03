using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BaseClass : MonoBehaviour
{
    protected int damage = 10;
    protected int speed = 6;
    protected int health = 100;

    public float getDamage()
    {
        return damage;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getHealth()
    {
        return health;
    }
}
