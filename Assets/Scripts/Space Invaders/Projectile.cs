using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [Tag, SerializeField] string enemyTag, derpEnemyTag, playerTag;
    [SerializeField] float speed;
    [SerializeField] bool enemyProjectile;

    private void Awake()
    {
        rb.velocity = Vector2.up * (enemyProjectile ? -speed : speed) ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(derpEnemyTag) && !enemyProjectile)
        {
            EnemySpawner.OnDerpKilled?.Invoke();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag(enemyTag) && !enemyProjectile)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag(playerTag) && enemyProjectile)
        {
            EnemySpawner.OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }
}