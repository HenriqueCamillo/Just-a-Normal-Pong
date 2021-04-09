using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public abstract class BaseController : MonoBehaviour
{
    protected StateChanger stateChanger;
    [SerializeField] protected BaseController nextState;
    [Tag, SerializeField] string stateTag;


    protected virtual void Awake()
    {
        stateChanger = GetComponent<StateChanger>();
        // Enable();
    }

    public abstract void Initialize();

    public virtual void Enable() 
    {
        this.enabled = true;
        stateChanger.currentStateTag = stateTag;
        Initialize();
    }

    public virtual void Disable() 
    {
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(stateTag))
        {
            stateChanger.ChangeState(this, nextState);
        }
    }
}
