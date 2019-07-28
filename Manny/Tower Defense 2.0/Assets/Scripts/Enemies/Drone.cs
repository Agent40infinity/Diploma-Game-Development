using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{ 
    public float firerate = 1f;

    public override void Attack(Transform target)
    {
        base.Attack(target);
    }

    public override void Death()
    {
        base.Death();
    }
}
