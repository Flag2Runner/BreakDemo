using System;
using UnityEngine;

[Serializable]

public struct ParticleSystemSpecs
{
    public ParticleSystem particleSystem;
    public float size;

    public ParticleSystemSpecs(ParticleSystem particleSystem, float size)
    {
        this.size = 1f;
        this.particleSystem = null;
    }
}