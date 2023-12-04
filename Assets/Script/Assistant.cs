using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assistant : MonoBehaviour
{
     public Text AssistantText;
     public Text turnCounterText;
    void Start()
    {
        AssistantText.text = "Hello, I am your assistant. I will help you with Economy Management.";
    }

    // Update is called once per frame
    void Update()
    {
        if(turnCounterText.text == "2")
        {
            AssistantText.text = "You have 5 turns to complete the task.";
        }
        else if (turnCounterText.text == "3")
        {
            AssistantText.text = "You have 4 turns to complete the task.";
        }
        else if (turnCounterText.text == "4")
        {
            AssistantText.text = "You have 3 turns to complete the task.";
        }
    }
}
