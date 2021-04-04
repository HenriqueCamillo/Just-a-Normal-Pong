using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MyBox;

public class CameraChangesOnTransition : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField, Tag] string playerTag;
    private CinemachineFramingTransposer composer;
    [SerializeField, Tag] string nextState;
    [SerializeField] float runnerDeadZoneHeight;
    [SerializeField] float spaceInvadersDeadZoneHeight;
    [SerializeField] float spaceInvaderwScreenY;

    private void Start()
    {
        composer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void ApplyChanges()
    {
        switch (nextState)
        {
            case "Runner":
                composer.m_DeadZoneHeight = runnerDeadZoneHeight;
                break;
            
            case "Space Invaders":
                composer.m_DeadZoneHeight = runnerDeadZoneHeight;
                composer.m_ScreenY = spaceInvaderwScreenY;
                break;
            
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            ApplyChanges();
        }
    }
}
