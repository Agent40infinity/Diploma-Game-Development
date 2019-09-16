using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
[AddComponentMenu("Camera/Drawing Paper")]
[RequireComponent(typeof(Camera))]
public class PaperShader : MonoBehaviour
{
    #region Variables
    public Shader shader;
    private float timeX = 1.0f;
    public Color pencilColour = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    [Range(0.0001f, 0.0022f)]
    public float pencilSize = 0.00125f;
    [Range(0, 2)]
    public float pencilCorrection = 0.35f;
    [Range(0, 1)]
    public float intensity = 1.0f;
    [Range(0, 2)]
    public float animationSpeed = 1f;
    [Range(0, 1)]
    public float cornerLoss = 1f;
    [Range(0, 1)]
    public float paperFadeIn = 0f;
    [Range(0, 1)]
    public float paperFadeColor = 1f;
    public Color backColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Material paperMaterial;
    public Texture2D paper;
    #endregion
    #region Properties
    Material material
    {
        get
        {
            if (paperMaterial == null)
            {
                paperMaterial = new Material(shader);
                paperMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return paperMaterial;
        }
    }
    #endregion
    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (shader != null)
        {
            timeX += Time.deltaTime;
            if (timeX > 100) timeX = 0;
            material.SetFloat("_TimeX", timeX);
            material.SetColor("_PColor", pencilColour);
            material.SetFloat("_Value1", pencilSize);
            material.SetFloat("_Value2", pencilCorrection);
            material.SetFloat("_Value3", intensity);
            material.SetFloat("_Value4", animationSpeed);
            material.SetFloat("_Value5", cornerLoss);
            material.SetFloat("_Value6", paperFadeIn);
            material.SetFloat("_Value7", paperFadeColor);
            material.SetColor("_PColor2", backColor);
            material.SetTexture("_MainTex2", paper);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    } 
    void OnDisable()
    {
        if (paperMaterial)
        {
            DestroyImmediate(paperMaterial);
        }
    }
}