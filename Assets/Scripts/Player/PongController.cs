using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class PongController : BaseController
{
    // Update is called once per frame
    [PositiveValueOnly, SerializeField] float speed;
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    
    private float movement;

    public override void Initialize()
    {
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(0f, movement * speed); 
    }
}
