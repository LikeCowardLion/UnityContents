using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaglidingMove : MonoBehaviour
{
    private connect_test cnt_test;
    public GameObject cntObj; // connect_test가 붙어있는 오브젝트
    private float startZ; // 초기 x값

    // Start is called before the first frame update
    void Start()
    {
        // connect_test 스크립트를 가져옴
        cnt_test = cntObj.GetComponent<connect_test>();

        if (cnt_test == null)
        {
            Debug.LogError("connect_test 스크립트를 찾을 수 없습니다.");
        }
        else
        {
            startZ = cnt_test.sensorEulerData.x; // 가장 초기 x값 저장
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt_test != null)
        {
            float currentZValue = cnt_test.sensorEulerData.x;

            // 값이 -180 ~ 180 범위를 벗어나지 않도록 조정
            float clampedZValue = Mathf.Clamp(currentZValue, -180f, 180f);

            // 새로운 회전값을 설정 (Z축을 clampedZValue로 설정)
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                clampedZValue
            );
        }
    }
}
