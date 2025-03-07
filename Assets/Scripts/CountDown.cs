using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public int countTime = 3;
    private int currentTime;
    public TextMeshProUGUI countDisplay;
    private bool isCountingDown = false; 
    private bool hasFinished = false;
    public GameObject panel; // 가림판

    public void StartCountdown()
    {
        if (isCountingDown || hasFinished) return;

        panel.SetActive(true);
        isCountingDown = true; 
        currentTime = countTime;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (currentTime > 0)
        {
            print($"현재 시간: {currentTime}");
            countDisplay.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        countDisplay.text = "";
        isCountingDown = false; 
        hasFinished = true; 
        panel.SetActive(false);
    }
}
