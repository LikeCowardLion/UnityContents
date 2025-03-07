using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaglidingMove : MonoBehaviour
{
    private connect_test cnt_test;
    public GameObject cntObj; // connect_test�� �پ��ִ� ������Ʈ
    private float startZ; // �ʱ� x��

    // Start is called before the first frame update
    void Start()
    {
        // connect_test ��ũ��Ʈ�� ������
        cnt_test = cntObj.GetComponent<connect_test>();

        if (cnt_test == null)
        {
            Debug.LogError("connect_test ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
        else
        {
            startZ = cnt_test.sensorEulerData.x; // ���� �ʱ� x�� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt_test != null)
        {
            float currentZValue = cnt_test.sensorEulerData.x;

            // ���� -180 ~ 180 ������ ����� �ʵ��� ����
            float clampedZValue = Mathf.Clamp(currentZValue, -180f, 180f);

            // ���ο� ȸ������ ���� (Z���� clampedZValue�� ����)
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                clampedZValue
            );
        }
    }
}
