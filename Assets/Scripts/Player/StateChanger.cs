using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class StateChanger : MonoBehaviour
{
    public static StateChanger instance;
    [ReadOnly, Tag] public string currentStateTag;

    public delegate void OnStateChangedHandler(string state);
    public static event OnStateChangedHandler OnStateChanged;

    public void ChangeState(BaseController currentState, BaseController nextState)
    {
        currentState.Disable();
        nextState.Enable();
        OnStateChanged?.Invoke(currentStateTag);
    }
}
