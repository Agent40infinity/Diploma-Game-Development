using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    //General:
    public float xAxis; //Used to log the Input for the xAxis (Horizonal Input).
    public float yAxis; //Used to log the Input for the yAxis (Vertical Input).
    public float speed = 5f; //Determines the speed at which the player can move.
    public bool hasShot = false; //Checks whether or not the player has Shot.
    public Vector3 mousePosition;
    public Vector2 direction;


    //Timers:
    public float shootTimer = 0.5f; //Cooldown timer for shooting.
    public float shootTReset = 0.5f; //value used to reset the shooting cooldown.


    //References:
    public Rigidbody2D rigid; //References the RigidBody.
    public GameObject bullet; //References the Prefab for the bullet GameObject.
    #endregion

    #region General
    public void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>(); //Used to obtain the attached GameObject's RigidBody.
        shootTimer = shootTReset; //Gateway reset to make sure everything is correct.
    }

    public void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal"); //Sets the Input value for the xAxis.
        yAxis = Input.GetAxisRaw("Vertical"); //Sets the Input value for the yAxis.

        Shoot(); //Calls upon the sub-routine "Shoot".
        Facing();
    }

    public void FixedUpdate()
    {
        rigid.velocity = new Vector2(xAxis * speed, yAxis * speed); //Automatic Input for the Player's movement.
    }
    #endregion

    #region Look Towards
    public void Facing()
    {
        mousePosition = Input.mousePosition; //Saves the mousePosition as a variable.
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //Changes the mousePosition to consider Screen to Worldpoint.
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); 
        transform.up = direction;
    }
    #endregion

    #region Shooting
    public void Shoot() //Used to allow the player to shoot.
    {
        if (Input.GetMouseButton(0) && shootTimer <= 0) //Checks whether or not the player is pressing the attack button and if the attack is off cooldown.
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
