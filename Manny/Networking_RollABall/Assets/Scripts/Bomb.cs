using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bomb : NetworkBehaviour
{
    public float explosionRadius = 2f;
    public float explosionDelay = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void Start()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);
        CmdExplode(transform.position, explosionRadius);
    }

    [Command]
    void CmdExplode(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            NetworkServer.Destroy(hit.gameObject);
        }
    }
}
