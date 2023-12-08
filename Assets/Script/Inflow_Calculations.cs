using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Inflow_Calculations : MonoBehaviour
{    

    public Text total;
    public Text exports;
    public Text loans;
    public Text Assests;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(exports.text != "" && loans.text != "" && Assests.text != "")
        {
            Calculations();
        }
    }

    void Calculations()
    {
        IGovtExternalProfile govt = Govt.GetInstance();
        govt.GetTradeDetails(out double imports, out double myexports, out double remit);
        govt.GetForeignDebtReceipts(out double china, out double arabs, out double imf);

        float.TryParse(exports.text, out float exports_Value);
        float.TryParse(loans.text, out float loans_Value);
        float.TryParse(Assests.text, out float Assests_Value);
      
        total.text = ((int)(exports_Value + (china + arabs + imf) / 1_000_000_000 + Assests_Value + (remit / 1_000_000_000))).ToString();
    }
}
