using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBlur : MonoBehaviour {

	private readonly string motionBlurMaterialPath = "TA_WHQ/PostProcess/MotionBlurMaterial";

	private Material motionBlurMaterial;

	private Matrix4x4 preVP;

	private Matrix4x4 currentVP;

	private bool isFirstFrame = true;

	private void Awake() {
		motionBlurMaterial = Resources.Load<Material>(motionBlurMaterialPath);
		Camera camera = GetComponent<Camera>();
		camera.depthTextureMode |= DepthTextureMode.Depth;

	}
    private void Update()
    {
		UpdateParams();
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
		Graphics.Blit(source, destination, motionBlurMaterial);
	}

	public void UpdateParams() {
		currentVP = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
		motionBlurMaterial.SetMatrix("_CurrentVPInverse", currentVP.inverse);
		if (isFirstFrame) {
			isFirstFrame = false;
			motionBlurMaterial.SetMatrix("_PreVP", currentVP);
			motionBlurMaterial.SetMatrix("_PreVPInverse", currentVP.inverse);

		} else {
			motionBlurMaterial.SetMatrix("_PreVP", preVP);
			motionBlurMaterial.SetMatrix("_PreVPInverse", preVP.inverse);
		}
		preVP = currentVP;
		

	}
}
