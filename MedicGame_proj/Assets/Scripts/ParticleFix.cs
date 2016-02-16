﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticleFix : MonoBehaviour {

    ParticleSystem.Particle[] unused = new ParticleSystem.Particle[1];

    void Awake() {
        GetComponent<ParticleSystemRenderer>().enabled = false;
    }

    void LateUpdate() {
        GetComponent<ParticleSystemRenderer>().enabled = GetComponent<ParticleSystem>().GetParticles(unused) > 0;
    }
}
