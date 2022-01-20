Shader "Custom/CoiledWire"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NumSpirals("Spirals", Range(0,50)) = 20
        _CableThickness("CableThickness", Range(0,50)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _NumSpirals;
        float _CableThickness;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float3 lerp3(float3 a, float3 b, float3 c, float t)
        {
            if (t <= 0.5f)
                return lerp(a, b, t / 0.5);
            else
                return lerp(b, c, (t - 0.5) / 0.5);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // ported from https://medium.com/xrlo-extended-reality-lowdown/how-to-create-a-coiled-cable-shader-in-ue4-8bb47777d8ab

            // build coilded uvs
            float2 coiledUVs = IN.uv_MainTex;
            coiledUVs.x = frac(coiledUVs.x + coiledUVs.y * _NumSpirals); 
          
            // normalize cable:
            float cable = saturate((coiledUVs.x / _CableThickness + 0.5) - (0.5 /_CableThickness));

            // buld normals:
            o.Normal = normalize(lerp3(float3(0,-1,0), float3(0,0,1), float3(0,1,0),cable));

            // build height map:
            float height = min(1 - cable, cable) * 2;

            // clip fragments outside the cable thickness:
            clip (height-0.01);

            // flat albedo color (could sample a texture here if desired)
            o.Albedo = _Color;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
