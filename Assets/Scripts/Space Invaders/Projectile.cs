using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Projectile : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D col;
    [SerializeField] AudioClip deathSound, killSound;
    [SerializeField] Rigidbody2D rb;
    [Tag, SerializeField] string enemyTag, derpEnemyTag, playerTag;
    [SerializeField] float speed;
    [SerializeField] bool enemyProjectile;

    private void Awake()
    {
        rb.velocity = Vector2.up * (enemyProjectile ? -speed : speed) ;
    }

    private void OnEnable()
    {
        EnemySpawner.OnPacifist += Destroy;
    }

    private void OnDisable()
    {
        EnemySpawner.OnPacifist -= Destroy;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(derpEnemyTag) && !enemyProjectile)
        {
            audioSource.PlayOneShot(killSound);
            EnemySpawner.OnDerpKilled?.Invoke();
            spriteRenderer.enabled = false;
            col.enabled = false;
            other.GetComponent<Animator>().SetTrigger("BobDeath");
            Destroy(this.gameObject, killSound.length);
        }
        else if (other.CompareTag(enemyTag) && !enemyProjectile)
        {
            EnemySpawner.OnEnemyDeath?.Invoke();
            audioSource.PlayOneShot(killSound);
            spriteRenderer.enabled = false;
            col.enabled = false;
            Destroy(other.gameObject);
            Destroy(this.gameObject, killSound.length);
        }
        else if (other.CompareTag(playerTag) && enemyProjectile)
        {
            EnemySpawner.OnDeath?.Invoke();
            audioSource.PlayOneShot(deathSound);
            spriteRenderer.enabled = false;
            col.enabled = false;
            Destroy(this.gameObject, deathSound.length);
        }
    }
}
