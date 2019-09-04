using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameAction[] actions;

    private void Update()
    {
        if (Application.isEditor)
        {
            foreach (var action in actions)
            {
                action.Update();
            }
        }
    }

    private void OnEnable()
    {
        foreach (var action in actions)
        {
            action.RegisterListener();
        }
    }

    private void OnDisable() 
    {
        foreach (var action in actions)
        {
            action.UnRegisterListener();
        }
    }
}