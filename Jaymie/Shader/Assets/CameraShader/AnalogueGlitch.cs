using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Analogue")]
public class AnalogueGlitch : MonoBehaviour
{
    #region Public Properties
    //Scan Line Jitter:
    [SerializeField, Range(0, 1)]
    float _scanLineJitter;
    public float _ScanLineJitter
    {
        get { return _scanLineJitter; }
        set { _scanLineJitter = value; }
    }

    //Vertical Jump:
    [SerializeField, Range(0, 1)]
    float _verticalJump;
    public float _VerticalJump
    {
        get { return _verticalJump; }
        set { _verticalJump = value; }
    }

    //Horizontal Shake:
    [SerializeField, Range(0, 1)]
    float _horizontalShake;
    public float _HorizontalShake
    {
        get { return _horizontalShake; }
        set { _horizontalShake = value; }
    }

    //Colour Drift:
    [SerializeField, Range(0, 1)]
    float _colourDrift;
    public float _ColourDrift
    {
        get { return _colourDrift; }
        set { _colourDrift = value; }
    }
    #endregion

    #region Private Properties
    [SerializeField]
    Shader _shader;
    Material _material;
    float _verticalJumpTime;
    #endregion

    #region MonoBehaviour Properties
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        _verticalJumpTime += Time.deltaTime * _verticalJump * 11.3f;

        float sl_thresh = Mathf.Clamp01(1 - _scanLineJitter * 1.2f);
        float sl_disp = 0.002f + Mathf.Pow(_scanLineJitter, 3) * 0.05f;

        Vector2 vj = new Vector2(_verticalJump, _verticalJumpTime);
        _material.SetVector("_VerticalJump", vj);

        _material.SetVector("_ScanLineJitter", new Vector2(sl_disp, sl_thresh));

        _material.SetFloat("_HorizontalShake", _horizontalShake * 0.2f);

        Vector2 cd = new Vector2(_colourDrift * 0.04f, Time.time * 606.11f);
        _material.SetVector("_ColourDrift", cd);

        Graphics.Blit(source, destination, _material);

    }
    #endregion
}
