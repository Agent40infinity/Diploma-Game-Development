using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*--------------------------------------------------------------------------
 * Script Created by: Aiden Nathan.
 *------------------------------------------------------------------------*/

public class Enemy : MonoBehaviour
{
    #region Variables
    //General:
    public float attackDistance = 2f; //Attack distance for the enemy.
    public float attackRange = 4f;  //Attack range of the raycast for the enemy.
    public int damage = 20; //Damage the enemy can deal towards the player.
    public float detectionRadius = 5f; //DetectionRadius to allow for the target to change.
    public float noiseTimer = 0f;
    public int health = 75; //Health of the enemy.

    //References:
    public Player player; //Reference to the current targetted player.
    public NavMeshAgent nav; //Reference to the enemies navMesh.
    #endregion

    #region General
    public void Start() //Sets up the references and finds the first target of the enemy.
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        GetClosestPlayer();
    }

    public void Update()
    {
        AI();
    }
    #endregion

    #region Get Closest Player
    public Player GetClosestPlayer() //Gets the closest player to the enemy.
    {
        Player result = null;
        float minDistance = float.PositiveInfinity;
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, detectionRadius);

        foreach (var hit in hits) //Runs through each object within range and checks whether or not it is a player. If it is a player determines which player to target based on the distance.
        {
            if (hit.tag == "Player")
            {
                Vector3 playerPosition = hit.transform.position;
                Vector3 enemyPosition = transform.position;
                float distance = Vector3.Distance(playerPosition, enemyPosition);
                if (distance < minDistance)
                {
                    result = hit.GetComponent<Player>();
                    minDistance = distance;
                }
            }
        }
        return result;
    }
    #endregion

    #region AI
    public void AI() //Used to track and follow the player
    {
        if (player != null) //If no player exists in the world, then do nothing.
        {
            float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (distance > attackDistance) //If the enemy is outside of attacking range, follow the player
            {
                nav.enabled = true;
                nav.SetDestination(player.transform.position);
            }
            else //Else attack the player
            {
                nav.enabled = false;
                StartCoroutine(Attack());
            }
        }
    }
    #endregion

    #region Attack
    public IEnumerator Attack() //Used to attack the player
    {
        yield return new WaitForSeconds(0.5f);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red, 5f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange)) //Shoots out a raycast to check whether or not a player is within range
        {
            if (hit.collider.tag == "Player") //If the raycast hits the player, deal damage to said player
            {
                Player playerHitRef = hit.collider.gameObject.GetComponent<Player>();
                playerHitRef.TakeDamage(damage);
            }
        }
    }
    #endregion

    #region Health Management
    public void TakeDamage(int damage) //Deals damage to the enemy and adds the bonuses to the player
    {
        health -= damage;
        if (health <= 0) //Checks whether or not the enemy can die and gives bonuses if it did die.
        {
            Death();
        }
    }

    public void Death() //Destroys the enemy from the world and counts up the players kills
    {
        Destroy(this.gameObject);
    }
    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
