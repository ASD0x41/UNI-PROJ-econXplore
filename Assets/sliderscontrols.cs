using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;
using TMPro;

public class sliderscontrols : MonoBehaviour
{
    IGovtBudgetControls govt = Govt.GetInstance();
    public TextMeshProUGUI tax;
    public TextMeshProUGUI duty;
    public TextMeshProUGUI subsidy;
    public TextMeshProUGUI nationalisation;
    public TextMeshProUGUI salary;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        govt.AdjustTaxRate(double.Parse(tax.text));
        govt.AdjustAvgSalary(double.Parse(salary.text));
        govt.AdjustImpDutyRate(double.Parse(duty.text));
        govt.AdjustExpSubsidyRate(double.Parse(subsidy.text));
        govt.AdjustNationalisationLevel(double.Parse(nationalisation.text));

        //Debug.Log("Frontend " + double.Parse(tax.text).ToString());
        //Debug.Log(govt.GetTaxRate());
    }
}
