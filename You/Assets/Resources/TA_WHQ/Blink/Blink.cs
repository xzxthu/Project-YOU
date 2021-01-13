using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	private GameObject m_Curtain;
	private Material m_BlinkMaterial;

	private void Start() {
		m_Curtain = transform.GetChild(0).gameObject;
		m_BlinkMaterial = new Material(m_Curtain.GetComponent<Image>().material);
		m_Curtain.GetComponent<Image>().material = m_BlinkMaterial;
		SetCurtainFocus(1, 1);
	}

	/// <summary>
	/// 设置椭圆视野的焦点
	/// </summary>
	/// <param name="focusOnAxisX">
	/// 视野在x轴的焦点[0, 1]
	/// </param>
	/// <param name="focusOnAxisY">
	/// 视野在y轴的焦点[0, 1]
	/// </param>
	public void SetCurtainFocus(float focusOnAxisX, float focusOnAxisY) {
		
		if (focusOnAxisX < 0)
			focusOnAxisX = 0;
		if (focusOnAxisX > 1)
			focusOnAxisX = 1;
		if (focusOnAxisY < 0)
			focusOnAxisY = 0;
		if (focusOnAxisY > 1)
			focusOnAxisY = 1;

		//m_Curtain.SetActive(!(Mathf.Approximately(1, focusOnAxisX) && Mathf.Approximately(1, focusOnAxisY)));
		m_BlinkMaterial.SetFloat("_FocusOnAxisX", focusOnAxisX);
		m_BlinkMaterial.SetFloat("_FocusOnAxisY", focusOnAxisY);
		
	}

}
