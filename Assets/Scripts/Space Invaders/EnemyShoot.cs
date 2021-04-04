using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
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
        Shoot();
    }

    private void OnDestroy()
    {
        StopCoroutine(nameof(Shoot));
    }
}
