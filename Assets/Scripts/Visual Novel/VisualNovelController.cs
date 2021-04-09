using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNovelController : BaseController
{
    [SerializeField] VisualNovel vn;
    public override void Initialize() 
    { 
        vn.StartDialogue();
    }

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
