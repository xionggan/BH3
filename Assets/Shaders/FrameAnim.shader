// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-1925-RGB;n:type:ShaderForge.SFN_Tex2d,id:1925,x:32661,y:32721,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_1925,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2ee0a0f5dbdc4f043b8c15a81781d9cc,ntxv:0,isnm:False|UVIN-5607-UVOUT;n:type:ShaderForge.SFN_UVTile,id:5607,x:32398,y:32716,varname:node_5607,prsc:2|UVIN-3817-OUT,WDT-1161-OUT,HGT-9018-OUT,TILE-4249-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1161,x:32110,y:32734,ptovrint:False,ptlb:Width,ptin:_Width,varname:node_1161,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:9018,x:32110,y:32817,ptovrint:False,ptlb:Height,ptin:_Height,varname:node_9018,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Slider,id:7033,x:31547,y:33204,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_7033,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:5;n:type:ShaderForge.SFN_Time,id:6813,x:31704,y:33047,varname:node_6813,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4437,x:31893,y:33047,varname:node_4437,prsc:2|A-6813-TTR,B-7033-OUT;n:type:ShaderForge.SFN_Trunc,id:4249,x:32296,y:33045,varname:node_4249,prsc:2|IN-7276-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7276,x:32117,y:33045,ptovrint:False,ptlb:IsAotuPlay,ptin:_IsAotuPlay,varname:node_7276,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-8382-OUT,B-4437-OUT;n:type:ShaderForge.SFN_Slider,id:8382,x:31783,y:32940,ptovrint:False,ptlb:CurFrame,ptin:_CurFrame,varname:node_8382,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:48;n:type:ShaderForge.SFN_TexCoord,id:252,x:31937,y:32517,varname:node_252,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:3817,x:32134,y:32517,varname:node_3817,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-252-UVOUT;proporder:1925-1161-9018-7033-7276-8382;pass:END;sub:END;*/

Shader "Shader Forge/FrameAnim" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Width ("Width", Float ) = 2
        _Height ("Height", Float ) = 2
        _Speed ("Speed", Range(0, 5)) = 2
        [MaterialToggle] _IsAotuPlay ("IsAotuPlay", Float ) = 0
        _CurFrame ("CurFrame", Range(0, 48)) = 0
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
            Blend One One
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
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Width;
            uniform float _Height;
            uniform float _Speed;
            uniform fixed _IsAotuPlay;
            uniform float _CurFrame;
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
                float4 node_6813 = _Time;
                float node_4249 = trunc(lerp( _CurFrame, (node_6813.a*_Speed), _IsAotuPlay ));
                float2 node_5607_tc_rcp = float2(1.0,1.0)/float2( _Width, _Height );
                float node_5607_ty = floor(node_4249 * node_5607_tc_rcp.x);
                float node_5607_tx = node_4249 - _Width * node_5607_ty;
                float2 node_5607 = ((i.uv0*2.0+0.0) + float2(node_5607_tx, node_5607_ty)) * node_5607_tc_rcp;
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_5607, _MainTexture));
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
