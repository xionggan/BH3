// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|normal-6976-RGB,emission-7257-OUT,custl-3009-OUT,olwid-8484-OUT,olcol-6317-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:5019,x:32652,y:33098,varname:node_5019,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3716,x:32652,y:32965,varname:node_3716,prsc:2;n:type:ShaderForge.SFN_LightVector,id:5423,x:31776,y:32609,varname:node_5423,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:1150,x:31776,y:32741,prsc:2,pt:True;n:type:ShaderForge.SFN_HalfVector,id:1431,x:31776,y:32946,varname:node_1431,prsc:2;n:type:ShaderForge.SFN_Dot,id:2941,x:31988,y:32609,cmnt:Lambert,varname:node_2941,prsc:2,dt:1|A-5423-OUT,B-1150-OUT;n:type:ShaderForge.SFN_Dot,id:3843,x:31988,y:32883,cmnt:Blinn-Phong,varname:node_3843,prsc:2,dt:1|A-1150-OUT,B-1431-OUT;n:type:ShaderForge.SFN_Multiply,id:8597,x:32383,y:32878,cmnt:Specular Contribution,varname:node_8597,prsc:2|A-7331-OUT,B-8305-OUT,C-5308-RGB,D-3896-OUT,E-4049-OUT;n:type:ShaderForge.SFN_Tex2d,id:9017,x:31989,y:32118,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2119,x:32458,y:32413,cmnt:Diffuse Contribution,varname:node_2119,prsc:2|A-7009-OUT,B-7331-OUT;n:type:ShaderForge.SFN_Color,id:3647,x:31989,y:32311,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Exp,id:4903,x:31988,y:33066,varname:node_4903,prsc:2,et:1|IN-204-OUT;n:type:ShaderForge.SFN_Slider,id:9955,x:31432,y:33069,ptovrint:False,ptlb:SpecularRange,ptin:_SpecularRange,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Power,id:8305,x:32186,y:32953,varname:node_8305,prsc:2|VAL-3843-OUT,EXP-4903-OUT;n:type:ShaderForge.SFN_Add,id:5903,x:32652,y:32825,cmnt:Combine,varname:node_5903,prsc:2|A-2119-OUT,B-8597-OUT;n:type:ShaderForge.SFN_Multiply,id:3009,x:32897,y:32965,cmnt:Attenuate and Color,varname:node_3009,prsc:2|A-5903-OUT,B-3716-RGB,C-5019-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:204,x:31776,y:33068,varname:node_204,prsc:2,a:10,b:0|IN-9955-OUT;n:type:ShaderForge.SFN_Color,id:5308,x:32186,y:33107,ptovrint:False,ptlb:Spec Color_copy,ptin:_SpecColor_copy,varname:_SpecColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7257,x:32996,y:32391,cmnt:Ambient Light,varname:node_7257,prsc:2|A-7009-OUT,B-7618-RGB,C-7882-OUT;n:type:ShaderForge.SFN_Multiply,id:7009,x:32193,y:32118,cmnt:Diffuse Color,varname:node_7009,prsc:2|A-9017-RGB,B-3647-RGB;n:type:ShaderForge.SFN_Slider,id:8484,x:32597,y:33472,ptovrint:False,ptlb:OutLineWidth,ptin:_OutLineWidth,varname:node_1130,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.005,max:1;n:type:ShaderForge.SFN_Color,id:6317,x:32741,y:33590,ptovrint:False,ptlb:OutLineColor,ptin:_OutLineColor,varname:node_6427,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6976,x:32875,y:32704,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:node_8635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:7882,x:32476,y:32126,ptovrint:False,ptlb:AmbientPower,ptin:_AmbientPower,varname:node_5567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Step,id:7331,x:32186,y:32609,varname:node_7331,prsc:2|A-926-OUT,B-2941-OUT;n:type:ShaderForge.SFN_Slider,id:926,x:31832,y:32515,ptovrint:False,ptlb:DiffuseSplitValue,ptin:_DiffuseSplitValue,varname:node_5003,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Slider,id:3896,x:32049,y:33306,ptovrint:False,ptlb:SpecularPower,ptin:_SpecularPower,varname:node_3481,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Tex2d,id:4030,x:31990,y:33406,ptovrint:False,ptlb:LightMap,ptin:_LightMap,varname:node_4824,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Desaturate,id:4049,x:32206,y:33406,varname:node_4049,prsc:2|COL-4030-RGB;n:type:ShaderForge.SFN_Color,id:7618,x:32667,y:32413,ptovrint:False,ptlb:AmbientLightColor,ptin:_AmbientLightColor,varname:node_7618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:9017-3647-9955-5308-926-3896-4030-8484-6317-6976-7882-7618;pass:END;sub:END;*/

Shader "Shader Forge/Cartoon1" {
    Properties {
        _Texture ("Texture", 2D) = "black" {}
        _Color_copy ("Color_copy", Color) = (0.5,0.5,0.5,1)
        _SpecularRange ("SpecularRange", Range(0, 1)) = 0.2
        _SpecColor_copy ("Spec Color_copy", Color) = (1,1,1,1)
        _DiffuseSplitValue ("DiffuseSplitValue", Range(0, 1)) = 0.5
        _SpecularPower ("SpecularPower", Range(0, 2)) = 1
        _LightMap ("LightMap", 2D) = "white" {}
        _OutLineWidth ("OutLineWidth", Range(0, 1)) = 0.005
        _OutLineColor ("OutLineColor", Color) = (0,0,0,1)
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _AmbientPower ("AmbientPower", Range(0, 10)) = 1
        _AmbientLightColor ("AmbientLightColor", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _OutLineWidth;
            uniform float4 _OutLineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutLineWidth,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutLineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _Color_copy;
            uniform float _SpecularRange;
            uniform float4 _SpecColor_copy;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _AmbientPower;
            uniform float _DiffuseSplitValue;
            uniform float _SpecularPower;
            uniform sampler2D _LightMap; uniform float4 _LightMap_ST;
            uniform float4 _AmbientLightColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 node_7009 = (_Texture_var.rgb*_Color_copy.rgb); // Diffuse Color
                float3 emissive = (node_7009*_AmbientLightColor.rgb*_AmbientPower);
                float node_7331 = step(_DiffuseSplitValue,max(0,dot(lightDirection,normalDirection)));
                float4 _LightMap_var = tex2D(_LightMap,TRANSFORM_TEX(i.uv0, _LightMap));
                float3 finalColor = emissive + (((node_7009*node_7331)+(node_7331*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(10,0,_SpecularRange)))*_SpecColor_copy.rgb*_SpecularPower*dot(_LightMap_var.rgb,float3(0.3,0.59,0.11))))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _Color_copy;
            uniform float _SpecularRange;
            uniform float4 _SpecColor_copy;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _AmbientPower;
            uniform float _DiffuseSplitValue;
            uniform float _SpecularPower;
            uniform sampler2D _LightMap; uniform float4 _LightMap_ST;
            uniform float4 _AmbientLightColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 node_7009 = (_Texture_var.rgb*_Color_copy.rgb); // Diffuse Color
                float node_7331 = step(_DiffuseSplitValue,max(0,dot(lightDirection,normalDirection)));
                float4 _LightMap_var = tex2D(_LightMap,TRANSFORM_TEX(i.uv0, _LightMap));
                float3 finalColor = (((node_7009*node_7331)+(node_7331*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(10,0,_SpecularRange)))*_SpecColor_copy.rgb*_SpecularPower*dot(_LightMap_var.rgb,float3(0.3,0.59,0.11))))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
