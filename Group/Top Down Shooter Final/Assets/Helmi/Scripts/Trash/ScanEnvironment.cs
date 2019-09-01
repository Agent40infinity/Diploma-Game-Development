using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanEnvironment : MonoBehaviour
{
    public AstarPath path;
    public MainMenu main;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime < 1)
        {
            path.Scan();
        }
    }
}
