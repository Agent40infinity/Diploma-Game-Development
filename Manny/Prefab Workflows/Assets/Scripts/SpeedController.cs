using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public MoveChildren moveChildren;
    public FurnitureSpawner furnitureSpawner;
    public List<ScrollTexture> scrollTextures = new List<ScrollTexture>();

    public void IncreaseSpeed()
    {
        moveChildren.movementPerSecond *= 2f;
        furnitureSpawner.timeBetweenSpawns *= 0.5f;
        foreach (ScrollTexture scrollTexture in scrollTextures)
        {
            scrollTexture.scrollSpeed *= 2f;
        }
    }

    public void DecreaseSpeed()
    {
        moveChildren.movementPerSecond *= 0.5f;
        furnitureSpawner.timeBetweenSpawns *= 2f;
        foreach (ScrollTexture scrollTexture in scrollTextures)
        {
            scrollTexture.scrollSpeed *= 0.5f;
        }
    }
}
