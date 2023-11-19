using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMove : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float rotationSpeed = 15.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 20.0f;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = verticalInput * camForward + horizontalInput * Camera.main.transform.right;

            if (move != Vector3.zero)
            {
                // �����ƶ��������ת�Ƕ�
                Quaternion targetRotation = Quaternion.LookRotation(move);

                // ƽ������ת��ɫ�����ƶ�����
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            moveDirection = move * moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (controller.transform.position.y < -10)
        {
            // ��ҵ� Y ����С�� -10�����䴫�ͻ�(0, 0, 0)
            controller.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
