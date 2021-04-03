using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceInvadersController : BaseController
{
    [PositiveValueOnly, SerializeField] float speed;
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [SerializeField] GameObject projectile;

    public override void Initialize()
    {
        rb.gravityScale = 0f;
        rb.constraints = 0; 
    }


    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectile, this.transform.position + Vector3.up, Quaternion.identity);
    }
}
