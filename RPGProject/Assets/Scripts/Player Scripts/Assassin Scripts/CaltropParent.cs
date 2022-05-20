using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropParent : MonoBehaviour
{
    private float totalDistance = 0;
    public float damage;
    private Vector3 oldPos;
    public GameObject caltrop;
    private GameObject newCaltrop;

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
            totalDistance = 0;
        }
    }
}
