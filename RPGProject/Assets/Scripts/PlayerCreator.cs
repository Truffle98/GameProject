using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.FindChild("Mage").gameObject;
        Instantiate(player, new Vector3(0, 0, 0), new Quaternion(0,0,0,0));
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
