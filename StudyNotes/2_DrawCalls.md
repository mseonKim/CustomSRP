참고한 튜토리얼: https://catlikecoding.com/unity/tutorials/custom-srp/draw-calls/

튜토리얼과 달리 GPU 인스턴싱 테스트 용으로 오브젝트(구) 생성을 위해 별도의 스크립트인 ObjectCreator.cs를 사용하였습니다. Color의 경우 색의 종류가 이 튜토리얼에서 중요한 부분이 아니라고 판단하여 정해진 6가지의 색만 아래처럼 할당해서 배치하였습니다.


렌더파이프라인에서 SRP Batcher, GPU Instancing, Dynamic Batching 사용 여부는 튜토리얼처럼 Pipeline Asset에서 조작하여 테스트해볼 수 있게끔 하였습니다.


테스트는 Assets/CustomSRP/CustomSRP.unity scene 파일로 가능합니다.


자세한 문서 링크: https://docs.google.com/document/d/1gT8lDt80lxTy4SNMzFtZ26z1rbMLJpcLOy5ZccoWqZw/edit