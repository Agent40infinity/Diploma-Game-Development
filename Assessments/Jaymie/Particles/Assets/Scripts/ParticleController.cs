using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Range(0, 9)]
    public int selectedParticle = 0;
    public int tempIndex;
    public bool particleSpawned = false;
    public GameObject[] particles;
    public GameObject Stand;

    public int zoomIndex = 1;
    public Transform[] zoom;
    public float zoomSpeed = 5;

    public void Start()
    {
        tempIndex = selectedParticle;
        Stand = GameObject.Find("spawnPos");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedParticle++;
            if (selectedParticle > particles.Length - 1)
            {
                selectedParticle = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedParticle--;
            if (selectedParticle < 0)
            {
                selectedParticle = particles.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            zoomIndex++;
            if (zoomIndex > zoom.Length - 1)
            {
                zoomIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            zoomIndex--;
            if (zoomIndex < 0)
            {
                zoomIndex = zoom.Length - 1;
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

        Zoom();
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

    public void Zoom()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, zoom[zoomIndex].position, zoomSpeed * Time.deltaTime);
    }
}
