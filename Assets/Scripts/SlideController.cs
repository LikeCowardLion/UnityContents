using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public ScrollRect scrollRect; 
    public int totalSlides = 5; // 전체 슬라이드 개수
    private int currentSlide = 0; // 현재 슬라이드 인덱스
    public CountDown countDownScript; // CountDown 스크립트 참조
    public screenMove screenMoveScript;
    private bool isScreenMoveActive = false; 

    private void Update()
    {
        CheckSlidePosition(); // 슬라이드 위치 확인
    }

    public void NextSlide()
    {
        if (currentSlide < totalSlides - 1) // 마지막 슬라이드가 아닐 경우만 이동
        {
            currentSlide++;
            MoveSlide();
        }
    }

    public void PrevSlide()
    {
        if (currentSlide > 0) // 첫 번째 슬라이드가 아닐 경우만 이동
        {
            currentSlide--;
            MoveSlide();
        }
    }

    private void MoveSlide()
    {
        float targetPosition = (float)currentSlide / (totalSlides - 1);
        scrollRect.horizontalNormalizedPosition = targetPosition;
    }

    private void CheckSlidePosition()
    {
        if (countDownScript == null)
        {
            Debug.LogError("countDownScript가 설정되지 않았습니다.");
            return;
        }

        if (currentSlide == 3) 
        {
            print("start corutine");
            countDownScript.StartCountdown();
        }

        if (currentSlide == 3)
        {
            if (!isScreenMoveActive) 
            {
                screenMoveScript.StartScreenMove();
                isScreenMoveActive = true;
            }
        }
        else
        {
            if (isScreenMoveActive)
            {
                screenMoveScript.StopScreenMove();
                isScreenMoveActive = false;
            }
        }
    }

}
