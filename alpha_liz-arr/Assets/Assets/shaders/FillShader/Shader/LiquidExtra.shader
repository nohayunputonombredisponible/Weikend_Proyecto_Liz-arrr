Shader "Unlit/SpecialFX/Liquid Extra"
{
    Properties
    {
		_Tint ("Tint", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("NoiseTexture", 2D) = "white" {}
		_noiseScale ("Noise Scale", Range(-10,10)) = 0.0
		_texScale ("Tex Scale", Range(-10,10)) = 0.0
        _FillAmount ("Fill Amount", Range(-10,10)) = 0.0
		[HideInInspector] _WobbleX ("WobbleX", Range(-1,1)) = 0.0
		[HideInInspector] _WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
        _TopColor ("Top Color", Color) = (1,1,1,1)
		_FoamColor ("Foam Line Color", Color) = (1,1,1,1)
        _Rim ("Foam Line Width", Range(0,0.1)) = 0.0    
		_RimColor ("Rim Color", Color) = (1,1,1,1)
	    _RimPower ("Rim Power", Range(0,10)) = 0.0
    }
 
    SubShader
    {
        Tags {"Queue"="Geometry"  "DisableBatching" = "True" }
  
                Pass
        {
		 Zwrite On
		 Cull Off // we want the front and back faces
		 AlphaToMask On // transparency

         CGPROGRAM


         #pragma vertex vert
         #pragma fragment frag
         // make fog work
         #pragma multi_compile_fog
           
         #include "UnityCG.cginc"
 
         struct appdata
         {
           float4 vertex : POSITION;
           float2 uv : TEXCOORD0;
		   float3 normal : NORMAL;	
         };
 
         struct v2f
         {
            float2 uv : TEXCOORD0;
            UNITY_FOG_COORDS(1)
            float4 vertex : SV_POSITION;
			float3 viewDir : COLOR;
		    float3 normal : COLOR2;	
			float3 worldPos : COLOR3;
			float fillEdge : TEXCOORD2;
			float3 worldNormal : TEXCOORD3;
         };
 
         sampler2D _MainTex, _NoiseTex;
         float4 _MainTex_ST;
         float _FillAmount, _WobbleX, _WobbleZ;
         float4 _TopColor, _RimColor, _FoamColor, _Tint;
         float _Rim, _RimPower,_noiseScale, _texScale;
           
		 float4 RotateAroundYInDegrees (float4 vertex, float degrees)
         {
			float alpha = degrees * UNITY_PI / 180;
			float sina, cosa;
			sincos(alpha, sina, cosa);
			float2x2 m = float2x2(cosa, sina, -sina, cosa);
			return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;				
         }
      

         v2f vert (appdata v)
         {
            v2f o;

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);			
			// get world position of the vertex
            float3 worldPos = mul (unity_ObjectToWorld, v.vertex.xyz);   
			// rotate it around XY
			float3 worldPosX= RotateAroundYInDegrees(float4(worldPos,0),360);
			// rotate around XZ
			float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);		
			// combine rotations with worldPos, based on sine wave from script
			float3 worldPosAdjusted = worldPos + (worldPosX  * _WobbleX)+ (worldPosZ* _WobbleZ); 
			// how high up the liquid is
			o.fillEdge =  worldPosAdjusted.y +  _FillAmount;
			o.worldPos = worldPosAdjusted;
			o.worldNormal = normalize( mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz);
			o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
            o.normal = v.normal;
            return o;
         }
           
         fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
         {
           // sample the texture
          
		  
		  float3 projNormal = saturate(pow(i.worldNormal * 1.1, 4));
           // apply fog
		  
           UNITY_APPLY_FOG(i.fogCoord, col);
		  // worldspace noise and texture
		   fixed4 col = tex2D(_MainTex,i.worldPos.xy * _texScale) * _Tint;
		   fixed4 col2 = tex2D(_MainTex,i.worldPos.yz * _texScale) * _Tint;
		    col = lerp(col, col2, projNormal.x);
		   fixed4 noise2 = tex2D(_NoiseTex, i.worldPos.yz * _noiseScale);
		   fixed4 noise = tex2D(_NoiseTex, i.worldPos.xy * _noiseScale);
		   

		   noise = lerp(noise, noise2, projNormal.x);
		   float4 noiseWobble = lerp(noise * _WobbleX, noise2 * _WobbleZ, projNormal.x);
		   // rim light
		   float dotProduct = 1 - pow(dot(i.normal, i.viewDir ), _RimPower);
           float4 RimResult = smoothstep(0.5, 1.0, dotProduct);
           RimResult *= _RimColor;
		 
		   // foam edge with noise
		   float4 foam = ( step(i.fillEdge  , 0.5 + noiseWobble.r) - step(i.fillEdge, (0.5 - _Rim+ noiseWobble.r )))  ;
           float4 foamColored = foam * (_FoamColor * 0.9);
		   // rest of the liquid with noise
		   float4 result = step(i.fillEdge, 0.5+ noiseWobble.r) - foam;
           float4 resultColored = result * col;
		   // both together, with the texture
           float4 finalResult = resultColored + foamColored;				
		   finalResult.rgb += RimResult;

		   // color of backfaces/ top
		   float4 topColor = _TopColor * (foam + result);
		   
		   //VFACE returns positive for front facing, negative for backfacing
		   return facing > 0 ? finalResult: topColor;
               
         }
         ENDCG
        }
 
    }
}