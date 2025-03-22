using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //  달리기 차선 설정
    const int minLine = -2;
    const int maxLine = 2;
    const float lineWidth = 1.0f;

    CharacterController controller;

    Vector3 moveDir = Vector3.zero;

    public float gravity;
    public float moveSpeed;
    public float jumpSpeed;

    int targetLine;
    public float speed_X; //수평 방향 속도
    public float acceler_Z; //전진 방향 속도

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            Move_Left();
        }
        if (Input.GetKeyDown("right"))
        {
            Move_Right();
        }
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }

        //가속 전진
        float acceleratgedZ = moveDir.z + (acceler_Z * Time.deltaTime);
        moveDir.z = Mathf.Clamp(acceleratgedZ, 0, moveSpeed);

        //좌우 방향 구하기
        float ratioX = (targetLine * lineWidth - transform.position.x) / lineWidth;
        moveDir.x = ratioX * speed_X;

        // 중력 계산
        moveDir.y -= gravity * Time.deltaTime;

        // 이동
        Vector3 globalDir = transform.TransformDirection(moveDir) ;
        controller.Move(globalDir * Time.deltaTime);
        

        // 이동하면서 땅에 닿아있으면 Y는 고정
        if (controller.isGrounded)
        {
            moveDir.y = 0;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (v > 0f)
            {
                moveDir.z = v * moveSpeed;
            }
            else
            {
                moveDir.z = 0;
            }
            transform.Rotate(0, h, 0);

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }

        controller.Move(transform.TransformDirection(moveDir) * Time.deltaTime);

    }
    public void Move_Left()
    {
        if(controller.isGrounded&& targetLine> minLine)
        {
            targetLine--;
        }
    }
    public void Move_Right()
    {
        if (controller.isGrounded && targetLine < maxLine)
        {
            targetLine++;
        }
    }
    public void Jump()
    {
        if (controller.isGrounded)
        {

            moveDir.y = jumpSpeed;
        }
    }
}