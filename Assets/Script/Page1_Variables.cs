using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Page1_Variables : MonoBehaviour
{ 
    [SerializeField] private Text ExportsText;
    [SerializeField] private Text SalesText;

    [SerializeField] private Text LoansText;
    
    [SerializeField] private Text ImportsText;

    [SerializeField] private Text InterestText;

    [SerializeField] private Text DebtText;

    IGovtExternalProfile govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetTradeDetails(out double imports, out double exports, out double remit);
        govt.GetForeignDebtPayments(out double chinaout, out double arabsout, out double imfout);
        govt.GetForeignDebtReceipts(out double chinain, out double arabsin, out double imfin);
        govt.GetForeignInterestPayments(out double china, out double arabs, out double imf);

        ExportsText.text = ((int)(exports / 1_000_000_000)).ToString();
        SalesText.text = ((int)(govt.GetAssetSales()  / 1_000_000_000)).ToString();
        LoansText.text = ((int)((chinain + arabsin + imfin) / 1_000_000_000)).ToString();
        //Debug.Log(chinain);
        //Debug.Log(arabsin);
        //Debug.Log(imfin);
        ImportsText.text = ((int)(imports / 1_000_000_000)).ToString();
        InterestText.text = ((int)((china + arabs + imf) / 1_000_000_000)).ToString();
        DebtText.text = ((int)((chinaout + arabsout + imfout) / 1_000_000_000)).ToString();
    }
}
