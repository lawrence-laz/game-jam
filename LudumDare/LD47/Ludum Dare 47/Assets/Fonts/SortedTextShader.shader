Shader "Custom/SortedTextShader"
{

	Properties
	{
		_MainTex("Font Texture", 2D) = "white" {}
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
		Cull Off
		ZWrite Off
		Fog{ Mode Off }

		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			ColorMaterial AmbientAndDiffuse
			SetTexture[_MainTex]
			{
				combine primary, texture * primary
			}
		}
	}

	FallBack "Diffuse"
}
