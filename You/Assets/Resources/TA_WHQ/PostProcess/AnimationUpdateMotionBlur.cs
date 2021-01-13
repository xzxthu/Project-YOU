using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUpdateMotionBlur : StateMachineBehaviour
{
    private MotionBlur motionBlur;

    private void Awake()
    {
        motionBlur = Camera.main.GetComponent<MotionBlur>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateMotionBlur();

    }

    private void UpdateMotionBlur()
    {
        //a Debug.Log(1);
        if (motionBlur)
        {
            motionBlur.UpdateParams();
        }
        else
        {
            Debug.LogError("motionBlur字段空引用");
        }
    }
}
