# Lighting
URP의 BRDF 모델을 참고하여 구현.
Lambert Diffuse + Minimalist CookTorrance Specular

Assets/CustomSRP/ShaderLibrary 폴더에 구현하였습니다.
(BRDF.hlsl, Surface.hlsl, Light.hlsl, Lighting.hlsl)

최대 4개 Directional Light 지원.


## BRDF
= Bidirectional Reflectance Distribution Function (반사의 흩어짐 분포)
Opaque 표면에 어떻게 빛이 반사될지 결정하는 Function.

<img width = "400" src="Images_jpg/3_BSDF.png">


BSDF: 빛이 어떤 물체에 부딪쳤을 때 얼마나 많은 빛이 반사되는가를 나타냄.
BSDF = BRDF + BTDF
BRDF: BSDF에서 반사되는 부분
BTDF: BSDF에서 투과되는 부분

BRDF는
1. 반사율 분포를 출력
2. 양방향성 (빛이 들어오는 입사 방향과 반사되는 방향이 바뀌어도 값이 변경되지 않음)
3. 광원과 관찰자를 입력 파라미터로 받음
4. 같은 입력에는 항상 하나의 같은 결과가 나오는 함수


## BRDF Models
Diffuse: Lambert, Oren-Nayer
Specular: Phong, Blinn-Phong
등등 많은 BRDF 모델이 존재함.