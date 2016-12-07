Shader "zOutline" {
    Properties {
        _Color ("Color", Color) = (0.0,0.3,0.75,1)
        _Thickness ("Thickness", Float ) = .5
        _Brightness ("Brightness", Float ) = 2
    }
    SubShader {
        Tags {
            "Queue"="Overlay"
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase" //Not deferred
            }
            ZTest Greater
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha //Alpha blending

            
            CGPROGRAM
            #pragma vertex vert //Compiler settings, not sure if needed or not
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            uniform float4 _Color;
            uniform float _Thickness;
            uniform float _Brightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
             	//Fresnel
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 col = (_Brightness*_Color.rgb);
                return fixed4(col,(_Color.a*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Thickness)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}