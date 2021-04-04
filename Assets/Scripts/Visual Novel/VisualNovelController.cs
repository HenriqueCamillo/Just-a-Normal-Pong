using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelController : BaseController
{
    public override void Initialize() { }

    private void OnEnable()
    {
        VisualNovel.OnAmongUs += Amogus;
    }

    private void OnDisable()
    {
        VisualNovel.OnAmongUs -= Amogus;
    }

    void Amogus()
    {
        stateChanger.ChangeState(this, nextState);
    }
}
