using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class CurrencyMarket : MonoBehaviour
{

    public Text DollarrateText;
    public InputField CurrencyInput;

    IGovtCurrencyExchangeControls govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetCurrencyExchangeDetails(out double treasuryBal, out double dollarRate, out double forexBal);
        DollarrateText.text = "Rs. " + dollarRate.ToString();
    }
    
    public void ConverttoDollar()
    {
        if(CurrencyInput.text != "")
        {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        govt.TradeForex(CurrencyInputValue);
        }
    }

    public void ConverttoPkr()
    {
        if(CurrencyInput.text != "")
        {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        govt.TradeForex(-CurrencyInputValue);
        }
    }

}
