using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rigid;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //if (TouchInput.beenTouched == true)
        //{
        //    rigid.velocity = new Vector2(rigid.position.x, speed);
        //    TouchInput.beenTouched = false;
        //}
    }
}
