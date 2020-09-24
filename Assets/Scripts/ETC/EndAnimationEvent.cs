using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animator 에서 ComboAttack state 중 AttackEnd State에서 사용중
/// </summary>
/// 

public class EndAnimationEvent : StateMachineBehaviour
{
    //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    // 새로운 상태로 변할 때 실행
        
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // 처음과 마지막 프레임을 제외한 각 프레임 단위로 실행
    //}

    //OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // 상태가 다음 상태로 바뀌기 직전에 실행
        animator.SetInteger("State", (int)Anim.IDLE);
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //     // MonoBehaviour.OnAnimatorMove 직후에 실행
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // MonoBehaviour.OnAnimatorIK 직후에 실행
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
        // 스크립트가 부착된 상태 기계로 전환이 왔을때 실행

    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //     // 스크립트가 부착된 상태 기계에서 빠져나올때 실행
    //}
}
