using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Custom Pipeline")]
public class CustomPipelineAsset : RenderPipelineAsset
{
	[SerializeField]
	bool useDynamicBatching = true, useGPUInstancing = true, useSRPBatcher = true;
	[SerializeField]
	ShadowSettings shadows = default;

    protected override RenderPipeline CreatePipeline()
	{
		return new CustomPipeline(useDynamicBatching, useGPUInstancing, useSRPBatcher, shadows);
	}
}
