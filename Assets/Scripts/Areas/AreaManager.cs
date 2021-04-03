using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class AreaManager : MonoBehaviour
{
    [SerializeField, Tag] string areaTag;
    private void OnEnable()
    {
        StateChanger.OnStateChanged += EnableOrDisableArea;        
    }

    private void OnDisable()
    {
        StateChanger.OnStateChanged -= EnableOrDisableArea;
    }

    private void EnableOrDisableArea(string state)
    {
        bool active = areaTag == state;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
     

    }

    protected virtual void EnableArea() { }

    protected virtual void DisableArea() { }
}
