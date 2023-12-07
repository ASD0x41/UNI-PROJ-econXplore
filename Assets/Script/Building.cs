using EMG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    IGovtMiscControls govt = Govt.GetInstance();

    public Text buildingtxt;

    // Start is called before the first frame update
    public GameObject[] background;
    int index;
    void Start()
    {
        index = 0;
        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        double buildings = govt.GetAssetDetails() / 5_000_000_000;
        buildingtxt.text = ((int)buildings).ToString();
        if (buildings <= 25)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[0].gameObject.SetActive(true);
        }
        else if (buildings <= 50)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[1].gameObject.SetActive(true);
        }
        else if (buildings <= 50)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[2].gameObject.SetActive(true);
        }
        else if (buildings <= 75)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[3].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[4].gameObject.SetActive(true);
        }




    }

    public void Ppress()
    {
        index += 1;
        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }

    public void Bback()
    {
        index -= 1;
        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }
}
