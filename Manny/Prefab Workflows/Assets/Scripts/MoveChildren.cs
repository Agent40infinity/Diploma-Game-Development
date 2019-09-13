using UnityEngine;

public class MoveChildren : MonoBehaviour
{
    public Vector3 movementPerSecond;

    void Update()
    {
        foreach (Transform child in transform)
        {
            child.Translate(movementPerSecond * Time.deltaTime, Space.World);
        }
    }
}
