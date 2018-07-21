

Shader "Custom/Sprite/SpriteRanged" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color("Color",color) = (1,1,1,1)
		_MaxTexFade("Max Fade of Texture",Range(0,1)) = 0.7
			
	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		// ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
	
			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			// float4 _MainTex_ST;
			float4 _Color;
			float _MaxTexFade;
			float _Speed;
			//Vertex
			v2f vert(appdata v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			//Fragment
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb = col.rgb * _MaxTexFade + _Color.rgb * (1 - _MaxTexFade);
				return col;
			}
		ENDCG
		}
	}

}
