using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    private float fps = 29.0f;
    private float time;
    private float time1;
    float waitTime = 3.0f;

    public Sprite[] animations;
    private Texture2D[] maps;
    private int nowFram;

    AsyncOperation async;

    int progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        maps = new Texture2D[animations.Length];
        Trasition();
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update()
    {
        //progress = (int)(async.progress * 100);
        //progress = (int)(time1 / (waitTime*2) *100);
    }

    IEnumerator loadScene()
    {

        yield return new WaitForSeconds(waitTime);
        async = SceneManager.LoadSceneAsync(Globe.LoadName);
        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                progress = (int)async.progress *100;
            }
            else
                progress = 100;

            if(progress>0.9f)
            {
                if (Input.anyKeyDown)
                {

                    async.allowSceneActivation = true;
                }
 
            }
            yield return null;
        }


    }
    private void OnGUI()
    {
        DrawAnimation(maps);
        Finished();
    }

    void DrawAnimation(Texture2D[] tex)
    {
        time += Time.deltaTime;
        time1 += Time.deltaTime;
        if(time>=1.0/fps)
        {
            nowFram++;
            time = 0;
            if (nowFram >= tex.Length)
                nowFram = 0;
        }

        GUI.DrawTexture(new Rect(100, 100, 40, 60), tex[nowFram]);
        GUI.Label(new Rect(100, 180, 300, 60), "Loading!!!" + progress);
    }

    void Finished()
    {
        if(progress>0.9f)
        {
            GUI.Label(new Rect(100, 500, 300, 60), "按任意键继续");
        }
    }

    void Trasition()
    {
        for(int i=0;i<maps.Length;i++)
        {
            maps[i] = new Texture2D((int)animations[i].rect.width, (int)animations[i].rect.height);
            var pixels = animations[i].texture.GetPixels((int)animations[i].rect.x,
                                                       (int)animations[i].rect.y,
                                                       (int)animations[i].rect.width,
                                                       (int)animations[i].rect.height);
            maps[i].SetPixels(pixels);
            maps[i].Apply();
        }
    }
}
