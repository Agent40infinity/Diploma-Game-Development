using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Wall;
    public int tileWidth = 32;
    public int tileHeight = 18;
    public int[,] tilesetPreset1 =
        {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };
    public int[,] tilesetPreset2 =
        {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };
    public int[,] tilesetPreset3 =
        {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,1,1,1,1,0,0,0,0,1,1,1,1,0,1},
        {1,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1},
        {1,0,1,1,1,1,0,0,0,0,1,1,1,1,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };

    public void Start()
    {
        Wall = Resources.Load<GameObject>("Prefabs/Wall");
        Tilesets();
    }

    public void Tilesets()
    {
        for (int y = 0; y < tileHeight; y++) //Checks the  Y position and value within the array to set it's content.
        {
            for (int x = 0; x < tileWidth; x++) //Checks the  X position and value within the array to set it's content.
            {  
                switch (tilesetPreset1[y, x]) //Generation for Tileset PReset 1
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(Wall, new Vector3(x, y, 0), transform.rotation);
                        break;
                }
                switch (tilesetPreset2[y, x]) //Generation for Tileset PReset 2
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(Wall, new Vector3(x, y, 0), transform.rotation);
                        break;
                }
                switch (tilesetPreset3[y, x]) //Generation for Tileset Preset 3
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(Wall, new Vector3(x, y, 0), transform.rotation);
                        break;
                }
            }
        }
    }
}
