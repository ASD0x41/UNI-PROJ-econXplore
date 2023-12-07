using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Spendings : MonoBehaviour
{ 

    public InputField W_SpendingInput;

    public InputField D_SpendingInput;

    IGovtBudgetControls govt = Govt.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (D_SpendingInput.text != "")
        {
            float D_SpendingInputValue = float.Parse(D_SpendingInput.text);
            govt.AdjustDevelopmentFund(D_SpendingInputValue * 1_000_000_000);
        }
        if (W_SpendingInput.text != "")
        {
            float W_SpendingInputValue = float.Parse(W_SpendingInput.text);
            govt.AdjustWelfareSpending(W_SpendingInputValue * 1_000_000_000);
        }

        
    }

    public void W_Spending()
    {
        float W_SpendingInputValue = float.Parse(W_SpendingInput.text);
        govt.AdjustWelfareSpending(W_SpendingInputValue * 1_000_000_000);
    }

    public void D_Spending()
    {
        float D_SpendingInputValue = float.Parse(D_SpendingInput.text);
        govt.AdjustDevelopmentFund(D_SpendingInputValue * 1_000_000_000);
    }

}
