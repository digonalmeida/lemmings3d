using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressTarget : MonoBehaviour
{

    public GameObject background;
    public GameObject foreground;
    public ParticleSystem particles;

    public void Trigger()
    {
        background.SetActive(false);
        foreground.SetActive(true);
        particles.Play();
    }

    public void Reset()
    {
        background.SetActive(true);
        foreground.SetActive(false);
    }
}
