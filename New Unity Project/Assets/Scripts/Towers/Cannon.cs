using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Tower
{
    public Transform barrel;
    public override void Aim(Transform target)
    {
        barrel.LookAt(target);
    }

    public override void Fire(Transform targets)
    {
        base.Fire(targets);
    }
}
