using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class EnemyShoot : MonoBehaviour
{
    [AutoProperty, SerializeField, HideInInspector] AudioSource audioSource;
    [SerializeField] AudioClip shootClip;
    [SerializeField] float minCooldown, maxCooldown;
    [SerializeField] GameObject projectile;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        Instantiate(projectile, this.transform.position + Vector3.down, Quaternion.Euler(0f, 0f, 180f));
        audioSource.PlayOneShot(shootClip);
        Shoot();
    }

    private void OnDestroy()
    {
        StopCoroutine(nameof(Shoot));
    }
    
    private void OnEnable()
    {
        EnemySpawner.OnDerpKilled += Stop;    
    }
    private void OnDisable()
    {
        EnemySpawner.OnDerpKilled -= Stop;    
    }
    void Stop()
    {
        StopAllCoroutines();
    }
}
