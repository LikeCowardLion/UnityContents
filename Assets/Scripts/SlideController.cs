using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public ScrollRect scrollRect; 
    public int totalSlides = 5; // ��ü �����̵� ����
    private int currentSlide = 0; // ���� �����̵� �ε���
    public CountDown countDownScript; // CountDown ��ũ��Ʈ ����
    public screenMove screenMoveScript;
    private bool isScreenMoveActive = false; 

    private void Update()
    {
        CheckSlidePosition(); // �����̵� ��ġ Ȯ��
    }

    public void NextSlide()
    {
        if (currentSlide < totalSlides - 1) // ������ �����̵尡 �ƴ� ��츸 �̵�
        {
            currentSlide++;
            MoveSlide();
        }
    }

    public void PrevSlide()
    {
        if (currentSlide > 0) // ù ��° �����̵尡 �ƴ� ��츸 �̵�
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
            Debug.LogError("countDownScript�� �������� �ʾҽ��ϴ�.");
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
