using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Camera/Drawing Paper")]
[RequireComponent(typeof(Camera))]
public class DrawingPaper : MonoBehaviour
{
    #region Variables
    public Shader shader;
    private float timeX = 1;
    public Color pencilColor = new Color(0, 0, 0, 0);
    [Range(0, 2)]
    public float pencilSize = 0.00125f;
    [Range(0, 2)]
    public float pencilCorrection = 0.35f;
    [Range(0, 1)]
    public float intensity = 1;
    [Range(0, 2)]
    public float animationSpeed = 1;
    [Range(0, 1)]
    public float cornerLoss = 1;
    [Range(0, 1)]
    public float paperFadeIn = 0;
    [Range(0, 1)]
    public float paperFadeColor = 1;
    public Color backColor = new Color(1, 1, 1, 1);
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
                paper.hideFlags = HideFlags.HideAndDontSave;
            }
            return paperMaterial;
        }
    }
    #endregion

    #region General
    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    public void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (shader != null)
        {
            timeX += Time.deltaTime;
            if (timeX > 100)
            {
                timeX = 0;
                material.SetFloat("_TimeX", timeX);
                material.SetColor("_PColor", pencilColor);
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
    }

    public void OnDisable()
    {
        if (paperMaterial)
        {
            DestroyImmediate(paperMaterial);
        }
    }
    #endregion
}
