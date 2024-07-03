using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Canvas canvas;

    [SerializeField] private ShopItem Item;

    // Fields for dragging
    private RectTransform rt;
    private CanvasGroup cg;
    private Image img;

    // To return to the original position
    private Vector3 originPos;
    private bool drag;

    public void Initialize(ShopItem item)
    {
        // Initialize Item
        Item = item;
    }

    private void Awake()
    {
        // Initialize fields
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        originPos = rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        drag = true;
        cg.blocksRaycasts = false;
        img.maskable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the icon object
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;
        cg.blocksRaycasts = true;
        img.maskable = true;
        rt.anchoredPosition = originPos;

        // Reset image visibility
        SetImageAlpha(1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (drag)
        {
            // Perform additional checks to ensure valid placement
            if (IsValidPlacement(other))
            {
                // Hide the shop and place the building
                ShopManagerNew.current.ShopButton_Click();
                SetImageAlpha(0f);

                Vector3 position = new Vector3(transform.position.x, transform.position.y, 0f);
                position = Camera.main.ScreenToWorldPoint(position);

                // Call the building system to start building
                BuildingSystem.current.InitializeWithObject(Item.Prefab, position);
            }
            else
            {
                // Revert to original position if placement is invalid
                rt.anchoredPosition = originPos;
            }
        }
    }

    private bool IsValidPlacement(Collider2D other)
    {
        // Example logic to determine if placement is valid
        // Check for overlapping buildings
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Building") && collider != other)
            {
                return false; // Invalid placement if overlapping another building
            }
        }
        return true; // Valid placement if no overlaps found
    }

    private void SetImageAlpha(float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    private void OnEnable()
    {
        drag = false;
        cg.blocksRaycasts = true;
        img.maskable = true;
        rt.anchoredPosition = originPos;

        // Make the image visible
        SetImageAlpha(1f);
    }
}
