using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

	public int entityCount = 30;

	[HideInInspector]
	public List<GameObject> entityPool = new List<GameObject>();

	public float tankSize = 15;

	public GameObject entityPrefab;

	[HideInInspector]
	public Vector3 globalPos = Vector3.zero;

	[Tooltip("个体移动最小速度")]
	public float minSpeed = 1.5f;

	[Tooltip("个体移动最大速度")]
	public float maxSpeed = 3f;

	// Start is called before the first frame update
	void Start() {
		for (int i = 0; i < entityCount; ++i) {
			float x = Random.Range(-tankSize, tankSize);
			float y = Random.Range(-tankSize, tankSize);
			float z = Random.Range(-tankSize, tankSize);
			entityPool.Add(Instantiate(entityPrefab, new Vector3(x, y, z) + transform.position, Quaternion.identity, transform));
		}
	}

	// Update is called once per frame
	void Update() {
		if (Random.Range(0, 10000) < 50) {
			globalPos = transform.position + new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
		}
	}

}
