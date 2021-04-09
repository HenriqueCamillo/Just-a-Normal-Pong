using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelController : BaseController
{
    public override void Initialize() { }

    protected override void Awake()
    {
        base.Awake();
        VisualNovel.OnAmongUs += Amogus;
        EnemySpawner.OnPacifist += Pacifist;
    }

    private void OnDestroy()
    {
        VisualNovel.OnAmongUs -= Amogus;
        EnemySpawner.OnPacifist -= Pacifist;
    }

    void Pacifist()
    {
        VisualNovel.Pacifist = true;
    }

    void Amogus()
    {
        stateChanger.ChangeState(this, nextState);
    }
}
