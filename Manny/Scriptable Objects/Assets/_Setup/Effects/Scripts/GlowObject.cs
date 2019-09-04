using UnityEngine;
using System.Collections.Generic;

public class GlowObject : MonoBehaviour
{
	public Color glowColor;
	[Range(1, 30)]
	[SerializeField]
	private float lerpMultiplier = 12;

	private List<Material> materials = new List<Material>();
	private Color currentColor, targetColor;
	private Renderer[] renders;

	private void Start()
	{
		renders = GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renders.Length; i++)
		{
			materials.AddRange(renders[i].materials);
		}
	}

	private void Update()
	{
		if (currentColor == targetColor)
		{
			enabled = false;
		}
        else
		{
			currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpMultiplier);
			for (int i = 0; i < materials.Count; i++)
			{
				materials[i].SetColor("_GlowColor", currentColor);
			}
		}
	}

	private void OnMouseEnter()
	{
		targetColor = glowColor;
		enabled = true;
	}

	private void OnMouseExit()
	{
		targetColor = Color.black;
		enabled = true;
	}
}
