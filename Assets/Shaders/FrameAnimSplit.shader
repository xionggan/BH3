// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-7872-RGB;n:type:ShaderForge.SFN_Tex2d,id:7872,x:32949,y:32955,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_7872,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e085922606e4f314193eccf05dd23bb0,ntxv:0,isnm:False|UVIN-4160-UVOUT;n:type:ShaderForge.SFN_UVTile,id:4160,x:32718,y:32955,varname:node_4160,prsc:2|UVIN-1660-OUT,WDT-876-X,HGT-876-Y,TILE-161-OUT;n:type:ShaderForge.SFN_Vector4Property,id:876,x:32470,y:32955,ptovrint:False,ptlb:Width&Height,ptin:_WidthHeight,varname:node_876,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2,v2:4,v3:0,v4:0;n:type:ShaderForge.SFN_TexCoord,id:3049,x:32296,y:32795,varname:node_3049,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:714,x:31782,y:33156,varname:node_714,prsc:2;n:type:ShaderForge.SFN_Fmod,id:1222,x:32298,y:33156,varname:node_1222,prsc:2|A-9116-OUT,B-9697-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9697,x:32137,y:33349,ptovrint:False,ptlb:FrameCount,ptin:_FrameCount,varname:node_9697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Slider,id:1810,x:31649,y:33340,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1810,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Multiply,id:8277,x:31978,y:33156,varname:node_8277,prsc:2|A-714-TTR,B-1810-OUT;n:type:ShaderForge.SFN_Trunc,id:9116,x:32137,y:33156,varname:node_9116,prsc:2|IN-8277-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9643,x:32298,y:33349,ptovrint:False,ptlb:StartFrameIndex,ptin:_StartFrameIndex,varname:node_9643,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:161,x:32470,y:33156,varname:node_161,prsc:2|A-1222-OUT,B-9643-OUT;n:type:ShaderForge.SFN_Multiply,id:1660,x:32470,y:32795,varname:node_1660,prsc:2|A-9539-OUT,B-3049-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9539,x:32296,y:32724,ptovrint:False,ptlb:UVScale,ptin:_UVScale,varname:node_9539,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7872-876-9697-1810-9643-9539;pass:END;sub:END;*/

Shader "Shader Forge/FrameAnimSplit" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _WidthHeight ("Width&Height", Vector) = (2,4,0,0)
        _FrameCount ("FrameCount", Float ) = 4
        _Speed ("Speed", Range(0, 5)) = 2.5
        _StartFrameIndex ("StartFrameIndex", Float ) = 0
        _UVScale ("UVScale", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float4 _WidthHeight;
            uniform float _FrameCount;
            uniform float _Speed;
            uniform float _StartFrameIndex;
            uniform float _UVScale;
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
////// Lighting:
                float4 node_714 = _Time;
                float node_161 = (fmod(trunc((node_714.a*_Speed)),_FrameCount)+_StartFrameIndex);
                float2 node_4160_tc_rcp = float2(1.0,1.0)/float2( _WidthHeight.r, _WidthHeight.g );
                float node_4160_ty = floor(node_161 * node_4160_tc_rcp.x);
                float node_4160_tx = node_161 - _WidthHeight.r * node_4160_ty;
                float2 node_4160 = ((_UVScale*i.uv0) + float2(node_4160_tx, node_4160_ty)) * node_4160_tc_rcp;
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_4160, _MainTexture));
                float3 finalColor = _MainTexture_var.rgb;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
