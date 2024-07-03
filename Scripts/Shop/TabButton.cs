using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    //tab group this button belongs to
    public TabGroup tabGroup;
    //tab button background
    public Sprite idleSprite;
    public Sprite activeSprite;

    private void Awake()
    {
        //subscribe the button
        tabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //on click select the tab
        tabGroup.OnTabSelected(this);
    }

    public void SetIdleState()
    {
        GetComponent<Image>().sprite = idleSprite;
    }

    public void SetActiveState()
    {
        GetComponent<Image>().sprite = activeSprite;
    }
}
