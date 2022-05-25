using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropParent : MonoBehaviour
{
    private float totalDistance = 0;
    public float damage, effectDamage, slow = 0;
    public int effectType;
    private Vector3 oldPos;
    public GameObject caltrop, effect;
    private GameObject newCaltrop, newEffect;

    void Start()
    {
        oldPos = transform.position;
        Destroy(gameObject, 5);
    }

    void Update()
    {
        totalDistance += Vector3.Distance(oldPos, transform.position);
        oldPos = transform.position;
        if (totalDistance > 1) {
            newCaltrop = Instantiate(caltrop, transform.position, Quaternion.Euler(0,0,0));
            newCaltrop.GetComponent<Caltrop>().damage = damage;
            if (effect != null)
            {
                newEffect = Instantiate(effect, transform.position, Quaternion.Euler(0,0,0));
                newEffect.transform.parent = newCaltrop.transform;
                newEffect.transform.localScale = new Vector3 (1.8f, 3f, 1);
                newCaltrop.AddComponent<BuffWeapon>();
                newCaltrop.GetComponent<BuffWeapon>().effect = effect;
                newCaltrop.GetComponent<BuffWeapon>().damage = effectDamage;
                newCaltrop.GetComponent<BuffWeapon>().type = effectType;
            }
            if (slow > 0) {
                newCaltrop.GetComponent<Caltrop>().slow = slow;
            }
            totalDistance = 0;
        }
    }
}
