using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2Int shape;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject derpEnemyPrefab;
    [SerializeField] float spacement;

    public static Action OnDeath;
    public static Action OnReset;
    public static Action OnDerpKilled;
    bool inGame;
    
    private void OnEnable()
    {
        Spawn();
        OnDeath += Death;
    }

    private void OnDisable()
    {
        OnDeath -= Death;
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
                Instantiate(prefab, pos, Quaternion.identity, this.transform);
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
