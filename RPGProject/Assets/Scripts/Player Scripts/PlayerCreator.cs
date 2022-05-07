using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    private MenuTest menuTest;
    public GameObject mage;
    private int decision;

    // Start is called before the first frame update
    void Start()
    {
        menuTest = GameObject.Find("Test").GetComponent<MenuTest>();
        decision = menuTest.ReturnDecision();
        if (decision == 0)
        {
            Instantiate(mage, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
