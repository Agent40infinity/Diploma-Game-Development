using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Vector2 scrollSpeed;
    private Renderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;
        myRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
