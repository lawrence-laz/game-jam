Shader "Hidden/classic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VertsColor("Verts fill color", Float) = 0
        _VertsColor2("Verts fill color 2", Float) = 0
        _Contrast("Contrast", Float) = 0
        _Br("Brightness", Float) = 0
        _ScansColor("Scans color", Float) = 0.5
        _Split("Split", Float) = 3
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 src_pos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.src_pos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _MainTex;
            uniform float _VertsColor;
            uniform float _VertsColor2;
            uniform float _Contrast;
            uniform float _Br;
            uniform float _ScansColor;
            uniform float _Split;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                float2 ps = i.src_pos.xy * _ScreenParams.xy / i.src_pos.w;

                float step = i.uv / _ScreenParams.xy;

                int pp = (int)ps.x % 4;

                color += (_Br / 255);
                color = color - _Contrast * (color - 1.0) * color * (color - 0.5);

                float4 muls = float4(0, 0, 0, 0);
                if (pp == 1) { muls.r = 1; muls.g = _VertsColor; muls.b = _VertsColor2; }
                else if (pp == 2) { muls.r = _VertsColor; muls.g = 1; muls.b = _VertsColor; }
                else { muls.r = _VertsColor2; muls.g = _VertsColor; muls.b = 0.8; }
                
                if ((int)ps.y % 3 == 0) muls *= float4(_ScansColor, _ScansColor, _ScansColor, 1);

                color = color * muls;

                color += tex2D(_MainTex, i.uv + step * _Split) * 0.01 * _Split;
                color += tex2D(_MainTex, i.uv - step * _Split) * 0.01 * _Split;

                return color;
            }
            ENDCG
        }
    }
}
