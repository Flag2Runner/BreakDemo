using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCompenent : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private Transform spawnTransform;

    private Animator _animator;
    private static readonly int SpawnId = Animator.StringToHash("Spawn");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool StartSpawning()
    {
        if (spawnPrefabs.Length == 0) 
            return false;
        if (_animator != null)
        {
            _animator.SetTrigger(SpawnId);
        }
        else
        {
            Spawn();
        }
        return true;
    }

    public void Spawn()
    {
        int randomPrefab = Random.Range(0, spawnPrefabs.Length);
        GameObject newSpawnPrefab = Instantiate(spawnPrefabs[randomPrefab], spawnTransform.position, spawnTransform.rotation);
        IspawnerInterface spawnerInterface = newSpawnPrefab.GetComponent<IspawnerInterface>();
        if (spawnerInterface != null)
        {
            spawnerInterface.GetSpawnerOwner(gameObject);
        }

    }
}
