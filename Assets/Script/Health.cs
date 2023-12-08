using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class Health : MonoBehaviour
{
    IGovtMiscControls govt = Govt.GetInstance();

    public Text healthtxt;

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
        double health = govt.GetAssetDetails() / 5_000_000_000;
        healthtxt.text = ((int)health).ToString();

        if (health <= 10)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[0].gameObject.SetActive(true);
        }
        else if (health <= 20)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[1].gameObject.SetActive(true);
        }
        else if(health <= 30)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[2].gameObject.SetActive(true);
        }
        else if(health <= 40)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[3].gameObject.SetActive(true);
        }
        else if(health <= 50)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[4].gameObject.SetActive(true);
        }
        else if(health <= 60)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[5].gameObject.SetActive(true);
        }
        else if(health <= 70)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[6].gameObject.SetActive(true);
        }
        else if(health <= 80)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[7].gameObject.SetActive(true);
        }
        else if (health <= 90)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[8].gameObject.SetActive(true);
        }
        else if (health <= 100)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[9].gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
            }
            background[10].gameObject.SetActive(true);
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
        //Debug.Log(index);
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
