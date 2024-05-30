using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool isOccupied = false;
    private int x, y;
    private GridManager gridManager;
    private Image cellImage;

    public Color normalColor = Color.white;
    public Color hoverColor = Color.green;
    public Color occupiedColor = Color.red;

    void Awake()
    {
        cellImage = GetComponent<Image>();
        cellImage.color = normalColor;
    }
    
    public void Initialize(int x, int y, GridManager gridManager)
    {
        this.x = x;
        this.y = y;
        this.gridManager = gridManager;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.isPlacingBuildingEvent && !isOccupied)
        {
            cellImage.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetColor();
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        ResetColor();
    }

    public void ResetColor()
    {
        if (GameManager.Instance.isPlacingBuildingEvent)
        {
            cellImage.color = isOccupied ? occupiedColor : normalColor;
        }
        else
        {
            cellImage.color = normalColor;
        }
    }
}
