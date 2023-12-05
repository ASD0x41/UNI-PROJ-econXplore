using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxpage;
    int currentpage;
    Vector3 targetPos; 
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform GamePagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    float dragThreshold;

    [SerializeField] Image[] pageIndicators;
    [SerializeField] Sprite pageClose, pageOpen;
    
    [SerializeField] Button nextButton, prevButton;

    private void Awake()
    {
        currentpage = 2;
        targetPos = GamePagesRect.localPosition;
        dragThreshold = Screen.width / 50;
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next()
    {
        if (currentpage < maxpage)
        {
            currentpage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Prev()
    {
        if (currentpage > 1)
        {
            currentpage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        GamePagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if(eventData.position.x > eventData.pressPosition.x)
            {
                Prev();
            }
            else Next();
        }
        else
        {
            MovePage();
        }
    }
    
    void UpdateBar()
    {
        foreach (var item in pageIndicators)
        {
            item.sprite = pageClose;
        }
        pageIndicators[currentpage - 1].sprite = pageOpen;
    }

    void UpdateArrowButton()
    {
        nextButton.interactable = true;
        prevButton.interactable = true;
        if(currentpage == 1)
        {
            prevButton.interactable = false;
        }
        if(currentpage == maxpage)
        {
            nextButton.interactable = false;
        }
    }
}
