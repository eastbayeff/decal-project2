using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStomp : MonoBehaviour
{
    private ParticleSystem particles;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("noteblock stomped");
        particles = GetComponent<ParticleSystem>();

        if (particles != null)
            particles.Emit(10);

        // allow for control of music and independent soundFX
        AudioController.Instance.BlipVolume();
        audioSource.Play();
    }
}
