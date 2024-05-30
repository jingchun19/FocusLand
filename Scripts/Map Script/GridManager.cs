using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 100;
    public int gridHeight = 100;
    public GameObject cellPrefab;
    public GameObject content;

    private GameObject[,] gridArray;

    void Start()
    {
        PopulateGrid();
    }

    void PopulateGrid()
    {
        gridArray = new GameObject[gridWidth, gridHeight];

        GridLayoutGroup gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gridWidth;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, content.transform);
                newCell.name = $"Cell {x},{y}"; // Cell xCoordinate, yCoordinate
                gridArray[x, y] = newCell;

                //add forest building later
                Cell gridScript = newCell.GetComponent<Cell>();
                if (gridScript != null)
                {
                    gridScript.Initialize(x, y, this);
                }
            }
        }
    }

    public bool CanPlaceBuilding(Vector2Int position, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (position.x + x >= gridWidth ||
                    position.y + y >= gridHeight ||
                    gridArray[position.x + x, position.y + y].GetComponent<Cell>().isOccupied)
                    {
                        return false;
                    }
            }
        }
        return true;
    }

    public void PlaceBuilding(Vector2Int position, Vector2Int size, GameObject buildingPrefab)
    {
        if (CanPlaceBuilding(position, size))
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    gridArray[position.x + x, position.y + y].GetComponent<Cell>().isOccupied = true;
                }
            }
            GameObject newBuilding = Instantiate(buildingPrefab, content.transform);
            RectTransform rt = newBuilding.GetComponent<RectTransform>();
            rt.anchoredPosition = gridArray[position.x, position.y].GetComponent<RectTransform>().anchoredPosition;
            rt.sizeDelta = new Vector2(size.x, size.y); // Adjust size to match grid
        }
        else
        {
            Debug.Log("Cannot place building here. Error"); // should not reach here
        }
    }

    public void ResetAllCellsColor()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                gridArray[x, y].GetComponent<Cell>().ResetColor();
            }
        }
    }
}
