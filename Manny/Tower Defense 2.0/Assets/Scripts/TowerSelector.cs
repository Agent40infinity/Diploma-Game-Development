using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public int towerMax = 2;
    public int towerIndex = 1;

    public void SpawnTower(Vector3 position)
    {
        GameObject towerPrefab = Resources.Load<GameObject>("Prefabs/Towers/Tower " + towerIndex);
        Instantiate(towerPrefab, position, Quaternion.identity);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            towerIndex++;
        }
        if (towerIndex > towerMax)
        {
            towerIndex = 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit))
            {
                Placeable p = hit.collider.GetComponent<Placeable>();
                if (p && p.isOccupied == false)
                {
                    SpawnTower(hit.transform.position);
                    p.isOccupied = true;
                }
            }
        }


    }
}
