using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceInvadersController : BaseController
{
    [AutoProperty, SerializeField] Animator anim;
    [PositiveValueOnly, SerializeField] float speed;
    [SerializeField] Transform bulletPos;
    [AutoProperty, SerializeField, HideInInspector] AudioSource audioSource;
    [SerializeField] AudioClip shootClip;
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [SerializeField] GameObject projectile;
    bool inCooldown = true;

    public override void Initialize()
    {
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; 
        Invoke(nameof(Transition), 0f);
    }

    void Transition()
    {
        anim.SetTrigger("Transition");

    }

    public override void Disable()
    {
        anim.SetTrigger("Vanish");
        this.enabled = false;
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized * speed;

        if (!inCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        inCooldown = true;
        anim.SetTrigger("Shoot");
    }

    public void ActuallyNowYouCanShoot()
    {
        Instantiate(projectile, bulletPos.position, Quaternion.identity);
        audioSource.PlayOneShot(shootClip);
        inCooldown = false;
        
    }

    public void ResetCooldown()
    {
        inCooldown = false;
    }

    private void OnEnable()
    {
        EnemySpawner.OnBobFinshedDeath += GoToVisualNovel;
    }

    private void OnDisable()
    {
        EnemySpawner.OnBobFinshedDeath -= GoToVisualNovel;
        
    }

    private void GoToVisualNovel()
    {
        stateChanger.ChangeState(this, nextState);
    }
}
