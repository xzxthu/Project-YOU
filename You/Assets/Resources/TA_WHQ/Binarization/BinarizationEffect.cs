using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TA_WHQ {
    public class BinarizationEffect : EffectBase {
        
        [Tooltip("相对此坐标进行光照二值化")]
        private Vector3 targetPosition = Vector3.zero;

		private readonly string binarizationMaterialPath = rootDirectoryInResource + "Binarization/BinarizationMaterial";

		private Material binarizationMaterial;

		private void Awake() {
			binarizationMaterial = ResourcesLoad<Material>(binarizationMaterialPath);
			binarizationMaterial = InitMaterialInstance(transform, binarizationMaterial, targetMaterialIndex);
		}

	}

}
