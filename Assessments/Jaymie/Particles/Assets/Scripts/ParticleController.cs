using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Range(0, 4)]
    public int selectedParticle = 0;
    public int tempIndex;
    public bool particleSpawned = false;
    public GameObject[] particles;
    public GameObject Stand;

    public void Start()
    {
        tempIndex = selectedParticle;
        Stand = GameObject.Find("spawnPos");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedParticle++;
            if (selectedParticle > 5)
            {
                selectedParticle = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedParticle--;
            if (selectedParticle < 0)
            {
                selectedParticle = 5;
            }
        }

        if (selectedParticle != tempIndex)
        {
            ParticleReset();
        }

        if (!particleSpawned)
        {
            particleSwitch();
        }
    }

    public void ParticleReset()
    {
        tempIndex = selectedParticle;
        particleSpawned = false;

    }

    public void particleSwitch()
    {
        Destroy(GameObject.FindGameObjectWithTag("Particle"));
        Instantiate(particles[selectedParticle], Stand.transform);
        particleSpawned = true;
    }
}
