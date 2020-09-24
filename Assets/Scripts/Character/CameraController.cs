using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target = null;      //추적 대상
    
    [SerializeField, Range(0, 10)] private float distance = 2.5f;          //카메라와의 거리
    [SerializeField, Range(0, 10)] private float height = 1.5f;            //카메라 높이

    private Transform tr = null;
    private void Start() {
        tr = transform;
    }

    private void LateUpdate()
    {
        // 추적대상 위치로부터 뒤쪽으로 distance만큼  위로 height 만큼 촬영
        tr.position = target.position - (Vector3.forward * distance) + (Vector3.up * height);          
        tr.LookAt(target);  //타겟 바라보기
    }
}
