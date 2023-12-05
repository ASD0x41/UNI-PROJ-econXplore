using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        float.TryParse(exports.text, out float exports_Value);
        float.TryParse(loans.text, out float loans_Value);
        float.TryParse(Assests.text, out float Assests_Value);
      
        total.text = (exports_Value + loans_Value + Assests_Value).ToString();
    }
}
