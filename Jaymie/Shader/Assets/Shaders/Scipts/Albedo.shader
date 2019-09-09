Shader "Lesson/Albedo" //This section allows for easy sorting of our shader in the shader menu.
{
	Properties //These are the public properties seen on a material.
	{
		_Texture("Texture", 2D) = "black"{} //Variable name is _Texture, Display name is Texture. This is 2D and the default untextured colour is black.
	}

	SubShader //There can be multiple SubShaders running at the same time as they are using different GPU levels on different platforms.
	{
		Tags //Tags are basically key-value pairs. Inside a SubShader, tags are used to determine rendering order and other parameters of a SubShader.
		{
			"RenderType" = "Opaque"  //RenderType tag categorizes shaders into serveral pre-defined groups.

		}

		CGPROGRAM //This is the start of our C for Graphics Language.
			#pragma surface MainColour Lambert //The surface of our models is affected by the mainColour Function. The material type is Lambert. Lambert is a flat material that has no highlights.
			sampler2D _Texture; //This connects our _Texture variable that is in the properties section to our 2D _Texture variable in CG.
			struct Input
			{
				float2 uv_Texture; //This is in refernce to our UV of our model. UV maps are wrapping of a model. UV denotes the axes of the 2D texture.
			};

			void MainColour(Input IN, inout SurfaceOutput o)
			{
				o.Albedo = tex2D(_Texture, IN.uv_Texture).rgb; //Albedo is in reference to the surface Image and RGB of our model. We are setting the models surface to the colour of our Texture2D and matching the Texture to our models UV mapping.
			}
		ENDCG //This is the end of our C for Graphics Language
	}
	FallBack "Diffuse" //If all else fails standard shader(Lambert and Texture)
}
