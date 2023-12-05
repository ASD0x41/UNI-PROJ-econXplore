using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyMarket : MonoBehaviour
{

    public Text DollarrateText;
    public InputField CurrencyInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DollarrateText.text = "300";
    }
    
    public void ConverttoDollar()
    {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        float Dollarrate = float.Parse(DollarrateText.text);
        float DollarValue = CurrencyInputValue / Dollarrate;
        Debug.Log(DollarValue);
    }

    public void ConverttoPkr()
    {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        float Dollarrate = float.Parse(DollarrateText.text);
        float CurrencyValue = CurrencyInputValue * Dollarrate;
        Debug.Log(CurrencyValue);
    }

}
