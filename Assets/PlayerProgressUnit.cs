using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressUnit : MonoBehaviour
{

    public GameObject background;
    public GameObject foreground;
    public ParticleSystem particles;
    private ParticleSystem.EmissionModule emi;

    public float minParticles;
    public float maxParticles;

    void Awake()
    {
        emi = particles.emission;
    }

    public void Reset()
    {
        background.SetActive(true);
        foreground.SetActive(false);
    }

    public void TurnOn(float percIntensity)
    {
        foreground.SetActive(true);
        emi.SetBurst(0, new ParticleSystem.Burst(0f, minParticles + percIntensity * (maxParticles - minParticles)));
        particles.Play();
    }
}
