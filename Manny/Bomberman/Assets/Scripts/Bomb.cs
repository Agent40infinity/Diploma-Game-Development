using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool exploded = false;
    private bool canBePushed = true;
    private float pushSpeed = 50f;
    public bool playerHasPushed = false;
    public Vector3 playerPos;

    public Vector3 bombPos;
    public Rigidbody rigid;
    public GameObject explosionPrefab;
    public LayerMask levelMask;

    public void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    public void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        bombPos = gameObject.GetComponent<Transform>().position;
        Invoke("Explode", 3f);
    }

    public void Update()
    {
        if (canBePushed && playerHasPushed)
        {
            if (playerPos.x < bombPos.x && (int)playerPos.z == (int)bombPos.z)
            {
                Debug.Log("player" + playerPos.z + "bomb" + bombPos.z);
                rigid.velocity = new Vector3(-pushSpeed, 0, 0);
            }
            else if (playerPos.z < bombPos.z && (int)playerPos.x == (int)bombPos.x)
            {
                rigid.velocity = new Vector3(0, 0, -pushSpeed);
            }
            else if (playerPos.x > bombPos.x && (int)playerPos.z == (int)bombPos.z)
            {
                rigid.velocity = new Vector3(pushSpeed, 0, 0);
            }
            else if (playerPos.z > bombPos.z && (int)playerPos.x == (int)bombPos.x)
            {
                rigid.velocity = new Vector3(0, 0, pushSpeed);
            }
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false);
        Destroy(gameObject, .3f);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }
}
