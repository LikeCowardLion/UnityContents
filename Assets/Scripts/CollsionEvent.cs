using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionEvent : MonoBehaviour
{

    public float loadDelay = 1f;

    //�ܺ� ��ü�� �浹 ���� ��
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false); // �浹�� ������Ʈ ��Ȱ��ȭ (�����)
        Score.score += 10;
    }

}
