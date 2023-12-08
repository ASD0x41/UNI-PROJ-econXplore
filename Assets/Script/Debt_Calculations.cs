using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Debt_Calculations : MonoBehaviour
{  
    public Text Debt;
    public Text Repayment;
    public Text owedChina;
    public Text owedArabs;
    public Text owedIMF;

    IGovtExternalProfile govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetForeignDebtPayments(out double chinapay, out double arabspay, out double imfpay);

        Debt.text = ((int)(double.Parse(owedChina.text) + double.Parse(owedArabs.text) + double.Parse(owedIMF.text))).ToString();
        Repayment.text = ((int)((chinapay + arabspay + imfpay) / 1_000_000_000)).ToString();
    }
}
