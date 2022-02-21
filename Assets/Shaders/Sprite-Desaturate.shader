
Shader "Sprites/Saturation"
{
   Properties
   {
      [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
      _Color("Tint", Color) = (1,1,1,1)
      _Saturation("Saturation", Range(0,1)) = 1
      [Toggle(BILLBOARD)] Billboard("Billboard", Float) = 0
      [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
      [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
      [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
      [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
      [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
   }
   SubShader
   {
      Tags
      {
         "Queue"="Transparent"
         "IgnoreProjector"="True"
         "RenderType"="Transparent"
         "PreviewType"="Plane"
         "CanUseSpriteAtlas"="True"
         "DisableBatching"="True"
      }
      Cull Off
      Lighting Off
      ZWrite Off
      Blend One OneMinusSrcAlpha
      Pass
      {
      CGPROGRAM
        #include "UnitySprites.cginc"
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma multi_compile _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #pragma multi_compile _ BILLBOARD
        float _Saturation;
        v2f Vert(appdata_t IN){
            v2f OUT;
            UNITY_SETUP_INSTANCE_ID (IN);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
         #ifdef UNITY_INSTANCING_ENABLED
            IN.vertex.xy *= _Flip.xy;
         #endif
         #ifdef BILLBOARD
				float3 vpos = mul((float3x3)unity_ObjectToWorld, IN.vertex.xyz);
				float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
				float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
				float4 outPos = mul(UNITY_MATRIX_P, viewPos);
            OUT.vertex = outPos;
         #else
            OUT.vertex = UnityObjectToClipPos(IN.vertex);
         #endif
            OUT.texcoord = IN.texcoord;
            OUT.color = IN.color * _Color * _RendererColor;
            #ifdef PIXELSNAP_ON
            OUT.vertex = UnityPixelSnap (OUT.vertex);
            #endif
            return OUT;
        }
        fixed4 Frag(v2f IN): SV_Target{
            fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
            c.rgb = lerp(c.rgb, dot(c.rgb, float3(0.3,0.59,0.11)), 1-_Saturation);
            c.rgb *= c.a;

            return c;
        }
        #pragma vertex Vert
        #pragma fragment Frag
      ENDCG
      }
   }
}