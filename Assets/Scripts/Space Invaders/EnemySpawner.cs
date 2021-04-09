using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2Int shape;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject derpEnemyPrefab, derpReference;
    [SerializeField] float spacement;

    public static Action OnDeath;
    public static Action OnReset;
    public static Action OnDerpKilled;
    public static Action OnBobFinshedDeath;
    public static Action OnEnemyDeath;
    public static Action OnPacifist;
    bool inGame;

    int nEnemies, deathCounter;

    private void Start()
    {
        nEnemies = shape.x * shape.y - 1;
    }
    
    private void Count()
    {
        deathCounter++;
        if (deathCounter == nEnemies)
        {
            OnPacifist?.Invoke();
            print("Pacifist");
        }
    }

    private void ResetCounter()
    {
        deathCounter = 0;
    }

    private void OnEnable()
    {
        Spawn();
        OnDeath += Death;
        OnEnemyDeath += Count;
        OnReset += ResetCounter;
        OnPacifist += Pacifist;
    }

    private void OnDisable()
    {
        OnDeath -= Death;
        OnEnemyDeath -= Count;
        OnReset -= ResetCounter;
        OnPacifist -= Pacifist;
    }

    private void Pacifist()
    {
        derpReference.GetComponent<Animator>().SetTrigger("Pacifist");
    }

    private void Death()
    {
        inGame = false; 
        Time.timeScale = 0f;
    }

    private void Spawn()
    {
        inGame = true;
        Vector2Int derpPosition = new Vector2Int(shape.x - 1, UnityEngine.Random.Range(0, shape.y));

        Vector2 pos;
        GameObject prefab;
        for (int i = 0; i < shape.x; i++)
        {
            for (int j = 0; j < shape.y; j++)
            {
                pos = (Vector2)this.transform.position + Vector2.right * j * spacement + Vector2.down * i * spacement;   
                
                prefab = (derpPosition.x == i && derpPosition.y == j) ? derpEnemyPrefab : enemyPrefab;
                GameObject enemy = Instantiate(prefab, pos, Quaternion.identity, this.transform);
                if(derpPosition.x == i && derpPosition.y == j)
                    derpReference = enemy;
            }
        }
    }

    private void Update()
    {
        if (!inGame && Input.GetKeyDown(KeyCode.Space))
        {
            OnReset?.Invoke();
            Time.timeScale = 1f;
            Spawn();
        }
    }

}
