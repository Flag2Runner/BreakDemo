using System;
using UnityEngine;

[RequireComponent(typeof(SpawnerCompenent))]
public class Spawner : Enemy
{
    [SerializeField] private ParticleSystemSpecs[] _particleSystemSpecs; 
    private SpawnerCompenent _spawnerCompenent;

    protected override void Awake()
    {
        base.Awake();
        _spawnerCompenent = GetComponent<SpawnerCompenent>();
    }

    public override void Attack(GameObject target)
    {
        if (_spawnerCompenent) {_spawnerCompenent.StartSpawning();}
    }

    protected override void OnDead()
    {
        foreach (var particleSystemSpecs in _particleSystemSpecs)
        {
            ParticleSystem newVFX = Instantiate(particleSystemSpecs.particleSystem, transform.position, Quaternion.identity);
            newVFX.transform.localScale = Vector3.one * particleSystemSpecs.size;
        }
    }

}
