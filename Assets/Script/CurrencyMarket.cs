using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class CurrencyMarket : MonoBehaviour
{

    public Text DollarrateText;
    public InputField CurrencyInput;

    public Button dollars;

    public Button pkr;

    IGovtCurrencyExchangeControls govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetCurrencyExchangeDetails(out double treasuryBal, out double dollarRate, out double forexBal);
        DollarrateText.text = "Rs. " + ((int)dollarRate).ToString();
    }
    
    public void ConverttoDollar()
    {
        if(CurrencyInput.text != "")
        {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        govt.TradeForex(CurrencyInputValue * 1_000_000_000);
        }
    }

    public void ConverttoPkr()
    {
        if(CurrencyInput.text != "")
        {
        float CurrencyInputValue = float.Parse(CurrencyInput.text);
        govt.TradeForex(-CurrencyInputValue * 1_000_000_000);
        }
    }

    public void DollarsbtnSelected()
    {

       Color targetColor = new Color(255, 255, 0); 

       if (dollars.GetComponent<Button>().colors.selectedColor == targetColor)
        {
            pkr.GetComponent<Button>().interactable = true;
            var colors = dollars.GetComponent<Button>().colors;
            colors.selectedColor = Color.white; 
            colors.normalColor = Color.white; 
            dollars.GetComponent<Button>().colors = colors;
        }
        else
        {
            pkr.GetComponent<Button>().interactable = false;
            var colors = dollars.GetComponent<Button>().colors;
            colors.selectedColor = targetColor;
            colors.normalColor = targetColor;
            dollars.GetComponent<Button>().colors = colors;
        }
    }

   public void PkrbtnSelected()
    {
        Color targetColor = new Color(0, 128, 0); 

        

        if (pkr.GetComponent<Button>().colors.selectedColor == targetColor)
        {
            dollars.GetComponent<Button>().interactable = true;
            var colors = pkr.GetComponent<Button>().colors;
            colors.selectedColor = Color.white; 
            colors.normalColor = Color.white; 
            pkr.GetComponent<Button>().colors = colors;
        }
        else
        {
            dollars.GetComponent<Button>().interactable = false;
            var colors = pkr.GetComponent<Button>().colors;
            colors.selectedColor = targetColor;
            colors.normalColor = targetColor;
            pkr.GetComponent<Button>().colors = colors;
        }
    }

}
