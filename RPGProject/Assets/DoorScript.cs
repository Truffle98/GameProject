using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public float newLocX, newLocY, cameraSize;
    public string scene;
    private Camera cameraVar;

    void OnTriggerStay2D(Collider2D other) {

        if (other.gameObject.CompareTag("Character")) {
        
            SceneManager.LoadScene(scene);
            other.gameObject.transform.position = new Vector3 (newLocX, newLocY, 0);
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = cameraSize;
            
        }

    }
}
