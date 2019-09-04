using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlowComposite : MonoBehaviour
{
	[Range(0, 10)]
	[SerializeField]
	private float intensity = 3;

	private Material compositeMat;

	private void OnEnable()
	{
		compositeMat = new Material(Shader.Find("Hidden/GlowComposite"));
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		compositeMat.SetFloat("_Intensity", intensity);
		Graphics.Blit(src, dst, compositeMat, 0);
	}
}
