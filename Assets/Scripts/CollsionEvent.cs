using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionEvent : MonoBehaviour
{

    public float loadDelay = 1f;

    //외부 물체와 충돌 했을 때
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false); // 충돌한 오브젝트 비활성화 (사라짐)
        Score.score += 10;
    }

}
