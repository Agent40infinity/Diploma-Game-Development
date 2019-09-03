using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D mapTexture;
    public PixelToObject[] pixelColorMappings;
    private Color pixelColor;

    public void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < mapTexture.width; i++)
        {
            for (int j = 0; j < mapTexture.height; j++)
            {
                GenerateObject(i, j);
            }
        }
    }

    public void GenerateObject(int x, int y)
    {
        pixelColor = mapTexture.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            Debug.Log("Mhm");
            return;            
        }

        foreach (PixelToObject pixelColorMapping in pixelColorMappings)
        {
            if (pixelColorMapping.pixelColor.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(pixelColorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
