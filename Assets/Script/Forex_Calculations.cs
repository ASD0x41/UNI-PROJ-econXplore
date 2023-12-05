using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forex_Calculations : MonoBehaviour
{   
    
    public Text OldForex;
    public Text NewForex;
    public Text Net;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OldForex.text = "3123";
        NewForex.text = "7463";
        Net.text = "23123";
    }
}
