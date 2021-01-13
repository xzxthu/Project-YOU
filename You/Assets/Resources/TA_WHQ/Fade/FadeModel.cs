using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeModel : MonoBehaviour
{
    
    private SkinnedMeshRenderer skin;

    //private float fadeTimer = 0;

    [SerializeField, Tooltip("模型淡出时间")]
    public float fadeTime = 2f;


    private Material m;

    // Start is called before the first frame update
    void Awake()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
        m = new Material(skin.material);
        skin.material = m;
    }

    private IEnumerator FadeOut()
    {
        float fadeTimer = 0;
        while (fadeTimer < fadeTime)
        {
            fadeTimer += Time.fixedDeltaTime;
            m.SetFloat("_FadeOutTimer", fadeTimer / fadeTime);
            yield return new WaitForFixedUpdate();
        }

    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void z()
    {
        m.SetFloat("_FadeOutTimer", 0);
    }
}
