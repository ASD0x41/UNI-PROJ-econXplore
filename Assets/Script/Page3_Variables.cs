using EMG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page3_Variables : MonoBehaviour
{
    public Text Taxes;

    public Text ImportDuty;

    public Text Privatization;

    public Text Fines;

    public Text LocalLoans;

    public Text Salaries;

    public Text Subsidies;

    public Text Welfare;

    public Text Development;

    public Text Intrest;

    public Text Nationalization;

    public Text PublicEvents;
    
    public Text NewTreasury;

    public Text NetValue;

    IGovtInternalProfile govt = Govt.GetInstance();
    IGovtCurrencyExchangeControls govt2 = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetForexTradeDetailsRupees(out double bought, out double sold);
        govt.GetNationalisationDetails(out double privatisationProceeds, out double nationalisationFund);
        govt.GetGovtRevenueDetails(out double taxes, out double duties);
        govt.GetGovtExpenseDetails(out double salaries, out double subsidies, out double welfare, out double devFund);
        govt.GetLocalPaymentDetails(out double debtTaken, out double debtRepaid, out double interestPaid);
        govt.GetMiscFinancialDetails(out double diversion, out double raidProceeds);
        govt2.GetCurrencyExchangeDetails(out double treasuryBal, out double dollarRate, out double forexReservesBal);

        Taxes.text = ((int)(taxes / 1_000_000_000)).ToString();
        ImportDuty.text = ((int)(duties / 1_000_000_000)).ToString();
        Privatization.text = ((int)(privatisationProceeds / 1_000_000_000)).ToString();
        Fines.text = ((int)(raidProceeds / 1_000_000_000)).ToString();
        LocalLoans.text = ((int)(debtTaken * 10 / 1_000_000_000)).ToString();
        Salaries.text = ((int)(salaries / 1_000_000_000)).ToString();
        Subsidies.text = ((int)(subsidies / 1_000_000_000)).ToString();
        Welfare.text = ((int)(welfare / 1_000_000_000)).ToString();
        Development.text = ((int)(devFund / 1_000_000_000)).ToString();
        Intrest.text = ((int)(interestPaid / 1_000_000_000)).ToString();
        Nationalization.text = ((int)(nationalisationFund / 1_000_000_000)).ToString();
        PublicEvents.text = ((int)(diversion / 1_000_000_000)).ToString();


        NewTreasury.text = ((int)(treasuryBal / 1_000_000_000)).ToString();
        NetValue.text = ((int)((taxes + duties + privatisationProceeds + debtTaken + raidProceeds - salaries - subsidies - welfare - devFund - interestPaid - nationalisationFund - diversion) / 1_000_000_000)).ToString();
    }
}
