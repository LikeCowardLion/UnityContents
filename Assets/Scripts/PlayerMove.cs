using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private connect_test cnt_test;
    public GameObject cntObj; // connect_test�� �پ��ִ� ������Ʈ
    private float startX; //�ʱ� x��

    public float speed = 20f;
    private Rigidbody rb;
    public GameObject goalOptionCanvas; //ĵ���� Ȱ��/��Ȱ��ȭ �뵵
    bool gameOver;
    bool goal;

    void Start()
    {
        //conntect_test  ��ũ��Ʈ�� ������
        cnt_test = cntObj.GetComponent<connect_test>();
        startX = cnt_test.sensorEulerData.x; //���� �ʱ� x��

        if (cnt_test == null)
        {
            Debug.LogError("connect_test ��ũ��Ʈ�� ã�� �� �����ϴ�.");
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

            // ���ο� ȸ������ ���� (X���� currentXValue�� ����)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentxValue, transform.rotation.eulerAngles.z);
        }

        if (gameOver || goal) // �߰� - ������ �����Ǹ� Pause() ȣ��
        {
            Pause();
        }

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        goalOptionCanvas.SetActive(true); //goal�̶� �����ߴµ� ���� ĵ������.
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log($"{other.gameObject.name} ����!");
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
            //�߶��� ��� 
            Debug.Log("�߶�!");
            gameOver = true;
        }
    }

    private IEnumerator SlowDown()
    {
        while (speed > 0)
        {
            speed -= 1f; // �ӵ��� ���������� ����
            yield return new WaitForSeconds(1f); // 0.1�� �������� ����
        }
        speed = 0f; // �ӵ��� ��Ȯ�� 0���� ����
    }

}
