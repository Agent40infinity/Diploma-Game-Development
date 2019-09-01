using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootBullet : NetworkBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    void Update()
    {
        if (this.isLocalPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            this.CmdShoot(transform.position);
        }
    }

    [ClientRpc]
    void RpcClientShot(Vector3 position)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * bulletSpeed;
        Destroy(bullet, 1f);
    }

    [Command]
    void CmdShoot(Vector3 position)
    {
        RpcClientShot(position);
    }
}
