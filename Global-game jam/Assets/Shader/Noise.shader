Shader "Custom/Noise" {
	Properties {
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white"
        time ("time", Float) = 1.0
        baseSpeed ("baseSpeed", Float) = 1.0
        noiseScale ("NoiseScale", Float) = 1.0
        decalU ("decalU", Float) = -10.7
        decalV ("decalV", Float) = 1.5
    }
	SubShader {
		Tags {"Queue" = "Transparent"}
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _Noise;
            uniform sampler2D _MainTex;
            uniform float time;
            uniform float noiseScale;
            uniform float baseSpeed;
            uniform float decalU;
            uniform float decalV;


            float4 frag(v2f_img i) : COLOR {
            	if (tex2D( _MainTex, i.uv).a <= 0)
            		discard; 
			float2 uvTimeShift;
			if(time%10 < 5)
				uvTimeShift = i.uv + float2(decalU, decalV) * time * baseSpeed;
			else
				uvTimeShift = i.uv - float2(decalU, decalV) * time * baseSpeed;

				float4 noiseGeneratorTimeShift = tex2D( _Noise, uvTimeShift );
				float2 uvNoiseTimeShift = i.uv + noiseScale * float2( noiseGeneratorTimeShift.r, noiseGeneratorTimeShift.b );
				float4 result = tex2D( _MainTex, uvNoiseTimeShift).rgba;
					
				return result;

            }
            ENDCG
        }
    }
}
