using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page1_Variables : MonoBehaviour
{ 
    [SerializeField] private Text ExportsText;
    [SerializeField] private Text SalesText;
    
    [SerializeField] private Text LoansText;
    
    [SerializeField] private Text ImportsText;

    [SerializeField] private Text IntrestText;

    [SerializeField] private Text DebtText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExportsText.text = "9000";
        SalesText.text = "5000";
        LoansText.text = "7000";
        ImportsText.text = "6000";
        IntrestText.text = "3000";
        DebtText.text = "1000";
    }
}
