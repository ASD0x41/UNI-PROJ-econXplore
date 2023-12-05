using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_actors : MonoBehaviour
{ 
   
    public InputField inputField;
    private string input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take()
    {
        input = inputField.text;
        Debug.Log("Debt Taken " + input);
    }

    public void Pay()
    {
        input = inputField.text;
        Debug.Log("Debt Paid " + input);
    }
}
