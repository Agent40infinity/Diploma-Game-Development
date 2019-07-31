using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float movementSpeed = 10f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 2f;
    public bool isGrounded = false;
    private Rigidbody rigid;

    private void OnCollisionEnter(Collision _col)
    {
        isGrounded = true;
    }

    public void Start()
    {
        rigid = GetComponent<Rigidbody>();

        AudioListener listener = GetComponent<AudioListener>();
        Camera camera = GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            camera.enabled = true;
            listener.enabled = true;
        }
        else
        {
            camera.enabled = false;
            listener.enabled = false;
        }
    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        KeyCode[] keys =
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Space
        };

        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }
        }
    }

    public void Move(KeyCode _key)
    {
        Vector3 position = rigid.position;
        Quaternion rotation = rigid.rotation;
        switch (_key)
        {
            case KeyCode.W:
                position += transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.S:
                position += -transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                rotation *= Quaternion.AngleAxis(-rotationSpeed, Vector3.up);
                break;
            case KeyCode.D:
                rotation *= Quaternion.AngleAxis(rotationSpeed, Vector3.up);
                break;
            case KeyCode.Space:
                if (isGrounded)
                {
                    rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    isGrounded = false;
                }
                break;
        }
        rigid.MovePosition(position);
        rigid.MoveRotation(rotation);
    }
}
