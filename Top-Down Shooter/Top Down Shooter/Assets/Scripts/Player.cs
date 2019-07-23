using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //General:
    public float xAxis;
    public float yAxis;
    public float speed = 2f;

    //Reference:
    public Rigidbody2D rigid;

    public void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }

    public void FixedUpdate()
    {
        rigid.velocity = new Vector2(xAxis * speed, yAxis * speed);
    }
}
