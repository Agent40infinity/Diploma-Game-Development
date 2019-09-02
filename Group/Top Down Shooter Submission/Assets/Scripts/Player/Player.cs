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
    public int health = 4;
    public bool beenDamaged = false;

    //Timers:
    public float shootTimer = 0.5f; //Cooldown timer for shooting.
    public float shootTReset = 0.5f; //value used to reset the shooting cooldown.


    //References:
    public Rigidbody2D rigid; //References the RigidBody.
    public GameObject bullet; //References the Prefab for the bullet GameObject.
    public Joystick joystickMovement; //References the joystick for Movement.
    public Joystick joystickAttack; //References the joystick for Attack.

    public bool joystickNotZero()
    {
        if (joystickAttack.Horizontal != 0f || joystickAttack.Vertical != 0f) //Checks whether or not the joystick has been let go of.
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Trigger
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Witch" || other.tag == "Zombie" || other.tag == "Shield Zombie")
        {
            if (beenDamaged == false)
                Health();
        }
    }
    #endregion

    #region General
    public void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>(); //Used to obtain the attached GameObject's RigidBody.
        joystickMovement = GameObject.FindGameObjectWithTag("Movement").GetComponent<Joystick>(); //Used to obtain the joystick based on the tag "Movement".
        joystickAttack = GameObject.FindGameObjectWithTag("Attack").GetComponent<Joystick>(); //Used to obtain the joystick based on the tag "Attack".
        shootTimer = shootTReset; //Gateway reset to make sure everything is correct.

    }

    public void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal"); //Sets the Input value for the xAxis.
        yAxis = Input.GetAxisRaw("Vertical"); //Sets the Input value for the yAxis.

        Shoot(); //Calls upon the sub-routine "Shoot".
        Facing(); //Calls upon the sub-routine "Facing".
    }

    public void FixedUpdate()
    {

        rigid.velocity = new Vector2(xAxis, yAxis) * speed;
        //rigid.velocity = new Vector2(joystickMovement.Horizontal, joystickMovement.Vertical) * speed; //Automatic Input for the Player's movement.
    }
    #endregion

    #region Look Towards
    public void Facing()
    {
        //mousePosition = Input.mousePosition; //Saves the mousePosition as a variable.
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //Changes the mousePosition to consider Screen to Worldpoint.
        if (joystickNotZero() == true) //Calls upon the variable.
        {
            direction = new Vector2(joystickAttack.Horizontal, joystickAttack.Vertical); //Sets the direction the player is facing if the joystick is being moved.
            transform.up = direction;
        }
    }
    #endregion

    #region Shooting
    public void Shoot() //Used to allow the player to shoot.
    {
        if (joystickNotZero() && shootTimer <= 0) //Checks whether or not the player is pressing the attack button and if the attack is off cooldown.
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

    #region Health Management
    public void Health()
    {

        if (health <= 0)
        {
            StartCoroutine("Death");
        }
        else
        {
            health--;
            StartCoroutine("TakenDamage");
            Debug.Log("health: " + health);
        }
    }

    public IEnumerator TakenDamage()
    {
        beenDamaged = true;
        yield return new WaitForSeconds(1.5f);
        beenDamaged = false;
        Debug.Log("End Taken Damage");
    }

    public IEnumerator Death()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    #endregion
}
