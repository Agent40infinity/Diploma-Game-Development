using UnityEngine;
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
    public float gravity = 20; //default return value for gravity.
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

    //MouseMovement: 
    [Range(0, 20)]
    public float sensX = 15; //Sensitivity for X axis of mouse.
    [Range(0, 20)]
    public float sensY = 15; //Sensitivity for Y axis of mouse.

    public float minY = -65; //Max value for the rotation on the Y axis for the camera.
    public float maxY = 65; //Min value for the rotation on the Y axis for the camera.
    public float rotationY = 0; //value to store the Y axis for the rotation.

    //References:
    private CharacterController controller; //Reference for the attached character controller.
    public GameObject camera; //Reference for the attached camera.
    public LayerMask ground; //Reference for the LayerMask that stores the value for ground.
    public Vector3 spawnPoint = new Vector3(0, 1, 0); //Transform used to store the spawn point.

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
        camera = gameObject.GetComponentInChildren<Camera>();
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
            moveDirection.z = Input.GetAxis("Vertical"); 
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection = transform.TransformDirection(moveDirection);
            if (isGrounded()) //Checks if the player is Grounded and applies all Y axis based movement.
            {
                moveDirection.y = 0;
                gravityIncreaseTimer = 0;
                appliedGravity = gravity;
                if (Input.GetButtonDown("Jump") && canIncrease == true && sprintSpeed <= 35) //Checks if the player can Increase their sprintSpeed and is attempting to jump to apply extra velocity.
                {
                    moveDirection.y += jumpSpeed;
                    increaseTimer = 0;
                    sprintSpeed *= 1.2f;
                    canIncrease = true;
                }
                else if (Input.GetButtonDown("Jump")) //Defaults back to default jump.
                {
                    moveDirection.y += jumpSpeed;
                    increaseTimer = 0;
                    canIncrease = true;
                }
            }
            else //If the player isn't grounded, change values for gravity overtime.
            {
                gravityIncreaseTimer += Time.deltaTime;
                if (gravityIncreaseTimer >= 0.4f && appliedGravity <= 50)
                {
                    appliedGravity = gravity + (gravityIncreaseTimer * 16f);
                    //Debug.Log(appliedGravity);
                }
            }
            if (Input.GetKey(KeyCode.LeftShift)) //Checks if the player is sprinting and applies extra force.
            {
                moveDirection.x *= sprintSpeed;
                moveDirection.z *= sprintSpeed;
            }
            else //Applies default speed if not sprinting.
            {
                moveDirection.x *= speed;
                moveDirection.z *= speed;
            }

            moveDirection.y -= appliedGravity * Time.deltaTime; //Applies gravity.
            controller.Move(moveDirection * Time.deltaTime); //Applies movement.
        }

        if (canIncrease == true && increaseTimer >= 0 && isGrounded()) //Used to start the timer that disables the ability to increase sprintSpeed.
        {
            increaseTimer += Time.deltaTime;
            if (increaseTimer >= 0.1f) //If the increase timer is over a certain amount, disables the ability to increase sprintSpeed.
            {
                sprintSpeed = 15;
                increaseTimer = 0;
                canIncrease = false;
            }
        }
    }
    #endregion

    #region MouseMovement
    public void MouseMovement() //Controls all mouse movement.
    {
        if (freezeCamera == false) //Checks if the camera has been frozen.
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensX, 0); //Records and applies movement for X axis.
            rotationY += Input.GetAxis("Mouse Y") * sensY; //Records movement for Y axis.
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
            Debug.Log("Yeet");
            Debug.Log(transform.position);
            gameObject.transform.position = spawnPoint;
            Debug.Log("Attempted");
            Debug.Log(transform.position);
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

    #region Gizmos
    private void OnDrawGizmos() //Displays the Gizmos for isGrounded.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.localPosition + groundDistance, groundedOverlay);
    }
    #endregion
}