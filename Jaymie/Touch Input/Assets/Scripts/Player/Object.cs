using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    #region Variables
    public float speed = 5f;
    public float shootTimer;
    public float shootTReset = 0.15f;
    public bool hasShot = false;  
    public Vector2 direction;
    public Rigidbody2D rigid;
    public Joystick joystickMovement;
    public Joystick joystickAttack;
    public GameObject bullet;
    #endregion 

    #region General
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        joystickMovement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Joystick>();
        joystickAttack = GameObject.FindGameObjectWithTag("Attack").GetComponent<Joystick>();
        //bullet = GameObject.FindWithTag("Bullet");
    }

    public void Update()
    {
        Movement();
        Attack();
    }
    #endregion

    #region Movement
    public void Movement()
    {
        rigid.velocity = new Vector2(joystickMovement.Horizontal, joystickMovement.Vertical) * speed;
    }
    #endregion

    #region Attack
    public void Attack()
    {
        direction = new Vector2(joystickAttack.Horizontal, joystickAttack.Vertical);
        transform.up = direction;
        //rigid.transform.Rotate(new Vector3(0, 0, joystickAttack.Horizontal), Space.World);

        if ((joystickAttack.Horizontal != 0f || joystickAttack.Vertical != 0f) && shootTimer <= 0) //Checks whether or not the player is pressing the attack button and if the attack is off cooldown.
        {
            shootTimer = shootTReset;
            hasShot = true;
        }
        else //Else, counts down.
        {
            shootTimer -= Time.deltaTime;
        }

        if (hasShot == true) //Creates a bullet is hasShot has been activated and then ends the function.
        {
            Instantiate(bullet, transform.position, transform.rotation);
            hasShot = false;
        }
    }
    #endregion
}
