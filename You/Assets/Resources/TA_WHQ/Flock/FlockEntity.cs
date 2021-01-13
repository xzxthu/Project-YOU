using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlockEntity : MonoBehaviour {

	public float moveSpeed = 0.5f;
	[SerializeField]
	private float rotateSpeed = 4.0f;
	[SerializeField]
	private float neighborDistance = 8f;
	bool turning = false;
	FlockManager flockManager;
	// Start is called before the first frame update
	void Start() {
		flockManager = transform.parent.GetComponent<FlockManager>();
		float ratio = transform.localScale.x;
		moveSpeed = Random.Range(flockManager.minSpeed * ratio, flockManager.maxSpeed * ratio);

	}

	// Update is called once per frame
	void Update() {
		if (Vector3.Distance(transform.position, flockManager.transform.position) >= flockManager.tankSize) {
			turning = true;
		} else {
			turning = false;
		}

		if (turning) {
			Vector3 direction = flockManager.transform.position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
		} else {
			if (Random.Range(0, 5) < 1) {
				ApplyRules();
			}
		}
		transform.Translate(0, 0, Time.deltaTime * moveSpeed);
	}
	private void ApplyRules() {
		List<GameObject> gos = flockManager.entityPool;
		Vector3 center = Vector3.zero;
		Vector3 avoid = Vector3.zero;
		float gSpeed = 0f;
		Vector3 goalPos = flockManager.globalPos;
		float dist;
		int groupSize = 0;
		foreach (var go in gos) {
			if (go != gameObject) {
				dist = Vector3.Distance(go.transform.position, transform.position);
				float threshold = neighborDistance * transform.localScale.x;
				if (dist <= threshold / 2) {
					center += go.transform.position;
					++groupSize;
					if (dist < transform.localScale.x * 2) {
						avoid += transform.position - go.transform.position;
					}
					gSpeed += go.GetComponent<FlockEntity>().moveSpeed;
				}
			}
		}

		if (groupSize > 0) {
			center = center / groupSize + (goalPos - transform.position);
			moveSpeed = gSpeed / groupSize;
			Vector3 direction = center + avoid - transform.position;
			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
			}
		}
	}
}
