using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] PlayerController playerController = null;
    [SerializeField] private RectTransform stick = null;
    [SerializeField, Range(10, 150)] private float stickLimiteRange = 100f;  //스틱 최대 이동 반경

    private RectTransform joyStickBG;

    private Vector2 moveDir;   //캐릭터 이동 방향
    private bool isInput;      //조이스틱 입력 여부

    private void Start() {
        joyStickBG = GetComponent<RectTransform>();
    }

    private void Update() {

        playerController.GravityCheck();
        if (MoveCheck()) {
            MovePlayer();
        }
        if (Input.GetKeyDown(KeyCode.A))
            OnClickAttack();
    }

    //플레이어 이동 여부 체크
    private bool MoveCheck() {

        Anim anim = playerController.GetAnim();
        return isInput && (anim == Anim.IDLE || anim == Anim.RUN);     //플레이어가 이동 및 대기중 일 때만 이동가능
    }

    //조이스틱으로 인해 캐릭터 이동 구현
    private void MovePlayer() {
        playerController.MoveTo(moveDir);
    }

    private void JoystickControll(PointerEventData eventData) {
     
        // 스틱 위치 = 드래그 위치 - 스틱 위치
        Vector2 stickPos = eventData.position - joyStickBG.anchoredPosition;

        // 스틱이 범위를 벗어낫다면 stick최대값인 normalized(방향벡터) * stickRange 적용
        Vector2 stickVector = stickPos.magnitude < stickLimiteRange ? stickPos : stickPos.normalized * stickLimiteRange;
        stick.anchoredPosition = stickVector;       //스틱 드래그 위치로 이동
        moveDir = stickVector.normalized;

    }

    public void OnBeginDrag(PointerEventData eventData) {
        JoystickControll(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData) {
        JoystickControll(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {

        stick.anchoredPosition = Vector2.zero;  //스틱 위치 초기화
        isInput = false;
        playerController.MoveTo(Vector2.zero);  //이동을 멈춘다면, 다시 IDLE상태로 돌아가도록
    }

    #region UI

   
    public void OnClickAttack() {
        playerController.SetAnim(Anim.ATTACK); 
        
    }

    public void OnClickSkill(int skillNum) {


    }

    #endregion UI
}
