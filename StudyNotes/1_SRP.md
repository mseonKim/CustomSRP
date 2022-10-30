# Custom SRP
이 튜토리얼에서는 URP에서 사용하고 있는 설정들을 참고로 하여 직접 SRP를 구현합니다.
단, URP의 ForwardRenderer처럼 ScriptableRenderer를 직접 상속하지 않고 pure C# Class(CameraRenderer)로 구현하였습니다.


### Render Pipeline
CustomPipeline.cs 에 구현

### Renderer
ScriptableRenderer가 아닌 pure c# class로 구현 (CameraRenderer.cs)
Opaque, Skybox, Transparent 순서로 Draw



## URP 특징
SRP는 로직이 Scriptable하기 때문에 로직에서 처리해야 할 데이터도 Scriptable해야 함.
High Quality 그래픽 처리를 위한 HDRP는 Pass간 의존성이 높아 DOD가 불가능하지만,
URP는 어느정도 조절 가능하기 때문에 DOD로 디자인됨.

URP라는 Render Pipeline에서 Renderer를 갖고 있음. (Forward, 2D, Deffered)
Renderer에서 사용할 Pass들을 구현.

- 장점
DOD이기 때문에 모듈화가 되어있어 실시간으로 바꿀 수 있음.
기존 Built-in RP에서는 숨겨져 있던 Render Pipeline을 직접 수정할 수 있음.
= RP의 구성을 어떻게, 어떤 순서로 구현할 것인지 등을 직접 수정 가능.


### URP Data
1. RenderPipelineAsset
RenderPipeline 클래스를 상속받은 URP. UniversalRenderPipelineAsset을 활용.
이 튜토리얼에서는 커스텀 Pipeline Asset을 만들어 Custom SRP를 구현.

2. RendererData
URP에서는 대표적으로 ForwardRenderData가 있고 여기서 어떤 Pass를 쓸 건지 구현되어 있음.

3. RenderObjectSettings
만든 Feature가 데이터를 필요할 경우 C# Serializable 데이터를 사용.
e.g. Forward Pass 경우 RenderObjectsPass의 데이터인 RenderObjectSettings.