using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldZombieAI : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer enemyGFX;
    public float speed = 3;

    private Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        enemyGFX = GetComponent<SpriteRenderer>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);

        if (target.position.x < transform.position.x)
        {
            enemyGFX.flipX = true;
        }

        if (target.position.x > transform.position.x)
        {
            enemyGFX.flipX = false;
        }
    }
}
