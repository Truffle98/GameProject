using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip swing, hit, pickup, walking;
    private AudioSource source;
    
    void Start() {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySwing () {
        source.clip = swing;
        source.Play();
    }
    /*public void PlayHit () {
        hit.Play();
    }
    public void PlayPickup () {
        pickup.Play();
    }*/
    public void PlayWalking () {
        source.clip = walking;
        source.Play();
    }
}
