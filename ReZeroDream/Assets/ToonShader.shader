Shader "Unlit/ToonShader"
{
    Properties
    {
        _Albedo("Albedo", Color) = (1,1,1,1)
        _Shades("Shades", Range(1,20)) = 3

        _InkColor("InkColor", Color) = (0,0,0,0)
        _InkSize("InkSize", float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _Albedo;
            float _Shades;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                //1. 오브젝트의 법선벡터를 월드 좌표로 변환
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //2. 조명 계산. 법선벡터와 빛 사이 각도 구하기(cosine)
                // 월드공간의 빛 방향은 _WorldSpaceLightPos0에 저장되고,
                // 월드좌표의 노멀은 i.worldNormal에 저장된다
                // => 두 좌표 정규화 & 내적 계산
                float cosineAngle = dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz));
                
                cosineAngle = max(cosineAngle, 0.0);

                cosineAngle = floor(cosineAngle * _Shades) / _Shades;

                return _Albedo* cosineAngle;
            }
            ENDCG
        }


         Pass
        {
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _InkColor;
            float _InkSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + _InkSize*v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                return _InkColor;
            }
            ENDCG
        }

    }

    Fallback "VertexLit"
}
