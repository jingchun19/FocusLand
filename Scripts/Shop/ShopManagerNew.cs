using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManagerNew : MonoBehaviour
{
    //singletone pattern
    public static ShopManagerNew current; 
    
    //currency sprites for initialization
    public static Sprite CurrencySprite;

    //fields for animating the shop window
    private RectTransform rt;
    private RectTransform prt;
    [SerializeField] private bool opened;

    //prefab for displaying shop info
    [SerializeField] private GameObject itemPrefab;
    //al shop items
    private Dictionary<ObjectType, List<ShopItem>> shopItems = new Dictionary<ObjectType, List<ShopItem>>(3);

    //tabs with items
    [SerializeField] public TabGroup shopTabs;

    private void Awake()
    {
        //initialize fields
        current = this;

        rt = GetComponent<RectTransform>();
        prt = transform.parent.GetComponent<RectTransform>();

        Debug.Log("rt: " + rt);
        Debug.Log("prt: " + prt);
        
        //subscribe method to an event
        EventManager.Instance.AddListener<LevelChangeEvent>(OnLevelChanged);
    }


    private void Start()
    {
        //load shop items and initialize the shop
        Load();
        Initialize();
        
        //disable the shop window so the tabs are not visible
        gameObject.SetActive(false);
    }

    private void Load()
    {
        //load every shop item from resources
        ShopItem[] items = Resources.LoadAll<ShopItem>("Shop");
        
        //initialize the dictionary
        shopItems.Add(ObjectType.Buildings, new List<ShopItem>());
        shopItems.Add(ObjectType.Characters, new List<ShopItem>());
        shopItems.Add(ObjectType.FocusClock, new List<ShopItem>());

        //add all shop items to the dictionary
        foreach (var item in items)
        {
            shopItems[item.Type].Add(item);
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < shopItems.Keys.Count; i++)
        {
            foreach (var item in shopItems[(ObjectType)i])
            {
                //create an item holder and initialize it
                GameObject itemObject = Instantiate(itemPrefab, shopTabs.objectsToSwap[i].transform);
                itemObject.GetComponent<ShopItemHolder>().Initialize(item);
            }
        }
    }

    private void OnLevelChanged(LevelChangeEvent info)
    {
        //when the player gets a new level
        for (int i = 0; i < shopItems.Keys.Count; i++)
        {
            ObjectType key = shopItems.Keys.ToArray()[i];
            for (int j = 0; j < shopItems[key].Count; j++)
            {
                ShopItem item = shopItems[key][j];

                if (item.Level == info.newLevel)
                {
                    //unlock item if its level matches the new one
                    shopTabs.transform.GetChild(i).GetChild(j).GetComponent<ShopItemHolder>().UnlockItem();
                }
            }
        }
    }
    
    public void ShopButton_Click()
    {
        //animation time
        float time = 0.2f;
        Debug.Log("ShopButton_Click called");
        Debug.Log("opened: " + opened);
        if (!opened)
        {
            //open the shop
            Debug.Log("Opening shop");
            LeanTween.moveX(prt, prt.anchoredPosition.x + rt.sizeDelta.x + 100, time);
            opened = true;
            gameObject.SetActive(true);
        }
        else
        {
            //close the shop
            Debug.Log("Closing shop");
            LeanTween.moveX(prt, prt.anchoredPosition.x - rt.sizeDelta.x - 100, time)
                .setOnComplete(delegate()
                {
                    gameObject.SetActive(false);
                });
            opened = false;
        }
    }

    //make the shop close when the player click on the area
    private bool dragging;

    public void OnBeginDrag()
    {
        dragging = true;
    }

    public void OnEndDrag()
    {
        dragging = false;
    }

    public void OnPointerClick()
    {
        if (!dragging)
        {
            ShopButton_Click();
        }
    }
}
