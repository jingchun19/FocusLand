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
    public GameObject shopDescriptionGO;
    public ShopDescription shopDescription;

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

    public void LoadDescription(int btnNo)
    {
        Debug.Log("Loading description for button number: " + btnNo);

        if (shopDescriptionGO == null)
        {
            Debug.LogError("shopDescriptionGO is not assigned!");
            return;
        }

        if (shopDescription == null)
        {
            Debug.LogError("shopDescription is not assigned!");
            return;
        }

        if (shopItemSO == null || shopItemSO.Length <= btnNo)
        {
            Debug.LogError("shopItemSO is not assigned or btnNo is out of bounds!");
            return;
        }

        shopDescriptionGO.SetActive(true);

        if (shopDescription.titleTxt == null)
        {
            Debug.LogError("shopDescription.titleTxt is not assigned!");
            return;
        }

        if (shopDescription.descriptionTxt == null)
        {
            Debug.LogError("shopDescription.descriptionTxt is not assigned!");
            return;
        }

        if (shopDescription.costTxt == null)
        {
            Debug.LogError("shopDescription.costTxt is not assigned!");
            return;
        }

        Debug.Log("All necessary references are assigned, setting text fields.");

        shopDescription.titleTxt.text = shopItemSO[btnNo].title;
        shopDescription.descriptionTxt.text = shopItemSO[btnNo].Description;
        shopDescription.costTxt.text = shopItemSO[btnNo].baseCost.ToString();
        shopDescription.BtnNo = btnNo;
        shopDescription.shopManager = this;

        Debug.Log("Description loaded successfully for button number: " + btnNo);
    }
}