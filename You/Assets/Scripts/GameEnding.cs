using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    bool m_IsPlayerAtExit;
    float m_Timer;

    public CanvasGroup exitBackgroundImageCanvasGroup;


    bool m_IsPlayerCaught;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    // Start is called before the first frame update

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_hasPlayAudio;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject==player)
        {
            m_IsPlayerAtExit = true;
            Debug.Log("You Win!!");
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    void Update()
    {
        if(m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false,exitAudio);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true,caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup,bool doRestart,AudioSource audioSource)
    {
        if(!m_hasPlayAudio)
        {
            audioSource.Play();
            m_hasPlayAudio = true;
        }
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if(m_Timer>fadeDuration+displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene("UIScense");
            }
            else
            {
                SceneManager.LoadScene("UIScense");
                //Application.Quit();
            }

        }
    }
}
