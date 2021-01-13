using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherCycleRunning2 : MonoBehaviour
{
    private CharacterController mCharacterController; // 物体的CharacterController组件

    private Transform mTransform; // 物体的Transform组件
    private Rigidbody mRigidBody; // 物体的RigidBod组件
    [SerializeField] private float mRightRunSpeed = 10.0f; // 父亲往右跑
    [SerializeField] private float mLeftRunSpeed = 8.0f; // 父亲往左退
    private float weight = 2.0f; // 权重
    private float leftBoundary = -34.613f; // z的左边界
    private float rightBoundary = 0; // z的右边界

    private Vector3 mLastVelocity; // 上一帧的人物刚体速度 Debug用
    public Vector3 mVelocity; // 人物刚体速度 Debug用
    public float mAcceleration; // 人物的加速度 Debug用

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
        mTransform = this.transform;
        mRigidBody = GetComponent<Rigidbody>();
        mLastVelocity = Vector3.zero;
        // mRigidBody.velocity = (-1) * mTransform.forward; // 开始是反向的速度
    }

    void RightRunning()
    {
        // mCharacterController.Move(mTransform.forward * mRightRunSpeed * Time.deltaTime);
        mRigidBody.AddForce(mTransform.forward * mRightRunSpeed * 3);
    }

    void LeftRunning()
    {
        mRigidBody.AddForce((-1) * mTransform.forward * mLeftRunSpeed * 2);
        // if (mTransform.position.z - leftBoundary <= 1.0f)
        // {
        //     
        // }
        // else
        // {
        //     mRigidBody.AddForce((-1) * mTransform.forward * mLeftRunSpeed * 2);
        // }
        // 位置靠近右边时，降低移动速度
        if (mTransform.position.z >= -15.0f && mTransform.position.z <= 0)
        {
            
        }
        else
        {
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RightRunning();
        }
        else
        {
            LeftRunning();
        }
        mVelocity = mRigidBody.velocity;
        mAcceleration = (mVelocity - mLastVelocity).magnitude / Time.deltaTime;
        mLastVelocity = mVelocity; // 保留当前帧
    }

    private void LateUpdate()
    {
        float afterLimitZ = Mathf.Clamp(mTransform.position.z, leftBoundary, rightBoundary);
        mTransform.position = new Vector3(mTransform.position.x, mTransform.position.y, afterLimitZ);
    }
}