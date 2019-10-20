// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:1,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-6843-OUT,custl-8289-RGB,clip-7836-OUT;n:type:ShaderForge.SFN_Tex2d,id:5710,x:32333,y:33541,ptovrint:False,ptlb:MainMask,ptin:_MainMask,varname:node_5710,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8289,x:32760,y:32962,ptovrint:False,ptlb:Main,ptin:_Main,varname:_node_1228_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1746,x:32489,y:33110,ptovrint:False,ptlb:SubMask,ptin:_SubMask,varname:node_1746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4454-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7836,x:32760,y:33147,varname:node_7836,prsc:2|A-1746-R,B-9042-OUT;n:type:ShaderForge.SFN_Slider,id:9971,x:32617,y:32813,ptovrint:False,ptlb:SpecularPower,ptin:_SpecularPower,varname:node_9971,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8547009,max:10;n:type:ShaderForge.SFN_Multiply,id:6843,x:33001,y:32812,varname:node_6843,prsc:2|A-9971-OUT,B-8289-RGB;n:type:ShaderForge.SFN_TexCoord,id:7876,x:32155,y:33110,varname:node_7876,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:4454,x:32333,y:33110,varname:node_4454,prsc:2,spu:0,spv:-1|UVIN-7876-UVOUT;n:type:ShaderForge.SFN_Time,id:3141,x:32007,y:33349,varname:node_3141,prsc:2;n:type:ShaderForge.SFN_Sin,id:734,x:32175,y:33349,varname:node_734,prsc:2|IN-3141-T;n:type:ShaderForge.SFN_RemapRange,id:2300,x:32333,y:33349,varname:node_2300,prsc:2,frmn:-1,frmx:1,tomn:0.9,tomx:1.1|IN-734-OUT;n:type:ShaderForge.SFN_Multiply,id:9042,x:32489,y:33349,varname:node_9042,prsc:2|A-2300-OUT,B-5710-R;proporder:5710-8289-1746-9971;pass:END;sub:END;*/

Shader "Shader Forge/BladeLight" {
    Properties {
        _MainMask ("MainMask", 2D) = "white" {}
        _Main ("Main", 2D) = "white" {}
        _SubMask ("SubMask", 2D) = "white" {}
        _SpecularPower ("SpecularPower", Range(0, 10)) = 0.8547009
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
            "DisableBatching"="True"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _MainMask; uniform float4 _MainMask_ST;
            uniform sampler2D _Main; uniform float4 _Main_ST;
            uniform sampler2D _SubMask; uniform float4 _SubMask_ST;
            uniform float _SpecularPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 node_7543 = _Time;
                float2 node_4454 = (i.uv0+node_7543.g*float2(0,-1));
                float4 _SubMask_var = tex2D(_SubMask,TRANSFORM_TEX(node_4454, _SubMask));
                float4 node_3141 = _Time;
                float4 _MainMask_var = tex2D(_MainMask,TRANSFORM_TEX(i.uv0, _MainMask));
                clip((_SubMask_var.r*((sin(node_3141.g)*0.1+1.0)*_MainMask_var.r)) - 0.5);
////// Lighting:
////// Emissive:
                float4 _Main_var = tex2D(_Main,TRANSFORM_TEX(i.uv0, _Main));
                float3 emissive = (_SpecularPower*_Main_var.rgb);
                float3 finalColor = emissive + _Main_var.rgb;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _MainMask; uniform float4 _MainMask_ST;
            uniform sampler2D _SubMask; uniform float4 _SubMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 node_5504 = _Time;
                float2 node_4454 = (i.uv0+node_5504.g*float2(0,-1));
                float4 _SubMask_var = tex2D(_SubMask,TRANSFORM_TEX(node_4454, _SubMask));
                float4 node_3141 = _Time;
                float4 _MainMask_var = tex2D(_MainMask,TRANSFORM_TEX(i.uv0, _MainMask));
                clip((_SubMask_var.r*((sin(node_3141.g)*0.1+1.0)*_MainMask_var.r)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
