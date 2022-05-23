using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThousandCutsParent : MonoBehaviour
{
    public float damage, angle, effectDamage;
    private float rX, rY;
    private int daggerCount = 0, daggerTimer = 0;
    public GameObject dagger, effect;
    private GameObject newDagger, newEffect;

    void Update() {
        if (daggerTimer == 0) {
            rX = Random.Range(-0.25f, 0.25f);
            rY = Random.Range(-0.25f, 0.25f);
            newDagger = Instantiate(dagger, (new Vector3(Mathf.Cos(angle + rX), Mathf.Sin(angle + rY), 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 45));
            newDagger.transform.parent = gameObject.transform;
            newDagger.GetComponent<ThousandCutsDagger>().damage = damage;
            daggerCount++;
            daggerTimer = 30;
            if (effect != null)
            {
                newEffect = Instantiate(effect, (new Vector3(Mathf.Cos(angle + rX) * 1.3f, Mathf.Sin(angle + rY) * 1.3f, 0) + transform.position), Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                newEffect.transform.parent = newDagger.transform;
                newEffect.transform.localScale = new Vector3 (1.8f, 3f, 1);
                newDagger.AddComponent<BuffWeapon>();
                newDagger.GetComponent<BuffWeapon>().effect = effect;
                newDagger.GetComponent<BuffWeapon>().damage = effectDamage;
            }
            if (daggerCount == 10) {
                Destroy(gameObject);
            }
        }
        daggerTimer--;
    }
}
