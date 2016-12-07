Shader "stenciledVertexColorLocal" {
    Properties {
        _Overlay ("Overlay", 2D) = "white" {}
        _OverlayStrength ("OverlayStrength", Float ) = .90
        _Cutoff ("Cutoff", Float ) = .48
    }
    SubShader {
        Tags {
            "Queue"="Background"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            /*
			Stencil {
                Ref 1
                ReadMask 1
                WriteMask 1
                Comp NotEqual
            }
			*/
            Stencil {
                Ref 1
                ReadMask 1
                WriteMask 1
                Pass Replace
				//ZFail
				//Fail
				//https://docs.unity3d.com/Manual/SL-Stencil.html
            }
            CGPROGRAM
            #pragma vertex vert // use "vert" function as the vertex shader
            #pragma fragment frag // use "frag" function as the pixel (fragment) shader
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
			
            uniform sampler2D _Overlay; uniform float4 _Overlay_ST; //Need ST or it breaks
            uniform float _Cutoff;
            uniform float _OverlayStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput vo = (VertexOutput)0;
                vo.vertexColor = v.vertexColor;
                vo.posWorld = mul(unity_ObjectToWorld, v.vertex);
                //vo.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return vo;
            }
            float4 frag(VertexOutput vo) : COLOR {
				//rb is x and z respect
                float4 tColor = tex2D(_Overlay,TRANSFORM_TEX(vo.posWorld.rgb.rg, _Overlay));
                clip((((tColor.r*_OverlayStrength)+vo.vertexColor.a)-_Cutoff) - 0.5);
                float3 finalColor = 0;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
}
