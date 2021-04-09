using UnityEngine;
public class AmongUsController : BaseController
{
    [SerializeField] AmongUs mogus;
    public override void Initialize() { mogus.StartAmongUs(); }

}
