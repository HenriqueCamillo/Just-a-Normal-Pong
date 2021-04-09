using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);
        if (!other.CompareTag("Player"))
            Destroy(other.gameObject);
    }
}
