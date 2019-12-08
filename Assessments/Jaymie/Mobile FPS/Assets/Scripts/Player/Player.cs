﻿using UnityEngine;
using System.Collections;

/*--------------------------------------------------------------------------
 * Script Created by: Aiden Nathan.
 * Frankensteined camera movement from Jaymie.
 -------------------------------------------------------------------------*/

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    #region Variables
    //Movement:
    public Vector3 moveDirection; //Vector3 used to store the movement values.
    public float jumpSpeed = 12; //speed applied to jumping.
    public float speed = 10; //speed applied for default movement.
    public float sprintSpeed = 15; //speed applied when sprinting.
    public float gravity = 20; //default return va/ssv  lue for gravity.
    public float appliedGravity; //gravity used to affect the player.
    public float gravityIncreaseTimer; //Timer used to increase appliedGravity's effect over time.
    public int teleportPoint = -5; //Value used to check if the player is below a certain point so that they can be teleported.

    public float increaseTimer = 0; //Counter used to disable the players ability to increase the sprintSpeed after a certain time frame.
    public bool canIncrease = false; //Determines whether or not player is able to increase their sprintSpeed.
    public static bool canMove = true; //Gateway variable to lock all movement (Camera and Player).
    public static bool freezeCamera = false; //Gateway variable to freeze Camera movement.
    public static bool freezeMove = false; //Gateway variable to freeze all Player movement.

    public Vector3 groundedOverlay = new Vector3(0.2f, 0.1f, 0.2f); //Vector3 used to store the values for the collider used to check for isGrounded.
    public Vector3 groundDistance = new Vector3(0f, -1f, 0f); //Vector3 used to store the position of the isGrounded check.

    //Shooting:
    public bool canFire = true;
    public float fireCooldown = 0.15f;
    public float fireSpeed = 0.15f;

    //MouseMovement: 
    [Range(0, 20)]
    public float sensX = 15; //Sensitivity for X axis of mouse.
    [Range(0, 20)]
    public float sensY = 15; //Sensitivity for Y axis of mouse.

    public float minY = -65; //Max value for the rotation on the Y axis for the camera.
    public float maxY = 65; //Min value for the rotation on the Y axis for the camera.
    public float rotationY = 0; //value to store the Y axis for the rotation.

    //Health Management:
    public int curHealth; //Current health of the player.
    public int maxHealth = 100; //Max health of the player.
    public bool playerDead = false; //Checks whether or not the player is dead.
    public bool canTakeDamage = true; //Checks whether or not the player can be damaged.

    //References:
    private CharacterController controller; //Reference for the attached character controller.
    public GameObject camera; //Reference for the attached camera.
    public LayerMask ground; //Reference for the LayerMask that stores the value for ground.
    public Vector3 spawnPoint = new Vector3(0, 1, 0); //Transform used to store the spawn point.
    public Joystick joystickMovement;
    public Joystick joystickAttack;

    public bool isGrounded() //Used to determine if the player is grounded based on a collider check.
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.localPosition + groundDistance, groundedOverlay, Quaternion.identity, ground); //Creates a overlap box to check whether the player is grounded.
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == 9) //Checks each gameObject that the collider hits to see if it is layered as "ground".
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region General
    public void Start() //Used to determine default values and grab references.
    {
        canMove = true;
        controller = gameObject.GetComponent<CharacterController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        joystickMovement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Joystick>();
        joystickAttack = GameObject.FindGameObjectWithTag("Attack").GetComponent<Joystick>();
    }

    public void Update() //Used to make reference to the sub-routines/methods.
    {
        Debug.Log(isGrounded());
        if (canMove)
        {
            Movement();
            MouseMovement();
        }

        BelowGround();
        Shooting();
    }
    #endregion

    #region Movement
    public void Movement() //Controls all player movement.
    {
        if (freezeMove == false) //Checks if the player movement is frozen.
        {
            moveDirection = new Vector3(joystickMovement.Horizontal, 0, joystickMovement.Vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            if (isGrounded()) //Checks if the player is Grounded and applies all Y axis based movement.
            {
                gravityIncreaseTimer = 0;
                appliedGravity = gravity;
            }
            else //If the player isn't grounded, change values for gravity overtime.
            {
                gravityIncreaseTimer += Time.deltaTime;
                if (gravityIncreaseTimer >= 0.4f && appliedGravity <= 50)
                {
                    appliedGravity = gravity + (gravityIncreaseTimer * 16f);

                }
            }

            moveDirection.x *= speed;
            moveDirection.z *= speed;
            moveDirection.y -= appliedGravity * Time.deltaTime; //Applies gravity.
            controller.Move(moveDirection * Time.deltaTime); //Applies movement.
        }
    }
    #endregion

    #region MouseMovement
    public void MouseMovement() //Controls all mouse movement.
    {
        if (freezeCamera == false) //Checks if the camera has been frozen.
        {
            transform.Rotate(0, joystickAttack.Horizontal * sensX, 0); //Records and applies movement for X axis.
            rotationY += joystickAttack.Vertical * sensY; //Records movement for Y axis.
            rotationY = Mathf.Clamp(rotationY, minY, maxY); //Applies a Clamp to give boundaries to the movement on the Y axis.
            camera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0); //applies movement for the Y axis.
        }
    }
    #endregion

    #region BelowGround
    public void BelowGround() //Used to teleport the player once they are below the ground/platforms.
    {
        if (gameObject.transform.position.y <= teleportPoint) //Checks if the player's y level is below the teleport positon's value.
        {
            gameObject.transform.position = spawnPoint;
        }
    }
    #endregion

    #region Shooting
    public void Shooting()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullet");
            Instantiate(bullet, transform.position, bullet.transform.rotation);   
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

    #region Health Management
    public void TakeDamage(int damageDealt) //Function that can be called upon to deal damage to the player.
    {
        if (canTakeDamage) //Checks whether or not the player has an iFrame and takes away damage if they don't.
        {
            curHealth -= damageDealt;
            if (curHealth <= 0) //Checks whether or not the player has 0 health to allow the player to go down.
            {
                playerDead = true;
                canTakeDamage = false;
            }
            StartCoroutine(iFrame());
        }
    }

    public IEnumerator iFrame() //Co-routine that's used as a timer to check whether or not the player can take damage.
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.3f);
        canTakeDamage = true;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos() //Displays the Gizmos for isGrounded.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.localPosition + groundDistance, groundedOverlay);
    }
    #endregion
}