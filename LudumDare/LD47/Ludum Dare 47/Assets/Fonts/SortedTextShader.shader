Shader "Custom/SortedTextShader"
{

	Properties
	{
		_MainTex("Font Texture", 2D) = "white" {}
		_Color("Text Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		Lighting Off
		Cull Back
		ZWrite Off
		Fog{ Mode Off }

		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			Color[_Color]
			Cull[_CullMode]
			ColorMaterial AmbientAndDiffuse
			SetTexture[_MainTex]
			{
				combine primary, texture * primary
			}
		}
	}

	FallBack "Diffuse"
}
