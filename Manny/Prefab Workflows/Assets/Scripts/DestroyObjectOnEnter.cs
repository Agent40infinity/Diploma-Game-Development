using UnityEngine;
using System.Collections;

public class DestroyObjectOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
