using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontEmission : MonoBehaviour {

	Text text;
	Material material;

	void Awake() {
		text = GetComponent<Text>();
		material = new Material(text.material);
		text.material = material;
	}

	public void EnableEmission() {
		material.SetFloat("_EmissionEnable", 1);
	}

	public void DisableEmission() {
		material.SetFloat("_EmissionEnable", 0);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.F1)) {
			EnableEmission();
		}
		if (Input.GetKeyDown(KeyCode.F2)) {
			DisableEmission();
		}
	}
}
