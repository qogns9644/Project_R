using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;    //이동 속도
    [SerializeField] private float gravity = -9.81f;   //중력계수

    private Vector3 moveForce;  //이동하는 힘

    private float curMoveSpeed; //현재 이동속도

    private CharacterController characterController;
    private Animator animator;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        curMoveSpeed = moveSpeed;
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

        moveForce = new Vector3(stickDir.x, moveForce.y, stickDir.y);

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

    /// <공격시, Move Speed를 조정한 이유>  
    /// 
    /// 현재 중력이 계속 체크 되고 있는 상태이고 플레이어의 Animator는 Apply root Motion이 체크 되어있다.
    /// Apply root motion이 체크 되므로서 애니메이션 고유의 변위값이 적용되어 좀 더 자연스러운 연출을 보여준다.
    /// 하지만 중력이 작용하고 있는 상태에서 변위가 있는 애니메이션을 실행하면 의도치 않게 조금씩 움직이게된다.
    /// 따라서, 자연스러운 애니메이션 연출을 위해 공격시에는 MoveSpeed를 0으로 조정하여 사용하고 있다.
    /// 단점은 공격시엔 중력을 받지 않게된다..
    /// </summary>

    public void SetAnim(Anim anim) {

        if (anim == Anim.ATTACK) {  //공격 중엔 다른 행동 X
            animator.SetTrigger("onComboAttack");    
            moveSpeed = 0f;             //공격중엔 움직이지 않도록 0으로 설정
        }
        else {       
            moveSpeed = curMoveSpeed;
            animator.ResetTrigger("onComboAttack");     //공격상태가 아닐 때, 불필요한 공격 Trigger 리셋
        }        
        animator.SetInteger("State", (int)anim);
    }

    public Anim GetAnim() {
       
        return (Anim)animator.GetInteger("State");
    }
}
