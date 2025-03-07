using System.Collections;
using UnityEngine;

public class screenMove : MonoBehaviour
{
    private connect_test cnt_test;
    public GameObject sensorObject; // ✅ 센서 연결 오브젝트
    private float startXValue; // 초기 x 값 저장 변수
    private float threshold = 150f; // sensorEulerData.x 값 차이 기준
    private float initialXPosition = 172.45f; // 초기 x 좌표 값

    private RectTransform rectTransform; // RectTransform 컴포넌트 저장 변수
    private bool isRunning = false; // ✅ screenMove 실행 여부 체크
    private Coroutine moveCoroutine; // ✅ 코루틴 저장 변수

    void Start()
    {
        if (sensorObject == null)
        {
            Debug.LogError("sensorObject가 설정되지 않았습니다!");
            return;
        }

        cnt_test = sensorObject.GetComponent<connect_test>();

        if (cnt_test == null)
        {
            Debug.LogError("connect_test 스크립트를 찾을 수 없습니다.");
            return;
        }

        startXValue = cnt_test.sensorEulerData.x;

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform을 찾을 수 없습니다.");
            return;
        }

        rectTransform.anchoredPosition = new Vector2(initialXPosition, rectTransform.anchoredPosition.y);
    }

 
    public void StartScreenMove()
    {
        if (!isRunning) 
        {
            isRunning = true;
            moveCoroutine = StartCoroutine(ScreenMoveLoop());
        }
    }

    public void StopScreenMove()
    {
        if (isRunning)
        {
            isRunning = false;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }
    }

    IEnumerator ScreenMoveLoop()
    {
        while (isRunning)
        {
            float currentXValue = cnt_test.sensorEulerData.x;

            if (Mathf.Abs(currentXValue - startXValue) >= threshold)
            {
                print($"센서 값 변경 감지: {currentXValue}");
                print("이동 시작");

                if (Mathf.Approximately(rectTransform.anchoredPosition.x, 172.45f))
                {
                    rectTransform.anchoredPosition = new Vector2(-172.45f, rectTransform.anchoredPosition.y);
                    print("x 좌표가 -172.45로 이동했습니다.");
                }
                else
                {
                    rectTransform.anchoredPosition = new Vector2(172.45f, rectTransform.anchoredPosition.y);
                    print("x 좌표가 172.45로 이동했습니다.");
                }

                startXValue = currentXValue;
            }

            yield return null;
        }
    }
}
