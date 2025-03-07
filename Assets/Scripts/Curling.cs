using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curling : MonoBehaviour
{
    private Rigidbody rb;
    public float chargeRate = 5f; //���� �ӵ�(�ʴ� ���� ������)
    private float chargeTime = 0f; // ���콺 ��ư�� ������ �ִ� �ð�
    private bool isCharging = false;
    private bool isStop = false;
    private bool isPoint = false;

    void Start()
    {
        // Rigidbody ������Ʈ ��������
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody�� Puck ������Ʈ�� �����ϴ�!");
            enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
            return;
        }

        // ������Ʈ�� �������� �ʵ��� ȸ�� ����
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // ���콺 ���� ��ư ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            isStop = false;
            isCharging = true;
            chargeTime = 0f; // ���� �ð� �ʱ�ȭ
        }

        // ���콺 ���� ��ư�� ������ �ִ� ����
        if (Input.GetMouseButton(0) && isCharging)
        {
            chargeTime += Time.deltaTime * chargeRate*10; // ����
        }

        // ���콺 ���� ��ư�� ���� ����
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            ApplyForce();
            StartCoroutine(CheckStopped()); //���� ���� ���� 
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
            isPoint = false; //�ٽ� �ǵ�����
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
        // �� ����
        float force = chargeTime;

        // x�� �������� �ӵ� ����
        Vector3 velocity = Vector3.right * force; // x�� ���� �ӵ�
        rb.velocity = velocity;

        Debug.Log($"Puck�� x�� �������� {force}��ŭ�� �ӵ��� �����߽��ϴ�.");
    }

    IEnumerator CheckStopped()
    {
        yield return new WaitForSeconds(0.5f); //��� ���

        while(rb.velocity.magnitude > 0.01f)
        {
            yield return null;
        }
        isStop = true; //��ü�� ���� ���� ���� Ȯ��
        Debug.Log("Puck�� �����߽��ϴ�!");
    }

}