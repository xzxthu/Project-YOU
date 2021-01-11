// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "myShader3"{
		Properties{
			 _Color("Color", Color) = (1,1,1,1)
		}	
		SubShader
		{
				Pass{
		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members temp)
						#pragma vertex vert
						#pragma fragment frag
						struct u2v{
							float4 u2vVertex : POSITION; // 告诉unity 把 模型空间下的顶点坐标填充给u2vVertex
							float3 normal : NORMAL; // 告诉unity 把 模型空间下的法线方向填充给normal
							float3 tangent : TANGENT0;
							//float4 texcoord0 : TEXCOORD0; // 告诉unity 把 第一套纹理坐标填充给texcoord
						};
						struct v2f{
							float4 position : SV_POSITION;
							float3 temp : COLOR0; // 每个顶点COLOR0  中间的点 进行 插值运算 保持颜色
						};
						//float4 vert(u2v v : POSITION) : SV_POSITION{ 
						//	return UnityObjectToClipPos(v.u2vVertex);
						//}
						v2f vert(u2v v){
							v2f f;
							f.position = UnityObjectToClipPos(v.u2vVertex);
							f.temp = v.normal + (0.8,0.8,0.8);
							return f;
						}

						// 标准光照模型 就是一个公式 用来计算某个点的光照效果
						// 在标准光照模型中 我们把摄像机的光分为以下四个部分
						// 自发光
						// Blinn-Phong 光照模型 Specular = 直射光 * pow(max(cosθ, 0), 10) θ:是法线和x的夹角  x 是 平行光和视野方向的平分线
						// 高光反射 Specular = 直射光 * pow(max(cosθ, 0), 高光的参数x) θ:是反射光方向与视野方向的夹角 某一片区域的光照 与 摄像机的视野有关
						// 高光的参数越小 返回越大
						// 漫反射 Diffuse = 直射光颜色 * max(0, cos夹角(光和法线的夹角))  
						// 环境光
		
						fixed4 frag(v2f f) : SV_Target{
							return fixed4(f.temp, 1);
						}
		ENDCG
				}
			}
	FallBack "VertexLit"
}