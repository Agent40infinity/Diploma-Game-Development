using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLook : NetworkBehaviour
{
    public float mouseSensitivity = 2.0f;
    public float minimumY = -90f;
    public float maximumY = 90f;

    private float yaw = 0f;
    private float pitch = 0f;
    private GameObject mainCamera;

    public void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }

    public void LateUpdate()
    {
        if (isLocalPlayer)
        {
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }

    public void HandleInput()
    {

    }
}
