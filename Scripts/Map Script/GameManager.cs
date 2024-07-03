using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singletone pattern
    public static GameManager Instance;

    //save the canvas
    public GameObject canvas;

    private void Awake()
    {
        //initialize fields
        Instance = this;
        
        //initialize
        ShopItemDrag.canvas = canvas.GetComponent<Canvas>();
    }
}
