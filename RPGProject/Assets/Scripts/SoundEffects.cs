using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip step, swing, pickup, enemyDied;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Walk()
    {
        AudioClip clip = step;
        audioSource.PlayOneShot(clip);
    }
    public void Swing()
    {
        AudioClip clip = swing;
        audioSource.PlayOneShot(clip);
    }
    public void Pickup()
    {
        AudioClip clip = pickup;
        audioSource.PlayOneShot(clip);
    }
    public void EnemyDied()
    {
        AudioClip clip = enemyDied;
        audioSource.PlayOneShot(clip);
    }
}
