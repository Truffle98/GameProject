using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    private MenuTest menuTest;
    public GameObject mage;
    public GameObject assassin;
    private GameObject newChar, characterParent;
    private int decision;

    // Start is called before the first frame update
    void Start()
    {
        menuTest = GameObject.Find("Test").GetComponent<MenuTest>();
        decision = menuTest.ReturnDecision();
        characterParent = GameObject.Find("CharacterParent");
        decision = 1;
        if (decision == 0)
        {
            newChar = Instantiate(mage, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            newChar.transform.parent = characterParent.transform;
        }
        if (decision == 1)
        {
            newChar = Instantiate(assassin, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            newChar.transform.parent = characterParent.transform;
        }
        Destroy(gameObject);
    }
}
