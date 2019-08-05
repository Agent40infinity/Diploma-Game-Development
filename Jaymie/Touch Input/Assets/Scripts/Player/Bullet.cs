using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;

    public Rigidbody2D rigid;

    public void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rigid.velocity = transform.up * speed;
    }
}
