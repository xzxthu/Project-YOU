using System.Collections;
using UnityEngine;

namespace TA_WHQ {

	public struct SharingInformation {
		public static string rootDirectoryInResource {
			private set; get;
		} = "TA_WHQ/";
	}

	public class FadeEffect : MonoBehaviour {


		#region field
		private Material m_FadeInMaterial;
		private Material m_FadeOutMaterial;
		private string fadeInMaterialPath = SharingInformation.rootDirectoryInResource + "Fade/FadeInMaterial";
		private string fadeOutMaterialPath = SharingInformation.rootDirectoryInResource + "Fade/FadeOutMaterial";


		[Tooltip("淡入用时"), SerializeField]
		private float fadeInTime = 0.5f;

		[Tooltip("刷入用时"), SerializeField]
		private float brushTime = 0.5f;

		[Tooltip("淡出用时"), SerializeField]
		private float fadeOutTime = 1f;

		private bool isFading = false;

		[Tooltip("淡入的相对位置"), SerializeField]
		private Vector3 targetPosition = Vector3.zero;


		#endregion

		#region private methods
		private void Awake() {
			m_FadeInMaterial = new Material(Resources.Load<Material>(fadeInMaterialPath));
			m_FadeOutMaterial = new Material(Resources.Load<Material>(fadeOutMaterialPath));
		}
		
		/// <summary>
		/// 设置淡入阈值
		/// </summary>
		/// <param name="threshold">
		/// 阈值[0, 1]
		/// </param>
		private void SetThreshold(float threshold) {
			threshold = Mathf.Clamp(threshold, 0, 1);
			m_FadeInMaterial.SetFloat("_Threshold", threshold);
		}

		private void SetMaxDistance(float maxDistance) {
			m_FadeInMaterial.SetFloat("_MaxDistance", maxDistance);
		}


		private void SetTargetPosition(Vector3 targetPosition) {
			m_FadeInMaterial.SetVector("_TargetPosition", new Vector4(targetPosition.x, targetPosition.y, targetPosition.z, 1));
			SetMaxDistance(Vector3.Distance(targetPosition, transform.position + transform.localScale / 1.6f));
		}

		private IEnumerator FadeInCoroutine() {
			m_FadeInMaterial.SetInt("_IsFadingStage", 0);
			isFading = true;
			float timer = 0;
			float totalTime = brushTime + fadeInTime;
			while (timer < totalTime) {
				timer += Time.fixedDeltaTime;
				SetThreshold(timer / brushTime);
				if (timer > brushTime) {
					m_FadeInMaterial.SetInt("_IsFadingStage", 1);
					m_FadeInMaterial.SetFloat("_FadingTimer", (timer - brushTime) / fadeInTime);
				}
				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}
			isFading = false;
		}

		private IEnumerator FadeOutCoroutine() {
			float timer = fadeOutTime;
			isFading = true;
			while (timer > 0) {
				timer -= Time.fixedDeltaTime;
				m_FadeOutMaterial.SetFloat("_FadeOutTimer", timer / fadeOutTime);
				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}
			isFading = false;
		}

		private void SetMaterial(Material material) {
			GetComponent<MeshRenderer>().material = material;
		}
		#endregion

		#region public API

		/// <summary>
		/// 开始相对目标物体淡入，由近及远
		/// </summary>
		public void StartFadeInRelativeTo() {
			if (isFading)
				return;
			SetMaterial(m_FadeInMaterial);
			SetTargetPosition(targetPosition);
			StartCoroutine(FadeInCoroutine());
		}

		/// <summary>
		/// 开始淡出
		/// </summary>
		public void StartFadeOut() {
			if (isFading)
				return;
			SetMaterial(m_FadeOutMaterial);
			SetTargetPosition(targetPosition);
			StartCoroutine(FadeOutCoroutine());
		}

		/// <summary>
		/// 得到淡出时间
		/// </summary>
		public float GetFadeOutTime()
        {
			return fadeOutTime;
        }

        #endregion


    }

	
}

