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
    public bool on {get;private set;}

    void Awake()
    {
        emi = particles.emission;
        on = false;
    }

    public void Reset()
    {
        background.SetActive(true);
        foreground.SetActive(false);
        on = false;
    }

    public void TurnOn(float percIntensity)
    {
        foreground.SetActive(true);
        emi.SetBurst(0, new ParticleSystem.Burst(0f, minParticles + percIntensity * (maxParticles - minParticles)));
        particles.Play();
        on = true;
    }
}
