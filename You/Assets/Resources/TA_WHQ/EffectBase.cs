using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TA_WHQ {
    public class EffectBase : MonoBehaviour {
		protected static readonly string rootDirectoryInResource  = "TA_WHQ/";

		[Tooltip("相应材质在网格组件中的索引，单材质网格默认为0，若单脚本控制多个材质则忽略此属性"), SerializeField]
		protected int targetMaterialIndex = 0;
		public Material InitMaterialInstance(Transform host, Material material, int materialIndex = 0) {
			Material materialInstance = new Material(material);
			Material[] materials = host.GetComponent<Renderer>().materials;
			materials[materialIndex] = materialInstance;
			host.GetComponent<Renderer>().materials = materials;
			return materialInstance;
		}

		public T ResourcesLoad<T>(string path) where T:Object {
			T resource = Resources.Load<T>(path);
			if (resource) {
				return resource;
			}
			Debug.LogError("资源路径加载失败: " + path + " 挂载脚本的物体：" + transform.name);
			return null;
		}


	}
}

