using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region variables
    public int health;
    public int pointsAwarded;
    public bool isDead;

    public GameObject enemy;
    #endregion

    #region General
    public void Start()
    {
        enemy = gameObject;

        if (enemy.tag == "Witch")
        {
            health = 2;
            pointsAwarded = 300;
        }
        else if (enemy.tag == "Zombie")
        {
            health = 1;
            pointsAwarded = 50;
        }
        else if (enemy.tag == "Shield Zombie")
        {
            health = 4;
            pointsAwarded = 200;
        }
    }
    #endregion

    #region Health Management
    public void TakenDamage()
    {
        health--;
        if (health <= 0)
        {
            StartCoroutine("Death");
        }
    }

    public IEnumerator Death()
    {
        Destroy(enemy);
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().addScore(pointsAwarded);
        yield return new WaitForEndOfFrame();
    }
    #endregion
}
