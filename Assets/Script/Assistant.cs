using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assistant : MonoBehaviour
{
     public Text AssistantText;
     public Text turnCounterText;
     public Text health;

    void Start()
    {
        AssistantText.text = "Hello, I am your assistant. I will help you with Economy Management.";
    }

    // Update is called once per frame
    void Update()
    {
       healthnotification();
    }

    public void infoSellAssests()
    {
        AssistantText.text = "Selling Assets helps you gain money fast but your people wont like it";
    }
    
    public void infocricketMatch()
    {
        AssistantText.text = "Seems like your hosting a cricket match.... Your People will love it ";
    }

    public void infoholiday()
    {
        AssistantText.text = "Seems like you've announced a public holiday.... Your People will love it";
    }

    public void InformRaid()
    {
        AssistantText.text = "Seems like you've raided black market... you will get funds for that";
    }



    public void healthnotification()
    { 
        int healthValue = int.Parse(health.text);
        if(healthValue <= 10 && healthValue >= 5)
        {
            AssistantText.text = "Your health is very low, You are about to Lose";
        }
        else if (healthValue <= 30 && healthValue >= 20)
        {
            AssistantText.text = "Your health is around 30% ";
        }
        else if (healthValue <= 50 && healthValue >= 40)
        {
            AssistantText.text = "Your health is about 50%";
        }
        else if (healthValue <= 90 && healthValue >= 80)
        {
            AssistantText.text = "Your health has increased upto 80% GOOD JOB";
        }
 
    }

    public void resetcomment()
    {
        AssistantText.text = "Hello, I am your assistant. I will help you with Economy Management.";
    }

}
