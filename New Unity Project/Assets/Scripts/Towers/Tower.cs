using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 10;
    public float firerate = 0.5f;
    public float range = 5f;
    public int cost = 5;
    public int level = 1;

    private float fireTimer = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public Transform GetClosestTarget(List<Transform> targets)
    {
        float min = float.MaxValue;
        Transform closest = null;
        foreach (var target in targets)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance < min)
            {
                min = distance;
                closest = target;
            }
        }
        return closest;
    }

    protected List<Transform> DetectTargets()
    {
        List<Transform> result = new List<Transform>();
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                result.Add(enemy.transform);
            }
        }
        return result;
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= firerate)
        {
            List<Transform> targets = DetectTargets();
            Transform closestTarget = GetClosestTarget(targets);
            Aim(closestTarget);
            Fire(closestTarget);
            fireTimer = 0;
        }
    }

    public virtual void Fire(Transform targets)
    {
        //Physics.OverlapSphere();
    }

    public virtual void Aim(Transform target)
    {

    }
}
