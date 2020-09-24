using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;    //이동 속도
    [SerializeField] private float gravity = -9.81f;   //중력계수

    private Vector3 moveForce;  //이동하는 힘

    private CharacterController characterController;
    private Animator animator;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    //캐릭터의 중력체크
    public void GravityCheck() {

        //캐릭터가 공중에 떠있다면
        if (!characterController.isGrounded) {
            moveForce.y += gravity * Time.deltaTime;   //중력적용
            characterController.Move(moveForce * moveSpeed * Time.deltaTime);
        }       
    }

    public void MoveTo(Vector2 stickDir) {  //조이스틱으로 이동시, 캐릭터 이동

        moveForce = new Vector3 (stickDir.x, moveForce.y, stickDir.y);

        if (stickDir.magnitude > 0) {

            SetAnim(Anim.RUN);

            //플레이어 회전 = 조이스틱 방향각도 (아크탄젠트로 변환 후 나온 라디안값을 디그리값으로 변환하면 회전값이 나온다.)
            float rotate = Mathf.LerpAngle(transform.eulerAngles.y, Mathf.Atan2(stickDir.x, stickDir.y) * Mathf.Rad2Deg, Time.deltaTime * moveSpeed);          
            transform.eulerAngles = new Vector3(0, rotate, 0);     
            characterController.Move(moveForce * moveSpeed * Time.deltaTime);   //이동방향 , 속도 , Time.deltaTime 으로 세부적인 이동
        }
        else {
            SetAnim(Anim.IDLE);
        }
    }

    public void SetAnim(Anim anim) {
      
        if (anim == Anim.ATTACK) {
            animator.SetTrigger("onComboAttack");    //공격 중엔 다른 행동 X
        }
        else {
            animator.SetInteger("State", (int)anim);
        }
    }

}
