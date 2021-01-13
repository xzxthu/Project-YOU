using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{

    private PlayableDirector director = null;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        if(director==null)
        {
            director = gameObject.GetComponent<PlayableDirector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Time is: " + time);
        Debug.Log("director.duration is: " + director.duration);
        QuitLevel();
    }

    void QuitLevel()
    {
        time += Time.deltaTime;
        if(time>=director.duration)
        {
            time = 0;
            SceneManager.LoadScene(0);
        }
    }
}
