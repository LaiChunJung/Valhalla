// Toony Colors Pro+Mobile Shaders
// (c) 2013,2014 Jean Moreno

//Texture Dissolve
//2015, Edit by Mahua

Shader "Toony Colors Pro/Normal/MultipleLights/Bumped Specular Rim + Texture Dissolve"
{
	Properties
	{
		_MainTex ("Base (RGB) Gloss (A) ", 2D) = "white" {}
		_BumpMap ("Normal map (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		
		//DISSOLVE TEXTURE
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}			
		_DissolveSrcBump ("DissolveSrcBump", 2D) = "white" {}	
		
		//SPECULAR
		_MainColor ("Main Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		
		//COLORS
		_Color ("Highlight Color", Color) = (0.8,0.8,0.8,1)
		_SColor ("Shadow Color", Color) = (0.0,0.0,0.0,1)
		
		//RIM LIGHT
		_RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.6)
		_RimPower ("Rim Power", Range(-2,10)) = 0.5
		
		//DISSOLVE PARAMETER
		_Amount ("Amount", Range (0, 1)) = 0.5				
		_StartAmount("StartAmount", Range (0, 0.5)) = 0.1				
		_Illuminate ("Illuminate", Range (0, 1)) = 0.5			
		_Tile("Tile", Range (0.1, 3)) = 1							
		_DissColor ("DissColor", Color) = (1,1,1,1)				
		_ColorAnimate ("ColorAnimate", vector) = (1,1,1,1)		
		
		_Cutoff ("Cut Off", Range(0, 1)) = 1
		
		//OUTLINE
		_Outline ("Outline Width", Range(0,0.1)) = 0.005
		_OutlineColor ("Outline Color", Color) = (0.2, 0.2, 0.2, 1)
		
		//------------
//		_Color ("Main Color", Color) = (1,1,1,1)
//        	_MainTex ("Base (RGB)", 2D) = "white" {}
//       		_OccludeColor ("Occlusion Color", Color) = (0,0,1,1)
	}
	
	SubShader
	{
//		Pass 
//		{
//           		ZWrite Off
//            		Blend One Zero
//            		ZTest Greater
//            		Cull off
//            		Color [_OccludeColor]
//            	}
//            	
//            	Pass {
//            		Tags {"LightMode" = "Vertex"}
//           		ZWrite On
//            		Lighting On
//
//            		Material {
//                		Diffuse [_Color]
//                		Ambient [_Color]
//           		}
//            
//           		SetTexture [_MainTex] {
//                		ConstantColor [_Color]
//               			Combine texture * primary DOUBLE, texture * constant
//           		}
//        	}
	
		Tags 
		{
			"Queue" = "AlphaTest"
            "IgnoreProjector" = "True" 
            "RenderType" = "TransparentCutout"
		}
		LOD 400
		cull Off
		Lighting On
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		
		#include "TGP_Include.cginc"
		#pragma target 3.0
		//nolightmap nodirlightmap		LIGHTMAP
		//approxview halfasview			SPECULAR/VIEW DIR
		#pragma surface surf ToonyColorsSpec nolightmap nodirlightmap approxview alphatest:_Cutoff addshadow 
		//#pragma exclude_renderers flash
		
		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed _RimPower;
		fixed4 _RimColor;
		fixed _Shininess;
		
		sampler2D _DissolveSrc;
		sampler2D _DissolveSrcBump;
		fixed4 _MainColor;
		fixed4 _DissColor;
		fixed _Amount;
		static half3 Color = float3(1,1,1);
		fixed4 _ColorAnimate;
		fixed _Illuminate;
		fixed _Tile;
		fixed _StartAmount;
		
		struct Input
		{
			fixed2 uv_MainTex : TEXCOORD0;
			fixed2 uv_BumpMap : TEXCOORD1;
			fixed3 viewDir;
			fixed2 uvDissolveSrc;
		};
		
		void vert (inout appdata_full v, out Input o) {}
	
		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			
			o.Albedo = c.rgb * _MainColor.rgb;
			
			fixed ClipTex = tex2D (_DissolveSrc, IN.uv_MainTex/_Tile).r;
			fixed ClipAmount = ClipTex - _Amount;
			fixed Clip = 0;
			//float4 DematBump =  tex2D (_DissolveSrcBump,IN.uv_MainTex/_Tile);
			
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			
			if (_Amount > 0)
			{
				if (ClipAmount <0)
				{
					Clip = 1;
					//clip(-0.1);
				}
				else
				{
					if (ClipAmount < _StartAmount)
					{
						if (_ColorAnimate.x == 0)
							Color.x = _DissColor.x;
						else
							Color.x = ClipAmount/_StartAmount;
			          
						if (_ColorAnimate.y == 0)
							Color.y = _DissColor.y;
						else
							Color.y = ClipAmount/_StartAmount;
			          
						if (_ColorAnimate.z == 0)
							Color.z = _DissColor.z;
						else
							Color.z = ClipAmount/_StartAmount;

						o.Albedo  = (o.Albedo * ((Color.x+Color.y+Color.z)) * Color * ((Color.x+Color.y+Color.z))) / (1 - _Illuminate);
						//o.Normal = UnpackNormal(tex2D(_DissolveSrcBump, IN.uvDissolveSrc));
					}
				}
			}
			 
			if (Clip == 1)
			{
				clip(-0.1);
			}
			
			//Specular
			//o.Gloss = c.a;
			//o.Specular = _Shininess;
			
			//Rim Light
			fixed rim = 1.0f - saturate( dot(normalize(IN.viewDir), o.Normal) );
			o.Emission = (_RimColor.rgb * pow(rim, _RimPower)) * _RimColor.a;
			o.Gloss = c.a;
			o.Alpha = c.a;
			o.Specular = _Shininess;
		}
		ENDCG
		UsePass "Hidden/ToonyColors-Outline/OUTLINE"
	}
	
	Fallback "Transparent/Cutout/VertexLit"//"Toony Colors Pro/Normal/MultipleLights/Bumped Rim"
}
