using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingGiveSkillEffect : MonoBehaviour
{

    private ParticleSystem particles;
    private LemmingStateController parentLemming;

    void OnEnable()
    {
        GameEvents.Lemmings.LemmingUsedSkill += Activate;
    }

    void OnDisable()
    {
        GameEvents.Lemmings.LemmingUsedSkill -= Activate;
    }

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
		parentLemming = GetComponentInParent<LemmingStateController>();
    }

    public void Activate(LemmingStateController lemming)
    {
        if (lemming == parentLemming)
        {
            particles.Play();
        }
    }
}
