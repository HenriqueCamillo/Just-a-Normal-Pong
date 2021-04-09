using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class PongController : BaseController
{
    // Update is called once per frame
    [SerializeField, AutoProperty] Animator animator;
    [PositiveValueOnly, SerializeField] float speed;
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [SerializeField] float camSize = 5f;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform playerPos;
    [SerializeField] PongBall ball;
    [SerializeField] float deadzoneHeight = 866482f, screenY = 0.5f; 
    private CinemachineFramingTransposer composer;
    bool replay;

    private float movement;

    protected override void Awake()
    {
        base.Awake();
        composer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public override void Initialize()
    {
        this.transform.position = playerPos.position;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        animator.Play("Paddle");

        composer.m_ScreenY = screenY;
        composer.m_DeadZoneHeight = deadzoneHeight;
        cam.enabled = false;
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        StartCoroutine(EnableCam());
        InvokeRepeating(nameof(LerpCam), 0f, Time.fixedDeltaTime);

    }

    private IEnumerator EnableCam()
    {
        yield return null;
        cam.enabled = true;
        if (replay)
        {
            ball.Throw(true);
        }

        replay = true;
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(0f, movement * speed); 
    }

    private void LerpCam()
    {
        if (Mathf.Abs(camSize - cam.m_Lens.OrthographicSize) < 0.1f)
        {
            cam.m_Lens.OrthographicSize = camSize;
            CancelInvoke(nameof(LerpCam));

        }
        else 
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, camSize, Time.deltaTime);
        }
    }

}
