using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] myPurchaseBtns;

    void Start()
    {
        for(int i = 0; i < shopItemSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        coinUI.text = "Coins: " + coins.ToString();
        LoadPanels();
        CheckPurchasable();
    }

    public void addCoins()
    {
        coins++;
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchasable();
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemSO[i].Description;
            shopPanels[i].costTxt.text = shopItemSO[i].baseCost.ToString();
        }
    }

    public void CheckPurchasable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (i < myPurchaseBtns.Length)
            {
                if (coins >= shopItemSO[i].baseCost)
                {
                    myPurchaseBtns[i].interactable = true;
                }
                else
                {
                    myPurchaseBtns[i].interactable = false;
                }
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (coins >= shopItemSO[btnNo].baseCost)
        {
            coins = coins - shopItemSO[btnNo].baseCost;
            coinUI.text = "Coins :" + coins.ToString();
            CheckPurchasable();
            //unlock item
        }
    }
}