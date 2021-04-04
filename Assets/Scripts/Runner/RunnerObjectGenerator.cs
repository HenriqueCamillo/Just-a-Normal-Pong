using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBox;

public class RunnerObjectGenerator : MonoBehaviour
{
    public bool spawning;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] float spawnDelayMin, spawnDelayMax;

    public static Action OnDeath;
    public static Action OnReset;


    public void StartSpawning()
    {
        spawning = true;
        SpawnObstacle();
    }    

    private void SpawnObstacle() 
    {
        GameObject prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
        Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);

        if (spawning)
        {
            float spawnDelay = UnityEngine.Random.Range(spawnDelayMin, spawnDelayMax);
            Invoke(nameof(SpawnObstacle), spawnDelay);
        }
    }

    private void OnEnable()
    {
        OnDeath += StopSpawning;
        StartSpawning();
    }

    private void OnDisable()
    {
        CancelInvoke();
        spawning = false;
        OnDeath -= StopSpawning;
    }

    private void StopSpawning()
    {
        spawning = false;
    }

    private void Update()
    {
        if (!spawning && PressedJump())
        {
            OnReset?.Invoke();
            Time.timeScale = 1f;
            spawning = true;
            // StartSpawning();
        }
    }

    private bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
    }
}
