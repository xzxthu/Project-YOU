using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherCycleRunning : MonoBehaviour
{
    private CharacterController mCharacterController; // 物体的CharacterController组件

    private Transform mTransform; // 物体的Transform组件
    [SerializeField] private float mRightRunSpeed = 3.0f;
    [SerializeField] private float mLeftRunSpeed = 3.0f;
    private float weight = 2.0f;
    private float leftBoundary = -34.613f;
    private float rightBoundary = 0;

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
        mTransform = this.transform;
    }

    void RightRunning()
    {
        mCharacterController.Move(mTransform.forward * mRightRunSpeed * Time.deltaTime);
    }

    void LeftRunning()
    {
        if (mTransform.position.z >= -15.0f && mTransform.position.z <= 0)
        {
            mCharacterController.Move((-1) * mTransform.forward * mLeftRunSpeed * Time.deltaTime * 0.05f * weight);
        }
        else
        {
            mCharacterController.Move((-1) * mTransform.forward * mLeftRunSpeed * Time.deltaTime * 0.05f);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RightRunning();
        }
        LeftRunning();
    }

    private void LateUpdate()
    {
        float afterLimitZ = Mathf.Clamp(mTransform.position.z, leftBoundary, rightBoundary);
        mTransform.position = new Vector3(mTransform.position.x, mTransform.position.y, afterLimitZ);
    }
}