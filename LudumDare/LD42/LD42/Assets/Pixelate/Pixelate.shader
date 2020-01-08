// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Pixelate"{

	Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_PixelateX("Pixelate X",Int) = 5
		_PixelateY("Pixelate Y",Int) = 5
	}
	SubShader{
		Cull Off ZWrite Off ZTest Always

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			inline float getHue(fixed4 color)
			{
				float maxRgb = max(max(color.r, color.g), color.b);
				float minRgb = min(min(color.r, color.g), color.b);
				float deltaRgb = maxRgb - minRgb;

				if (deltaRgb == 0)
				{
					return 0;
				}
				else if (maxRgb == color.r)
				{
					return 60 * (((color.g - color.b) / deltaRgb) % 6);
				}
				else if (maxRgb == color.g)
				{
					return 60 * (((color.b - color.r) / deltaRgb) + 2);
				}
				else if (maxRgb == color.b)
				{
					return 60 * (((color.r - color.g) / deltaRgb) + 4);
				}

				return 0;
			}

			inline fixed4 getFirstSimilarColor(fixed4 color, float2 uv, int size, sampler2D tex)
			{
				float pixelWidth = 1.0 / _ScreenParams.x;
				float pixelHeight = 1.0 / _ScreenParams.y;
				for (int x = -size/2; x < size / 2; x++)
				{
					for (int y = -size/2; y < size / 2; y++)
					{
						fixed4 col = tex2D(tex, uv + float2(pixelWidth * x, pixelHeight * y));
						if (abs(getHue(col) - getHue(color)) < 1.5) //(length(col.rgb - color.rgb) < 0.15)
						{
							return col;
						}
					}

					return color;// *fixed4(0.1, 0.1, 0.1, 1);
				}
			}

			sampler2D _MainTex;
			sampler2D tex2;
			int _PixelateX;
			int _PixelateY;

			struct appdata{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
			};

			struct v2f{
				float2 uv:TEXCOORD0;
				float4 vertex:SV_POSITION;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex=UnityObjectToClipPos(v.vertex);
				o.uv=v.uv;
				tex2 = _MainTex;
				return o;
			}

			fixed4 frag(v2f i):SV_Target{
				int2 Pixelate=int2(_PixelateX,_PixelateY);
				fixed4 rescol=float4(0,0,0,0);
				if(_PixelateX>1 || _PixelateY>1){
					float2 PixelSize=1/float2(_ScreenParams.x,_ScreenParams.y);
					float2 BlockSize=PixelSize*Pixelate;
					float2 CurrentBlock=float2(
						(floor(i.uv.x/BlockSize.x)*BlockSize.x),
						(floor(i.uv.y/BlockSize.y)*BlockSize.y)
					);
					rescol=tex2D(_MainTex,CurrentBlock+BlockSize/2);
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/2,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,BlockSize.y/4));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,BlockSize.y/2));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,BlockSize.y/2));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/4,(BlockSize.y/4)*3));
					rescol+=tex2D(_MainTex,CurrentBlock+float2(BlockSize.x/2,(BlockSize.y/4)*3));
					rescol+=tex2D(_MainTex,CurrentBlock+float2((BlockSize.x/4)*3,(BlockSize.y/4)*3));
					rescol/=9;

					
				}else{
					rescol=tex2D(_MainTex,i.uv);
				}





				//tex2 = _MainTex;
				//rescol = tex2D(tex2, i.uv);
				//float thresholdColor = 0.72;
				//float thresholdHue = 20;
				//float pixelWidth = 1.0 / _ScreenParams.x;
				//float pixelHeight = 1.0 / _ScreenParams.y;
				//float2 pixelPos = i.uv * float2(_ScreenParams.x, _ScreenParams.y);

				//fixed4 leftColor = tex2D(tex2, i.uv + float2(-pixelWidth, 0));
				//fixed4 rightColor = tex2D(tex2, i.uv + float2(pixelWidth, 0));
				//fixed4 upColor = tex2D(tex2, i.uv + float2(0, -pixelHeight));
				//fixed4 downColor = tex2D(tex2, i.uv + float2(0, pixelHeight));

				if (!(_Time.z % _Time.y > sin(_Time.y)))
					rescol = getFirstSimilarColor(rescol, i.uv, 10, _MainTex);
				else
					rescol = getFirstSimilarColor(rescol, i.uv, 10, _MainTex);

				float4 p = UnityObjectToClipPos(float4(i.uv.x, i.uv.y, 0, 0));
				if (abs(floor(p.y * _ScreenParams.x + _Time.x)) % 2 == 0)
					//rescol /= abs(sin(i.uv.y * 1000 + _Time.z)) +1;
					rescol /= 1.1f;


				return rescol;
			}


			ENDCG
		}
	}
}
