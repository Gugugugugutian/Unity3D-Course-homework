using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, -2, -5); // 相机与玩家的偏移
    public float rotationSpeed = 2.0f;

    private Transform cameraTransform;

    private float currentRotationX = 0;

    void Start()
    {
        cameraTransform = transform;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // 计算相机的旋转
        float playerRotationY = player.eulerAngles.y;
        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;

        // 设置相机的旋转
        Quaternion rotation = Quaternion.Euler(0, currentRotationX, 0);
        Vector3 targetPosition = player.position - rotation * offset;
        cameraTransform.position = targetPosition;
        cameraTransform.LookAt(player);
    }
}
