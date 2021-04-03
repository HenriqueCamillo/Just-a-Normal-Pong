using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Obstacle : MonoBehaviour
{
    [SerializeField, Tag] string playerTag;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.left * speed;        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            RunnerObjectGenerator.OnDeath?.Invoke();
    }

    private void OnEnable()
    {
        RunnerObjectGenerator.OnReset += DestroyObj;
        RunnerObjectGenerator.OnDeath += Stop;
    }

    private void OnDisable()
    {
        RunnerObjectGenerator.OnReset -= DestroyObj;
        RunnerObjectGenerator.OnDeath -= Stop;
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
        Time.timeScale = 0f;
    }

    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
