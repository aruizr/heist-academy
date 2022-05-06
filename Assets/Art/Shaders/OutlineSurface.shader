// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HeistAcademy/OutlineSurface"
{
    Properties
    {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        _Metallic ("Metalness", Range(0, 1)) = 0
        [HDR] _Emission ("Emission", color) = (0,0,0)
        
        // OUTLINE PASS PROPERTIES
        _OutColor("Outline Color", Color) = (0, 0, 0, 1) // Outline color
        _OutValue("Outline Value", Range(0, .1)) = 0.006 // Amount to extrude the outline mesh
    }
    SubShader
    {
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        
        half _Metallic;
        half _Smoothness;
        half3 _Emission;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            c *= _Color;
            o.Albedo = c.rgb;
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = c.a;
        }
        ENDCG
        
        Pass{
            Cull front 
            
            CGPROGRAM
            #include "UnityCG.cginc"
            
            #pragma vertex vert // Vertex shader
            #pragma fragment frag // Fragment shader

            fixed4 _OutColor; // Color of the outline
            float _OutValue; // Thickness of the outline

            struct appdata // Data put into vertex shader
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };

            struct v2f // Data used to generate fragments and read by vertex shader
            {
                float4 position: SV_POSITION;
            };

            v2f vert (appdata v) // Vertex shader
            {
                v2f o;

                // Convert vertex position from object to clip space
                //o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * _OutValue);
                //o.position = UnityObjectToClipPos(v.vertex*_Scale + normalize(v.normal) * _OutValue);
                //o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * _OutValue * _Scale);
                o.position = UnityObjectToClipPos(v.vertex + v.normal * _OutValue);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target // Fragment shader
            {
                return _OutColor;
            }
            ENDCG
        }
    }
    
    FallBack "Standard"
}
