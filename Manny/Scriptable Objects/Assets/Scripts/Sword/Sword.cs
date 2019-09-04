using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    [SerializeField] private SwordData swordData;

    [SerializeField] public UnityEvent onMouseDown;

    private void OnMouseDown()
    {
        onMouseDown.Invoke();
    }

}
