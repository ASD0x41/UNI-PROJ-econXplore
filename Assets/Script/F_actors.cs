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
        if(inputField.text != "")
        {
            govtC.GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000 || inputField.text != "")
            {
                govtC.ManageChineseDebt(double.Parse(inputField.text) * 1_000_000_000);
                // take button shows up as pressed, repay button shows up as released
                
            }
        }
    }

    public void PayToChina()
    {
        if(inputField.text != "")
        {
            govtC.GetChineseDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000) 
            {
                govtC.ManageChineseDebt(-double.Parse(inputField.text) * 1_000_000_000);
                // repay button shows up as pressed, take button shows up as released
            }
        }
    }

    public void TakeFromIMF()
    {
        if(inputField.text != "")
        {
            govtI.GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000)
            {
                govtI.ManageIMFDebt(double.Parse(inputField.text) * 1_000_000_000 );
                // take button shows up as pressed, repay button shows up as released
                //Debug.Log("in takeIMF func");
            }
        }
    }

    public void PayToIMF()
    {
        if(inputField.text != "")
        {
            govtI.GetIMFDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000)
            {
                govtI.ManageIMFDebt(-double.Parse(inputField.text) * 1_000_000_000);
                // repay button shows up as pressed, take button shows up as released
            }
        }
    }

    public void TakeFromArabs()
    {
        if(inputField.text != "")
        {
            govtA.GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000)
            {
                govtA.ManageArabDebt(double.Parse(inputField.text) * 1_000_000_000);
                // take button shows up as pressed, repay button shows up as released
            }
        }
    }

    public void PayToArabs()
    {
        if(inputField.text != "")
        {
            govtA.GetArabDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000)
            {
                govtA.ManageArabDebt(-double.Parse(inputField.text) * 1_000_000_000);
                // repay button shows up as pressed, take button shows up as released
            }
        }
    }

    public void TakeFromLocal()
    {
        if(inputField.text != "")
        {
            govtL.GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtLimit / 1_000_000_000)
            {
                govtL.ManageLocalDebt(double.Parse(inputField.text) * 1_000_000_000);
                // take button shows up as pressed, repay button shows up as released
            }
        }
    }

    public void PayToLocal()
    {
        if(inputField.text != "")
        {
            govtL.GetLocalDebtDetails(out double debtOwed, out double interestRate, out double debtLimit, out bool allowed);
            if ((double.Parse(inputField.text)) <= debtOwed / 1_000_000_000)
            {
                govtL.ManageLocalDebt(-double.Parse(inputField.text) * 1_000_000_000);
                // repay button shows up as pressed, take button shows up as released
            }
        }
    }

    public void repaybtnSelected()
    {

       Color targetColor = new Color(0, 128, 0); 

       if (repay.GetComponent<Button>().colors.selectedColor == targetColor)
        {
            take.GetComponent<Button>().interactable = true;
            var colors = repay.GetComponent<Button>().colors;
            colors.selectedColor = Color.white; 
            colors.normalColor = Color.white; 
            repay.GetComponent<Button>().colors = colors;
        }
        else
        {
            take.GetComponent<Button>().interactable = false;
            var colors = repay.GetComponent<Button>().colors;
            colors.selectedColor = targetColor;
            colors.normalColor = targetColor;
            repay.GetComponent<Button>().colors = colors;
        }
    }

   public void takebtnSelected()
    {
        Color targetColor = new Color(255, 0, 0); 

        

        if (take.GetComponent<Button>().colors.selectedColor == targetColor)
        {
            repay.GetComponent<Button>().interactable = true;
            var colors = take.GetComponent<Button>().colors;
            colors.selectedColor = Color.white; 
            colors.normalColor = Color.white;
            take.GetComponent<Button>().colors = colors;
        }
        else
        {
            repay.GetComponent<Button>().interactable = false;
            var colors = take.GetComponent<Button>().colors;
            colors.selectedColor = targetColor;
            colors.normalColor = targetColor;
            take.GetComponent<Button>().colors = colors;
        }
    }

}
