using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour
{
    // Start is called before the first frame update

    public Text turnCounterText;

    void Start()
    {
        turnCounterText.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnCounter()
    {
        turnCounterText.text = (int.Parse(turnCounterText.text) + 1).ToString();
    }
}
