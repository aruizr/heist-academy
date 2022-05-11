Shader "Custom/Vision Cone Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _InnerRadius ("Inner Radius", Range(0, 1)) = 0
        _FieldOfView ("Field Of View", Range(0, 360)) = 90
        _RadiusSaturation ("Radius Saturation", Float) = 70
        _FieldOfViewSaturation ("Field Of View Saturation", Float) = 1
    }
    SubShader
    {
        Tags 
        {
            "RenderType" = "Transparent"
        }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vertex_shader
            #pragma fragment fragment_shader
            
            #include "UnityCG.cginc"

            sampler2D _CameraDepthTexture;
            sampler2D _ViewDepthTexture;

            float4 _Color;
            float _InnerRadius;
            float _RadiusSaturation;
            float _FieldOfView;
            float _FieldOfViewSaturation;
            float4x4 _ViewSpaceMatrix;

            struct vertex_shader_input
            {
                float4 vertex_position : POSITION;
            };

            struct vertex_shader_output
            {
                float4 vertex_clip_position : SV_POSITION;
                float3 vertex_view_position : TEXCOORD1;
                float4 vertex_screen_position : TEXCOORD2;
            };

            vertex_shader_output vertex_shader (const vertex_shader_input input)
            {
                vertex_shader_output output;

                output.vertex_clip_position = UnityObjectToClipPos(input.vertex_position);
                output.vertex_view_position = UnityObjectToViewPos(input.vertex_position) * float3(1, 1, -1);
                output.vertex_screen_position = ComputeScreenPos(output.vertex_clip_position);
                
                return output;
            }

            float get_radius_alpha(const float radius)
            {
                const float inner_alpha = saturate(radius - _InnerRadius);
                const float final_alpha = max((0.5 - radius) * inner_alpha * _RadiusSaturation, 0);

                return final_alpha;
            }

            float get_angle_alpha(const float2 position)
            {
                const float angle_radians = _FieldOfView / 2 * UNITY_PI / 180;
                const float normalized_position = normalize(position);
                const float dot_position = max(dot(float2(0, 1), normalized_position), 0);
                const float angle_degrees = acos(dot_position);
                const float final_angles = angle_degrees / angle_radians;

                return max(1.0 - pow(final_angles, _FieldOfViewSaturation), 0);
            }

            float get_obstacle_alpha(const float4 world_position)
            {
                const float bias = 0.0001;
                const float4 position_view_space = mul(_ViewSpaceMatrix, world_position);
                const float3 projection_coordinates = position_view_space.xyz / position_view_space.w * 0.5 + 0.5;
                const float sampled_depth = 1.0 - SAMPLE_DEPTH_TEXTURE(_ViewDepthTexture, projection_coordinates.xy);
                const float depth_difference = projection_coordinates.z - bias - sampled_depth;

                return saturate(depth_difference > 0 ? 0 : 1);
            }
            
            fixed4 fragment_shader (vertex_shader_output input) : SV_Target
            {
                input.vertex_view_position *= _ProjectionParams.z / input.vertex_view_position.z;

                const float depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.vertex_screen_position);
                const float linear_depth = Linear01Depth(depth);
                const float4 view_position = float4(input.vertex_view_position * linear_depth, 1);
                const float4 world_position = mul(unity_CameraToWorld, view_position);

                clip(dot(normalize(ddy(world_position)), float3(0, 1, 0)) > 0.45 ? -1 : 1);
                
                float3 object_position = mul(unity_WorldToObject, world_position);

                object_position.y = 0;

                clip(float3(0.5, 0.5, 0.5) - abs(object_position.xyz));

                const float2 horizontal_position = object_position.xz;
                const float radius = length(horizontal_position);
                const float alpha = get_radius_alpha(radius) * get_angle_alpha(horizontal_position) * get_obstacle_alpha(world_position) * _Color.a;
                const float4 final_color = float4(_Color.rgb, alpha);

                return saturate(final_color);
            }
            
            ENDCG
        }
    }
}
