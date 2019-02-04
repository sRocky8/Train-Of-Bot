Shader "Custom/My shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_RampTex("Ramp", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_OutlineExtrusion("Outline Extrusion", float) = 0
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutlineDot("Outline Dot", float) = 0.25
	}
		
	SubShader
	{
		// Regular color & lighting pass
		Pass
		{
			Tags
			{
			"LightMode" = "ForwardBase"
			}
			// Lighting/ Texture Pass
			Stencil
			{
				Ref 4
				Comp always
				Pass replace
				ZFail keep
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"
			#include "UnityCG.cginc"
			// Properties
			sampler2D _MainTex;
			sampler2D _RampTex;
			float4 _Color;
			float4 _LightColor0; // provided by Unity

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
				LIGHTING_COORDS(1,2) // shadows
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				// convert input to world space
				output.pos = UnityObjectToClipPos(input.vertex);
				float4 normal4 = float4(input.normal, 0.0); 
				output.normal = normalize(mul(normal4, unity_WorldToObject).xyz);
				output.texCoord = input.texCoord;
				TRANSFER_VERTEX_TO_FRAGMENT(output);
				return output;
			}
						
			float4 frag(vertexOutput input) : COLOR
			{
				// convert light direction to world space & normalize
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

				// finds location on ramp texture to sample based on angle between surface normal and light direction
				float ramp = clamp(dot(input.normal, lightDir), 0, 1.0);
				float3 lighting = tex2D(_RampTex, float2(ramp, 0.5)).rgb;
				
				// sample texture for color
				float4 albedo = tex2D(_MainTex, input.texCoord.xy);
				float attenuation = LIGHT_ATTENUATION(input);
				float3 rgb = albedo.rgb * _LightColor0.rgb * lighting * _Color.rgb * attenuation;
				return float4(rgb, 1.0);
			}
			ENDCG
		}

		// Shadow pass
		Pass
    	{
            Tags 
			{
				"LightMode" = "ShadowCaster"
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }
            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
    	}

		// Outline pass
		Pass
		{
			// Won't draw where it sees ref value 4
			Cull OFF
			ZWrite OFF
			ZTest ON
			Stencil
			{
				Ref 4
				Comp notequal
				Fail keep
				Pass replace
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Properties
			uniform float4 _OutlineColor;
			uniform float _OutlineSize;
			uniform float _OutlineExtrusion;
			uniform float _OutlineDot;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR;
			};
			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				float4 newPos = input.vertex;

				// normal extrusion technique
				float3 normal = normalize(input.normal);
				newPos += float4(normal, 0.0) * _OutlineExtrusion;

				// convert to world space
				output.pos = UnityObjectToClipPos(newPos);

				output.color = _OutlineColor;
				return output;
			}
			float4 frag(vertexOutput input) : COLOR
			{
				return input.color;
			}
			ENDCG
		}
	}
}