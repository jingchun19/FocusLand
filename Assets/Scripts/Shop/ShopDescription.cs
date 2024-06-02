using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;


public class ShopDescription : MonoBehaviour
{
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text costTxt;
    public int BtnNo;
    public ShopManager shopManager;

    public void PurchaseItem()
    {
        shopManager.PurchaseItem(BtnNo);
        gameObject.SetActive(false); // Hide the description after purchase
    }
}
