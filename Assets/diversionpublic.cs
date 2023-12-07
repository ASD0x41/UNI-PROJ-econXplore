using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class diversionpublic : MonoBehaviour
{
    public Button raidbtn;
    public Button sportsbtn;
    public Button holidaybtn;
    public Button salebtn;


    IGovtMiscControls govt = Govt.GetInstance();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        govt.GetDiversionDetails(out bool sports, out bool holiday, out bool festival);
        if (sports == false)
        {
            sportsbtn.interactable = false;
        }
        else { sportsbtn.interactable = true; }
        if (holiday == false)
        {
            holidaybtn.interactable = false;
        }
        else { holidaybtn.interactable = true; }

        
        if (govt.GetBlackMarketDetails() == false)
        {
            raidbtn.interactable = false;
        }
        else { raidbtn.interactable = true; }

        // assets?
    }

    public void SellAssets()
    {
        govt.SellAssets(10_000_000_000);
    }

    public void conductRaid()
    {
        govt.RaidBlackMarket();
    }

    public void conductSport()
    {
        PublicEvent sports = SportsContest.GetInstance();
        govt.ConductDiversion(sports);
    }

    public void orderHoliday()
    {
        PublicEvent holiday = PublicHoliday.GetInstance();
        govt.ConductDiversion(holiday);
    }
}
