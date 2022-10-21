using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
    private static ShaderTagId litShaderTagId = new ShaderTagId("CustomLit");

    private ScriptableRenderContext context;
	private Camera camera;
    private const string bufferName = "Render Camera";
	private CommandBuffer buffer = new CommandBuffer { name = bufferName };
    private CullingResults cullingResults;
    private Lighting lighting = new Lighting();

	public void Render(ScriptableRenderContext context, Camera camera, bool useDynamicBatching, bool useGPUInstancing, ShadowSettings shadowSettings)
    {
		this.context = context;
		this.camera = camera;

        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull(shadowSettings.maxDistance))
        {
            return;
        }

        buffer.BeginSample(SampleName);
        ExecuteBuffer();
        lighting.Setup(context, cullingResults, shadowSettings);
        buffer.EndSample(SampleName);
        Setup();
        DrawVisibleGeometry(useDynamicBatching, useGPUInstancing);
        DrawUnsupportedShaders();
        DrawGizmos();
        lighting.Cleanup();
        Submit();
	}

    private void Setup()
    {
        context.SetupCameraProperties(camera);
        CameraClearFlags flags = camera.clearFlags;
        bool isColorFlag = (flags == CameraClearFlags.Color);
        buffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, isColorFlag, isColorFlag ? camera.backgroundColor.linear : Color.clear    );
        buffer.BeginSample(SampleName);
        ExecuteBuffer();
    }

    private void DrawVisibleGeometry(bool useDynamicBatching, bool useGPUInstancing)
    {
        // Draw Opaque objects first
		var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        var sortingSettings = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings)
                            {
                                enableDynamicBatching = useDynamicBatching,
                                enableInstancing = useGPUInstancing
                            };
        drawingSettings.SetShaderPassName(1, litShaderTagId);
		context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        // Draw Skybox
        context.DrawSkybox(camera);

        // Draw Transparent objects
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
		context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }

    private void Submit()
    {
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }

    private void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    private bool Cull(float maxShadowDistance)
    {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters parameters))
        {
            parameters.shadowDistance = Mathf.Min(maxShadowDistance, camera.farClipPlane);
            cullingResults = context.Cull(ref parameters); 
            return true;
        }
        return false;
    }
}
