using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private connect_test cnt_test;
    public GameObject cntObj; // connect_test가 붙어있는 오브젝트
    private float startX; //초기 x값

    public float speed = 20f;
    private Rigidbody rb;
    public GameObject goalOptionCanvas; //캔버스 활성/비활성화 용도
    bool gameOver;
    bool goal;

    void Start()
    {
        //conntect_test  스크립트를 가져옴
        cnt_test = cntObj.GetComponent<connect_test>();
        startX = cnt_test.sensorEulerData.x; //가장 초기 x값

        if (cnt_test == null)
        {
            Debug.LogError("connect_test 스크립트를 찾을 수 없습니다.");
        }

        rb = GetComponent<Rigidbody>();
        goalOptionCanvas.SetActive(false);
        gameOver = false;
        goal = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (cnt_test != null)
        {
            float currentXValue = cnt_test.sensorEulerData.x;
            float currentxValue = 2 * currentXValue;

            // 새로운 회전값을 설정 (X축을 currentXValue로 설정)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentxValue, transform.rotation.eulerAngles.z);
        }

        if (gameOver || goal) // 추가 - 조건이 만족되면 Pause() 호출
        {
            Pause();
        }

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        goalOptionCanvas.SetActive(true); //goal이라 쓰긴했는데 종료 캔버스임.
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log($"{other.gameObject.name} 도착!");
            goal = true;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Score.score += 1;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ocean"))
        {
            //추락한 경우 
            Debug.Log("추락!");
            gameOver = true;
        }
    }

    private IEnumerator SlowDown()
    {
        while (speed > 0)
        {
            speed -= 1f; // 속도를 점진적으로 감소
            yield return new WaitForSeconds(1f); // 0.1초 간격으로 감소
        }
        speed = 0f; // 속도를 정확히 0으로 설정
    }

}
