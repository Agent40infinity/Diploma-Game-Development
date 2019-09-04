using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlowPrePass : MonoBehaviour
{
	private static RenderTexture prePass;
	private static RenderTexture blurred;

	private Material blurMat;

	private void OnEnable()
	{
		prePass = new RenderTexture(Screen.width, Screen.height, 24);
		prePass.antiAliasing = QualitySettings.antiAliasing;
		blurred = new RenderTexture(Screen.width >> 1, Screen.height >> 1, 0);

		Camera camera = GetComponent<Camera>();
		Shader glowShader = Shader.Find("Hidden/GlowReplace");
		camera.targetTexture = prePass;
		camera.SetReplacementShader(glowShader, "Glowable");
		Shader.SetGlobalTexture("_GlowPrePassTex", prePass);

		Shader.SetGlobalTexture("_GlowBlurredTex", blurred);

		blurMat = new Material(Shader.Find("Hidden/Blur"));
		blurMat.SetVector("_BlurSize", new Vector2(blurred.texelSize.x * 1.5f, blurred.texelSize.y * 1.5f));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination);

		Graphics.SetRenderTarget(blurred);
		GL.Clear(false, true, Color.clear);

		Graphics.Blit(source, blurred);

		for (int i = 0; i < 4; i++)
		{
			RenderTexture temp = RenderTexture.GetTemporary(blurred.width, blurred.height);
			Graphics.Blit(blurred, temp, blurMat, 0);
			Graphics.Blit(temp, blurred, blurMat, 1);
			RenderTexture.ReleaseTemporary(temp);
		}

		Graphics.Blit(source, destination);
	}
}
