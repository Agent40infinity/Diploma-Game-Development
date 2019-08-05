using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rigid;
    public Joystick joystickMovement;
    public Joystick joystickAttack;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        joystickMovement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Joystick>();
        joystickAttack = GameObject.FindGameObjectWithTag("Attack").GetComponent<Joystick>();
    }

    public void Update()
    {
        rigid.velocity = new Vector2(joystickMovement.Horizontal, joystickMovement.Vertical )*speed;
        rigid.transform.Rotate(new Vector3(0,0,joystickAttack.Horizontal),Space.World);
    }
}
