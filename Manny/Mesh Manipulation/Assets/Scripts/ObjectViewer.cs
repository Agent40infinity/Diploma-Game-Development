using UnityEngine;
using System.Collections;

public class ObjectViewer : MonoBehaviour
{
    public Camera mainCam;
    public Transform target;
    public bool isReadyForTransform = false;
    // rotate
    public float distance = 10f;
    public float xspeed = 250f;
    public float yspeed = 120f;
    public float xsign = 1f;
    private float x;
    private float y;
    Vector3 prevPos = new Vector3();

    // Use this for initialization
    public void Init()
    {
        // get distance
        distance = Vector3.Distance(mainCam.transform.position, target.transform.position);
        isReadyForTransform = true;
        Input.simulateMouseWithTouches = true;
    }

    void LateUpdate()
    {
        if (!isReadyForTransform)
        {
            return;
        }

        // Rotation
        Vector3 forward = mainCam.transform.TransformDirection(Vector3.up); // camera's transform
        Vector3 forward2 = target.transform.TransformDirection(Vector3.up); // target's transform

        if (Vector3.Dot(forward, forward2) < 0)
        {
            xsign = -1;
        }
        else
        {
            xsign = 1;
        }

        if (Input.GetMouseButton(0))
        {
            if (prevPos != Vector3.zero && Input.mousePosition != prevPos)
            {
                x += xsign * (Input.mousePosition.x - prevPos.x) * xspeed * 0.02f;
                y -= (Input.mousePosition.y - prevPos.y) * yspeed * 0.02f;
                DoRotation(x, y);
            }
            prevPos = Input.mousePosition;
        }
        else
        {
            prevPos = Vector3.zero;
        }
    }

    void DoRotation(float x, float y)
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = (rotation * new Vector3(0.0f, 0.0f, -distance)) + target.transform.position;
        mainCam.transform.rotation = rotation;
        mainCam.transform.position = position;
    }
}










