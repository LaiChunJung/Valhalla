// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Mobile/Mobile-Xray" {
    Properties {
   	 _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color("_Color", Color) = (0,1,0,1)
        _Inside("_Inside", Range(0,1) ) = 0
        _Rim("_Rim", Range(0,2) ) = 1.2
        _OccludeColor ("Occlusion Color", Color) = (0,0,1,1)
    }
    SubShader {
        Tags { "Queue" = "Transparent"}
        
        Pass {
            ZWrite Off
            Blend One Zero
            ZTest Greater
            Color [_OccludeColor]
        }
        
    Pass {
    Tags {"LightMode" = "Vertex"}
        Cull off
        Zwrite On
        Blend oneminusdstcolor one
                
        CGPROGRAM
        
        #pragma vertex vert
        #pragma fragment frag

        #include "HLSLSupport.cginc"
        #include "UnityCG.cginc"


        struct v2f_surf {
              half4 pos     : SV_POSITION;
              fixed4 finalColor : COLOR;
        };
        
        uniform half4 _Color;
        uniform half _Rim;
        uniform half _Inside;
        
        

        v2f_surf vert (appdata_base v) {
        v2f_surf o;
            
            o.pos = UnityObjectToClipPos (v.vertex);
            half3 uv = mul( (float3x3)UNITY_MATRIX_IT_MV, v.normal );
            uv = normalize(uv);
            o.finalColor = lerp(half4(0,0,0,0),_Color,saturate(max(1- pow (uv.z,_Rim),_Inside)));
            return o;
        }
        
        fixed4 frag (v2f_surf IN) : COLOR {

            return IN.finalColor;
        }
        
        
 
    ENDCG
    
    }
    
    
    
     }

FallBack off
}