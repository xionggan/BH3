// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-3247-RGB,alpha-3247-A,clip-9823-R;n:type:ShaderForge.SFN_Tex2d,id:9823,x:32794,y:33071,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_9823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b7f59baeb0340994bb0830b83533e60a,ntxv:0,isnm:False|UVIN-6186-OUT;n:type:ShaderForge.SFN_Color,id:3247,x:32794,y:32891,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3247,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:0.5;n:type:ShaderForge.SFN_TexCoord,id:7937,x:32368,y:32958,varname:node_7937,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6186,x:32561,y:33072,varname:node_6186,prsc:2|A-7937-UVOUT,B-1478-OUT;n:type:ShaderForge.SFN_Slider,id:5520,x:31784,y:33215,ptovrint:False,ptlb:USpeed,ptin:_USpeed,varname:node_5520,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:2539,x:31784,y:33335,ptovrint:False,ptlb:VSpeed,ptin:_VSpeed,varname:node_2539,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.75,max:5;n:type:ShaderForge.SFN_Append,id:5711,x:32157,y:33215,varname:node_5711,prsc:2|A-5520-OUT,B-2539-OUT;n:type:ShaderForge.SFN_Multiply,id:1478,x:32368,y:33215,varname:node_1478,prsc:2|A-5711-OUT,B-1525-T;n:type:ShaderForge.SFN_Time,id:1525,x:32169,y:33425,varname:node_1525,prsc:2;proporder:9823-3247-5520-2539;pass:END;sub:END;*/

Shader "Shader Forge/UVPanner" {
    Properties {
        _Mask ("Mask", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,0.5)
        _USpeed ("USpeed", Range(0, 5)) = 0
        _VSpeed ("VSpeed", Range(0, 5)) = 0.75
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _Color;
            uniform float _USpeed;
            uniform float _VSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1525 = _Time;
                float2 node_6186 = (i.uv0+(float2(_USpeed,_VSpeed)*node_1525.g));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_6186, _Mask));
                clip(_Mask_var.r - 0.5);
////// Lighting:
                float3 finalColor = _Color.rgb;
                fixed4 finalRGBA = fixed4(finalColor,_Color.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _USpeed;
            uniform float _VSpeed;
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
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1525 = _Time;
                float2 node_6186 = (i.uv0+(float2(_USpeed,_VSpeed)*node_1525.g));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_6186, _Mask));
                clip(_Mask_var.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
