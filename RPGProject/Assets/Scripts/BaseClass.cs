using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BaseClass : MonoBehaviour
{
    //protected int baseDamage = 10;
    protected int baseSpeed = 6;
    //protected int baseHealth = 100;

/*    public float getDamage()
    {
        return baseDamage;
    }
*/
    public float getSpeed()
    {
        return baseSpeed;
    }

/*    public float getHealth()
    {
        return baseHealth;
    }
    */
}
