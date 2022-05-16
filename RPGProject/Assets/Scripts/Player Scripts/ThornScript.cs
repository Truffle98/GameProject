using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    private GameObject player;
    private int timePassed = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mage(Clone)");
        transform.position = player.transform.position;
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0.1f * timePassed);
        timePassed++;
    }
}
