Shader "Custom/Acid"
{
    Properties
    {
        _Tint("Colour Tint", Color) = (1,1,1,1)
        _Speed("Speed", Range(0.1,100)) = 10
        _Scale1("Scale 1", Range(0.1,10)) = 2
        _Scale2("Scale 2", Range(0.1,10)) = 2
        _Scale3("Scale 3", Range(0.1,10)) = 2
        _Scale4("Scale 4", Range(0.1,10)) = 2
        _GScale("GScale", Range(0.0,5)) = 0
        _BScale("BScale", Range(1,5)) = 1
        _EScale("EScale", Range(0.01,3)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        float4 _Tint;
        float _Speed;
        float _Scale1;
        float _Scale2;
        float _Scale3;
        float _Scale4;
        float _GScale;
        float _BScale;
        float _EScale;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            const float PI = 3.14169265;
            float t = _Time.x * _Speed;

            float c = sin(IN.worldPos.x * _Scale1 + t);
            c += sin(IN.worldPos.z * _Scale2 + t);
            c += sin(_Scale3 * (IN.worldPos *sin(t/2.0) + IN.worldPos.z *cos(t/3)) + t) ;
            
            float red = sin(10*c/4.0*PI)/10;
            float green = sin(5*c/4.0*PI + 1*PI/2) + _GScale;
            float blue = sin(3*c/4.0*PI + PI)/_BScale;

            o.Albedo.r = red;
            o.Albedo.g = green;
            o.Albedo.b = blue;
            o.Albedo *= _Tint;
            o.Emission.r = red *_EScale;
            o.Emission.b = green*_EScale;
            o.Emission.g = blue*_EScale;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
