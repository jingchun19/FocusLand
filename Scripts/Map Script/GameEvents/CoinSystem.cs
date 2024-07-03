using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CoinSystem : MonoBehaviour
{
    private TMP_Text CurrencyText;
    private int CurrencyAmount;

    private void Start()
    {
        EventManager.Instance.AddListener<CurrencyChange>(onCurrencyChange);
        EventManager.Instance.AddListener<NotEnoughCurrency>(onNotEnough);
    }

    public void onCurrencyChange(CurrencyChange info) {
        // todo save currency
        CurrencyAmount += info.amount;
        CurrencyText.text = CurrencyAmount.ToString();
    }

    public void onNotEnough(NotEnoughCurrency info)
    {
        Debug.Log("You do not have enough coins");
    }
}
