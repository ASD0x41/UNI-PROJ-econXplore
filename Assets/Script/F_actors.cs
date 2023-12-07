using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class F_actors : MonoBehaviour
{ 
   
    public InputField inputField;

    public Text owed;
    public Text limit;
    public Text interest;
    public Button repay;
    public Button take;
    public string input;

    IGovtChineseDebtControls govtC = Govt.GetInstance();
    IGovtArabDebtControls govtA = Govt.GetInstance();
    IGovtIMFDebtControls govtI = Govt.GetInstance();
    IGovtLocalDebtControls govtL = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govtC.GetChineseDebtDetails(out double debtOwedC, out double interestRateC, out double debtLimitC, out bool allowedC);
        govtI.GetIMFDebtDetails(out double debtOwedI, out double interestRateI, out double debtLimitI, out bool allowedI);
        govtA.GetArabDebtDetails(out double debtOwedA, out double interestRateA, out double debtLimitA, out bool allowedA);
        govtL.GetLocalDebtDetails(out double debtOwedL, out double interestRateL, out double debtLimitL, out bool allowedL);

        if(input == "China")
        {
            owed.text = ((int)(debtOwedC / 1_000_000_000)).ToString();
            limit.text = ((int)(debtLimitC / 1_000_000_000)).ToString();
            interest.text = ((interestRateC * 100)).ToString() + " %";
        }
        else if(input == "IMF")
        {
            owed.text = ((int)(debtOwedI / 1_000_000_000)).ToString();
            limit.text = ((int)(debtLimitI / 1_000_000_000)).ToString();
            interest.text = ((interestRateI * 100)).ToString() + " %";
        }
        else if(input == "Arabs")
        {
            owed.text = ((int)(debtOwedA / 1_000_000_000)).ToString();
            limit.text = ((int)(debtLimitA / 1_000_000_000)).ToString();
            interest.text = ((interestRateA * 100)).ToString() + " %";
        }
        else if(input == "Local")
        {
            owed.text = ((int)(debtOwedL / 1_000_000_000)).ToString();
            limit.text = ((int)(debtLimitL / 1_000_000_000)).ToString();
            interest.text = ((interestRateL * 100)).ToString() + " %";
        }
    }

    public void TakeFromChina()
    {
        govtC.GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000 || inputField.text != "")
        {
            govtC.ManageChineseDebt(double.Parse(inputField.text) * 1_000_000_000);
            // take button shows up as pressed, repay button shows up as released
            
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void PayToChina()
    {
        govtC.GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000 || inputField.text != "") 
        {
            govtC.ManageChineseDebt(-double.Parse(inputField.text) * 1_000_000_000);
            // repay button shows up as pressed, take button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void TakeFromIMF()
    {
        govtI.GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000 || inputField.text != "")
        {
            govtI.ManageIMFDebt(double.Parse(inputField.text) * 1_000_000_000 );
            // take button shows up as pressed, repay button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void PayToIMF()
    {
        govtI.GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000 || inputField.text != "")
        {
            govtI.ManageIMFDebt(-double.Parse(inputField.text) * 1_000_000_000);
            // repay button shows up as pressed, take button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void TakeFromArabs()
    {
        govtA.GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000 || inputField.text != "")
        {
            govtA.ManageArabDebt(double.Parse(inputField.text) * 1_000_000_000);
            // take button shows up as pressed, repay button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void PayToArabs()
    {
        govtA.GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000 || inputField.text != "")
        {
            govtA.ManageArabDebt(-double.Parse(inputField.text) * 1_000_000_000);
            // repay button shows up as pressed, take button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void TakeFromLocal()
    {
        govtL.GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000 || inputField.text != "")
        {
            govtL.ManageLocalDebt(double.Parse(inputField.text) * 1_000_000_000);
            // take button shows up as pressed, repay button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void PayToLocal()
    {
        govtL.GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
        if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000 || inputField.text != "")
        {
            govtL.ManageLocalDebt(-double.Parse(inputField.text) * 1_000_000_000);
            // repay button shows up as pressed, take button shows up as released
        }
        else
        {
            // invalid value entered popup
        }
    }

    public void repaybtnSelected()
    {
      
       Color targetColor = new Color(1, 255, 0); // Replace with the color you want

       if (repay.GetComponent<Button>().colors.normalColor == targetColor)
    {
        repay.GetComponent<Button>().interactable = true;
        var colors = take.GetComponent<Button>().colors;
        colors.normalColor = Color.white; // Replace with the color you want
        take.GetComponent<Button>().colors = colors;
    }
    else
    {
        repay.GetComponent<Button>().interactable = false;
        var colors = take.GetComponent<Button>().colors;
        colors.normalColor = targetColor;
        take.GetComponent<Button>().colors = colors;
    }
    }

  public void takebtnSelected()
{
    Color targetColor = new Color(1, 255, 0); // Replace with the color you want

    if (take.GetComponent<Button>().colors.normalColor == targetColor)
    {
        repay.GetComponent<Button>().interactable = true;
        var colors = take.GetComponent<Button>().colors;
        colors.normalColor = Color.white; // Replace with the color you want
        take.GetComponent<Button>().colors = colors;
    }
    else
    {
        repay.GetComponent<Button>().interactable = false;
        var colors = take.GetComponent<Button>().colors;
        colors.normalColor = targetColor;
        take.GetComponent<Button>().colors = colors;
    }
}

}
