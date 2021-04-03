using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class PongAI : MonoBehaviour
{
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [SerializeField] Transform ball;
    [SerializeField] float speed;
    [SerializeField] float toleranceDistance;
    [SerializeField] float cooldown;
    private bool inCooldown;

    private float vDist;

    private void Awake()
    {
        if (ball == null)
            ball = FindObjectOfType<PongBall>().transform;
    }

    void Update()
    {
        if (inCooldown) 
            return;

        vDist = ball.position.y - transform.position.y;

        if (Mathf.Abs(vDist) <= toleranceDistance)
            rb.velocity = Vector2.zero;
        else if (vDist > 0f)
            rb.velocity = Vector2.up * speed;
        else
            rb.velocity = Vector2.down * speed;   
    }

    IEnumerator Cooldown() 
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;
    }
}
