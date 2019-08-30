    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        public float speed = 15f;

        public Rigidbody2D rigid;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Witch" || other.tag == "Zombie" || other.tag == "Shield Zombie")
            {
                other.GetComponent<EnemyHealth>().TakenDamage();
                Destroy(gameObject);
            }
            else if (other.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
        public void Start()
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            rigid.velocity = transform.up * speed;
        }
    }
