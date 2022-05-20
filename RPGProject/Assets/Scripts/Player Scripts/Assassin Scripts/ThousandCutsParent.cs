using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThousandCutsParent : MonoBehaviour
{
    public float damage, angle;
    private float rX, rY;
    private int daggerCount = 0, daggerTimer = 0;
    public GameObject dagger;
    private GameObject newDagger;

    void Update() {
        if (daggerTimer == 0) {
            rX = Random.Range(-0.5f, 0.5f);
            rY = Random.Range(-0.5f, 0.5f);
            newDagger = Instantiate(dagger, (new Vector3(Mathf.Cos(angle + rX), Mathf.Sin(angle + rY), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 45));
            newDagger.transform.parent = gameObject.transform;
            newDagger.GetComponent<ThousandCutsDagger>().damage = damage;
            daggerCount++;
            daggerTimer = 30;
            if (daggerCount == 10) {
                Destroy(gameObject);
            }
        }
        daggerTimer--;
    }
}
