using UnityEngine;

public class WaterTest : MonoBehaviour {

	private MeshFilter meshRenderer;

	private Mesh mesh;

	private Vector3[] localPositions;

	[SerializeField]
	private float amplitude = 0.001f;

	[SerializeField]
	private float speed = 1;

	private void Start() {
		meshRenderer = GetComponent<MeshFilter>();
		mesh = meshRenderer.mesh;
		localPositions = mesh.vertices;
	}

	private float Hash(Vector2 seed) {
		float h = Mathf.Sin(Vector2.Dot(seed, new Vector2(12.9898f, 78.233f))) * 433758.5453f;
		return Mathf.Abs(h - Mathf.Floor(h));
	}

	private void Update() {
		Vector3[] positions = localPositions;
		for (int i = 0; i < localPositions.Length; ++i) {
			float offset = speed * Mathf.Cos(Time.unscaledTime) * Hash(new Vector2(positions[i].x, positions[i].z)) * amplitude;
			positions[i].y += offset;
		}
		mesh.SetVertices(localPositions);
	}



}
