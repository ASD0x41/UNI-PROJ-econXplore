using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Forex_Calculations : MonoBehaviour
{   
    

    public Text NewForex;
    public Text Net;

    public Text inFlow;
    public Text outFlow;

    public Text Remittance;

    IGovtForFinancialBody govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetCountryFinanceReport(out double assets, out double liabilities, out double forex, out double remit);

        NewForex.text = ((int)(forex / 1_000_000_000)).ToString();
        Net.text = ((int)(double.Parse(inFlow.text) - double.Parse(outFlow.text))).ToString();

        Remittance.text = ((int)(remit / 1_000_000_000)).ToString();
    }
}
