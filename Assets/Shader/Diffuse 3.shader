Shader "Diffuse3" {
	Properties
	{
		_MainTex("2D Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_CoveredColor("Covered Color", Color) = (0,0,0,0)
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent+1" }

		// 隠れていない部分を描画するパス
		Pass
		{
			ColorMask 0

			Stencil{
				Ref 1
				Comp NotEqual
			}

			Tags { "LightMode" = "ForwardBase" }

			ZWrite Off

			CGPROGRAM
		   #pragma vertex vert
		   #pragma fragment frag

		   #include "UnityCG.cginc"

			struct appdata
			{
				half4 vertex : POSITION;
				half3 normal : NORMAL;
			};

			struct v2f
			{
				half4 pos : SV_POSITION;
				half3 normal: TEXCOORD1;
			};

			half4 _Color;
			half4 _LightColor0;

			v2f vert(appdata v)
			{
				v2f o = (v2f)0;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half3 diff = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz)) * _LightColor0;

				fixed4 col;
				col.rgb = _Color * diff;
				return col;
			}
			ENDCG
		}

		// 隠れている部分を描画するパス
		Pass
		{
			Stencil{
				Ref 4
				Comp Equal
			}

			ZWrite Off

			CGPROGRAM
		   #pragma vertex vert
		   #pragma fragment frag

		   #include "UnityCG.cginc"

			struct appdata
			{
				half4 vertex : POSITION;
			};

			struct v2f
			{
				half4 pos : SV_POSITION;
			};

			half4 _CoveredColor;

			v2f vert(appdata v)
			{
				v2f o = (v2f)0;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _CoveredColor;
			}
			ENDCG
		}
	}
}