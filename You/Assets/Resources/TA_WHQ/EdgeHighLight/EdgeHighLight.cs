using UnityEngine;

public class EdgeHighLight : MonoBehaviour {
	[SerializeField]
	private int edgeHighLightMaterialIndex;

	private Renderer meshRenderer;

	private Material materialInstance;

	private void Awake() {
		meshRenderer = GetComponent<Renderer>();
		Material[] ms = meshRenderer.materials;
		materialInstance = new Material(ms[edgeHighLightMaterialIndex]);
		ms[edgeHighLightMaterialIndex] = materialInstance;
		meshRenderer.materials = ms;
	}

	public void DisableEdgeHightLight() {
		materialInstance.SetFloat("_IsEnable", 0);
	}

	public void EnableEdgeHightLight() {
		materialInstance.SetFloat("_IsEnable", 1);
	}

}
