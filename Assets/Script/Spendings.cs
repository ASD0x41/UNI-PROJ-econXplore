using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spendings : MonoBehaviour
{ 

    public InputField W_SpendingInput;

    public InputField D_SpendingInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void W_Spending()
    {
        float W_SpendingInputValue = float.Parse(W_SpendingInput.text);
        Debug.Log(W_SpendingInputValue);
    }

    public void D_Spending()
    {
        float D_SpendingInputValue = float.Parse(D_SpendingInput.text);
        Debug.Log(D_SpendingInputValue);
    }

}
