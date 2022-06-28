Shader "Custom/Circle Shader"
{
    Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Radius("Radius", Range(0,1)) = 1
		_Thickness("Thickness", Range(0,1)) = 1
		_ForegroundMask("Foreground Mask", 2D) = "white" {}
	}
	SubShader
	{
		Tags 
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha:fade	
		#pragma target 3.0

		sampler2D _ForegroundMask;

		struct Input {
			float2 uv_ForegroundMask;
		};

		float4 _Color;
		float _Thickness;
		float _Radius;

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			const float x = (-0.5 + IN.uv_ForegroundMask.x) * 2;
			const float y = (-0.5 + IN.uv_ForegroundMask.y) * 2;
			const float radius = 1 - sqrt(x*x + y*y);
			
			clip(radius - _Radius);
			
			o.Albedo = _Color;
			if (radius - _Radius < _Thickness) {
				o.Alpha = _Color.a;
			}
			else
			{
				o.Alpha = 0;
			}
			// o.Alpha = 1;
			o.Emission = _Color.rgb;
		}
		ENDCG
	}
	FallBack "Standard"
}
