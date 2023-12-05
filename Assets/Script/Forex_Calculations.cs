using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Forex_Calculations : MonoBehaviour
{   
    
    public Text OldForex;
    public Text NewForex;
    public Text Net;

    public Text inFlow;
    public Text outFlow;

    IGovtForFinancialBody govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetCountryFinanceReport(out double assets, out double liabilities, out double forex);

        NewForex.text = ((int)(forex / 1_000_000_000)).ToString();
        Net.text = ((int)(double.Parse(outFlow.text) - double.Parse(inFlow.text))).ToString();
        OldForex.text = ((int)((forex / 1_000_000_000) - double.Parse(Net.text))).ToString();
    }
}
