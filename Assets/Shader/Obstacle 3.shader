Shader "Obstacle3" {
	Properties
	{
		_MainTex("2D Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

	}
		SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

		Pass
		{
		
			Stencil{
				Ref 4
				Comp always
				Pass replace
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
				//half3 diff = max(0, dot(i.normal, _WorldSpaceLightPos0.xyz)) * _LightColor0;

				fixed4 col;
				col.rgb = _Color;
				return col;
			}
			ENDCG
		}
	}
}
