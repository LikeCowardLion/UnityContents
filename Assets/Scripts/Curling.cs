using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curling : MonoBehaviour
{
    private Rigidbody rb;
    public float chargeRate = 5f; //충전 속도(초당 힘의 증가량)
    private float chargeTime = 0f; // 마우스 버튼을 누르고 있는 시간
    private bool isCharging = false;
    private bool isStop = false;
    private bool isPoint = false;

    void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody가 Puck 오브젝트에 없습니다!");
            enabled = false; // 스크립트 비활성화
            return;
        }

        // 오브젝트가 굴러가지 않도록 회전 제한
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 누르기 시작
        if (Input.GetMouseButtonDown(0))
        {
            isStop = false;
            isCharging = true;
            chargeTime = 0f; // 충전 시간 초기화
        }

        // 마우스 왼쪽 버튼을 누르고 있는 동안
        if (Input.GetMouseButton(0) && isCharging)
        {
            chargeTime += Time.deltaTime * chargeRate*10; // 충전
        }

        // 마우스 왼쪽 버튼을 떼는 순간
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            ApplyForce();
            StartCoroutine(CheckStopped()); //정지 감지 시작 
        }
        IsStopPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name}");

        if (other.gameObject.CompareTag("Point"))
        {
            isPoint = true;
            Debug.Log($"{isPoint}");
        }
        else
        {
            isPoint = false; //다시 되돌리기
            Debug.Log($"{isPoint}");
        }
    }

    void IsStopPoint()
    {
        Debug.Log($"{isPoint}");
        if (isPoint &&  isStop)
        {
            Score.score += 100;
            isStop = false;
        }
    }

    void ApplyForce()
    {
        // 힘 제한
        float force = chargeTime;

        // x축 방향으로 속도 설정
        Vector3 velocity = Vector3.right * force; // x축 방향 속도
        rb.velocity = velocity;

        Debug.Log($"Puck에 x축 방향으로 {force}만큼의 속도를 설정했습니다.");
    }

    IEnumerator CheckStopped()
    {
        yield return new WaitForSeconds(0.5f); //잠시 대기

        while(rb.velocity.magnitude > 0.01f)
        {
            yield return null;
        }
        isStop = true; //물체가 완전 멈춘 것을 확인
        Debug.Log("Puck이 정지했습니다!");
    }

}