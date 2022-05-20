using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    private GameObject player, wheel2;
    public GameObject wheel2Object;
    private int timePassed = 0;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        /*player = GameObject.Find("Assassin(Clone)");
        transform.position = player.transform.position;
        transform.rotation = Quaternion.Euler(0,0,0);*/
        if (gameObject.CompareTag("AOE")) {
            wheel2 = Instantiate(wheel2Object, new Vector3 (0,0,0), Quaternion.Euler(0,0,0));
            wheel2.transform.parent = transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,rotationSpeed * timePassed);
        timePassed++;
    }
}
