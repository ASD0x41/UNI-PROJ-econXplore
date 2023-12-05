using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outflow_Calculation : MonoBehaviour
{ 
    public Text total;
    public Text imports;
    public Text intrest;
    public Text debt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(imports.text != "" && intrest.text != "" && debt.text != "")
        {
            Calculations();
        }   
    }

    void Calculations()
    {
        float.TryParse(imports.text, out float imports_Value);
        float.TryParse(intrest.text, out float intrest_Value);
        float.TryParse(debt.text, out float debt_Value);
      
        total.text = (imports_Value + intrest_Value + debt_Value).ToString();
    }
}
