using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class StateChanger : MonoBehaviour
{
    [ReadOnly, Tag] public string currentStateTag;

    public void ChangeState(BaseController currentState, BaseController nextState)
    {
        currentState.Disable();
        nextState.Enable();
    }
}
