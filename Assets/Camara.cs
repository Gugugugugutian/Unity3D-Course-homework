using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, -2, -5); // �������ҵ�ƫ��
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

        // �����������ת
        float playerRotationY = player.eulerAngles.y;
        currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;

        // �����������ת
        Quaternion rotation = Quaternion.Euler(0, currentRotationX, 0);
        Vector3 targetPosition = player.position - rotation * offset;
        cameraTransform.position = targetPosition;
        cameraTransform.LookAt(player);
    }
}
