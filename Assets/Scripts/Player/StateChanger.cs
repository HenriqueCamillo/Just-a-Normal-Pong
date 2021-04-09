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

    [SerializeField] BaseController startState;
    [SerializeField] BaseController currentState;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }


    private void Start()
    {
        startState.Enable();
    }

    public void ChangeState(BaseController currentState, BaseController nextState)
    {
        currentState.Disable();
        nextState.Enable();
        OnStateChanged?.Invoke(currentStateTag);

        this.currentState = nextState;
    }

    public void ChangeState()
    {

        ChangeState(currentState, currentState.nextState);
    }
}
