using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class F_actors : MonoBehaviour
{ 
   
    public InputField inputField;
    private string input;

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
        
    }

    public void TakeFromChina()
    {
        input = inputField.text;
        Debug.Log("Debt Taken " + input);
    }

    public void PayToChina()
    {
        input = inputField.text;
        Debug.Log("Debt Paid " + input);
    }
}
