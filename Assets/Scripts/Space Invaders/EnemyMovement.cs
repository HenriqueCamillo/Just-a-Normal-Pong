using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Tag] string playerTag;
    [SerializeField] float cooldown;
    [SerializeField] float offset;
    private Vector3[] path = { Vector3.right, Vector3.right, Vector3.right, Vector3.down, Vector3.left, Vector3.left, Vector3.left, Vector3.down};
    private int i;

    private void Awake()
    {
        InvokeRepeating(nameof(Move), 0f, cooldown);
    }

    private void Move()
    {
        this.transform.position += path[i] * offset;
        i = (i + 1) % path.Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            EnemySpawner.OnDeath?.Invoke();
        }
    }

    private void OnEnable()
    {
        EnemySpawner.OnDeath += Stop;
        EnemySpawner.OnPacifist += Stop;
        EnemySpawner.OnDerpKilled += Stop;
        EnemySpawner.OnReset += Destroy;
    }

    private void OnDisable()
    {
        EnemySpawner.OnDeath -= Stop;
        EnemySpawner.OnDerpKilled -= Stop;
        EnemySpawner.OnPacifist -= Stop;
        EnemySpawner.OnReset -= Destroy;
    }

    private void Stop()
    {
        CancelInvoke();
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void TriggerEnd()
    {
        EnemySpawner.OnBobFinshedDeath?.Invoke();
    }
}
