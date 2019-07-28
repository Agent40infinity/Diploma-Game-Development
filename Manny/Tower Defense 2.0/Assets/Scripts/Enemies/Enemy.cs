using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;
    public float speed = 10f;

    //protected List<Transform> DetectObsticles()
    //{
    //
    //}

    public virtual void Attack(Transform target)
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {

    }
}
