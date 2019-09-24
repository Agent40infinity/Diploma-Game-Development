﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region Variables
    //Movement:
    public Vector3 moveDirection; 
    public float jumpSpeed = 8;
    public float speed = 5;
    public float sprintSpeed = 10;
    public float gravity = 20;

    public static bool canMove;
    #endregion

    //MouseMovement: 
    [Range(0, 20)]
    public float sensX = 15;
    [Range(0, 20)]
    public float sensY = 15;

    public float minY = -85;
    public float maxY = 85;
    public float rotationY = 0;

    //Shooting:
    public float bulletSpeed = 100f;
    public float fireCooldown;
    public float fireSpeed = 0.1f;
    public bool canFire;

    //References:
    private CharacterController controller;
    private Rigidbody rigid;
    public GameObject camera;

    #region General
    public void Start()
    {
        canMove = true;
        controller = gameObject.GetComponent<CharacterController>();
        rigid = gameObject.GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void Update()
    {
        Movement();
        MouseMovement();
        Shooting();
    }

    public void Movement()
    {
        if (canMove) //canMove check
        {
            if (controller.isGrounded) //isGrounded check
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); 
                moveDirection = transform.TransformDirection(moveDirection); 
                if (Input.GetKey(KeyCode.LeftShift)) //Checks if the player is sprinting and applies extra force.
                {
                    moveDirection *= sprintSpeed;
                }
                else
                {
                    moveDirection *= speed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
    #endregion

    #region MouseMovement
    public void MouseMovement()
    {
        if (canMove)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensX, 0);
            rotationY += Input.GetAxis("Mouse Y") * sensY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            camera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
    }
    #endregion

    #region Shooting
    public void Shooting()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullet");
            Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
            bulletRigid.AddForce(Vector3.forward * speed, ForceMode.Impulse);
            Debug.Log("Bullet: " + bulletRigid + " force: " + bulletRigid.velocity);
            fireCooldown = fireSpeed;
        }

        if (fireCooldown <= 0)
        {
            canFire = true;
            fireCooldown = 0;
        }
        else
        {
            canFire = false;
            fireCooldown -= Time.deltaTime;
        }
    }
    #endregion
}