

Shader "Custom/Sprite/SpriteMarked" {
Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Color("Color",color) = (1,1,1,1)
    _MaxTexFade("Max Fade of Texture",Range(0,1)) = 0.7
    _MinTexFade("Min Fade of Texture",Range(0,1)) = 0.45
		_OffSet("y offset",float) = 0.05
	_Speed("Speed of fade",float) = 2
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

   // ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
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
			float _MinTexFade;
			float _MaxTexFade;
			float _Speed;
			float _OffSet;
			//Vertex
            v2f vert (appdata v)
            {
                v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.position.y += _OffSet;
                o.uv = v.uv;
                return o;
            }
			//Fragment
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
			    float fade = (abs((_MaxTexFade- _MinTexFade) * sin(_Time.y*_Speed))) + _MinTexFade;
			    col.rgb = col.rgb * fade  + _Color.rgb * (1-fade);
                return col;
            }
        ENDCG
    }
}

}
