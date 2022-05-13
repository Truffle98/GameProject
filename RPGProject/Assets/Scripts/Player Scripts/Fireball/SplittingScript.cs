using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingScript : MonoBehaviour
{
    public float angle, speed;
    private Vector3 shootDirection;

    void Start() {
        shootDirection = new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(shootDirection * speed * Time.deltaTime);
    }

}
