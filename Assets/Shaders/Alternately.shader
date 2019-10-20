// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33713,y:32466,varname:node_9361,prsc:2|custl-2407-OUT,alpha-1248-OUT;n:type:ShaderForge.SFN_DepthBlend,id:1942,x:32887,y:32955,varname:node_1942,prsc:2|DIST-6812-OUT;n:type:ShaderForge.SFN_Fresnel,id:7931,x:32701,y:32573,varname:node_7931,prsc:2;n:type:ShaderForge.SFN_Power,id:2362,x:33021,y:32721,varname:node_2362,prsc:2|VAL-7931-OUT,EXP-1611-OUT;n:type:ShaderForge.SFN_Exp,id:1611,x:32712,y:32732,varname:node_1611,prsc:2,et:0|IN-8618-OUT;n:type:ShaderForge.SFN_Slider,id:8618,x:32293,y:32809,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_8618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8547009,max:1;n:type:ShaderForge.SFN_Add,id:7387,x:33271,y:32848,varname:node_7387,prsc:2|A-2362-OUT,B-6721-OUT;n:type:ShaderForge.SFN_OneMinus,id:6721,x:33072,y:32955,varname:node_6721,prsc:2|IN-1942-OUT;n:type:ShaderForge.SFN_RemapRange,id:6812,x:32688,y:32938,varname:node_6812,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-8618-OUT;n:type:ShaderForge.SFN_Color,id:6556,x:33219,y:32558,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_6556,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.210532,c2:0.6229284,c3:0.8676471,c4:1;n:type:ShaderForge.SFN_Multiply,id:2407,x:33479,y:32706,varname:node_2407,prsc:2|A-6556-RGB,B-7387-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1248,x:33474,y:33002,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_1248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:8618-6556-1248;pass:END;sub:END;*/

Shader "Shader Forge/Alternately" {
    Properties {
        _Gloss ("Gloss", Range(0, 1)) = 0.8547009
        _MainColor ("MainColor", Color) = (0.210532,0.6229284,0.8676471,1)
        _Alpha ("Alpha", Float ) = 1
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
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _Gloss;
            uniform float4 _MainColor;
            uniform float _Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
                float3 finalColor = (_MainColor.rgb*(pow((1.0-max(0,dot(normalDirection, viewDirection))),exp(_Gloss))+(1.0 - saturate((sceneZ-partZ)/(_Gloss*-1.0+1.0)))));
                return fixed4(finalColor,_Alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
