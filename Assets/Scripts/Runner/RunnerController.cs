using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class RunnerController : BaseController
{
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [AutoProperty, SerializeField, HideInInspector] AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [PositiveValueOnly, SerializeField] float jumpForce;

    [Foldout("Better Jumping", true)]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    public override void Initialize()
    {
        this.enabled = true;
        rb.gravityScale = 1f;
    }

    void Update()
    {
        if (PressedJump())
        {
            Jump();
        }

        if (rb.velocity.y < 0) 
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        if (rb.velocity.y > 0 && !PressingJump())
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

    }

    private bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
    }

    private bool PressingJump() 
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(jumpSound);
    }


    public override void Disable()
    {
        RunnerObjectGenerator.OnReset?.Invoke();
        this.enabled = false;
    }
}
