using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    //General:
    [Header("General")]
    public Material[] effectMaterial;
    [Range(0, 3)]
    public int selectedIndex;

    //Pixelate:
    [Header("Pixelate")]
    public float pixelColumn = 64;
    public float pixelRow = 64;

    //BlackAndWhite:
    [Header("Black and White")]
    [Range(0, 1)]
    public float intensity;

    //CRT:
    [Header("CRT")]
    public float strength;
    [Range(0, 1)]
    public float maskSize;
    [Range(0, 1)]
    public float maskBlend;

    public void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        switch (selectedIndex)
        {
            case 0:
                effectMaterial[selectedIndex].SetFloat("_BWBlend", 0);
                Graphics.Blit(src, dst, effectMaterial[selectedIndex]);
                break;
            case 1:
                effectMaterial[selectedIndex].SetFloat("_Columns", pixelColumn);
                effectMaterial[selectedIndex].SetFloat("_Rows", pixelRow);
                Graphics.Blit(src, dst, effectMaterial[selectedIndex]);
                break;
            case 2:
                effectMaterial[selectedIndex].SetFloat("_BWBlend", intensity);
                Graphics.Blit(src, dst, effectMaterial[selectedIndex]);
                break;
            case 3:
                effectMaterial[selectedIndex].SetFloat("_Strength", strength);
                effectMaterial[selectedIndex].SetFloat("_MaskSize", maskSize);
                effectMaterial[selectedIndex].SetFloat("_MaskBlend", maskBlend);
                Graphics.Blit(src, dst, effectMaterial[selectedIndex]);
                break;
        }
    }
}
