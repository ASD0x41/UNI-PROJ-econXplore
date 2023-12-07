using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resetButtons : MonoBehaviour
{   
    public Button Take1;
    public Button Take2;
    public Button Take3;
    public Button Take4;

    public Button Repay1;
    public Button Repay2;
    public Button Repay3;
    public Button Repay4;

    public Button Dollars;

    public InputField IMFinput;

    public InputField Chinainput;

    public InputField Arabinput;
 
    public InputField Localinput;
    
    public InputField W_develope;

    public InputField D_develope;

    public InputField C_Amount;

    public Button Pkr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetbtn()
    {
        var colors = Take1.GetComponent<Button>().colors;
        colors.selectedColor = Color.white; 
        colors.normalColor = Color.white;
        Take1.GetComponent<Button>().colors = colors;
        Repay1.GetComponent<Button>().interactable = true; 

        colors = Take2.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Take2.GetComponent<Button>().colors = colors;
        Repay2.GetComponent<Button>().interactable = true;

        colors = Take3.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Take3.GetComponent<Button>().colors = colors;
        Repay3.GetComponent<Button>().interactable = true;

        colors = Take4.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Take4.GetComponent<Button>().colors = colors;
        Repay4.GetComponent<Button>().interactable = true;

        colors = Repay1.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Repay1.GetComponent<Button>().colors = colors;
        Take1.GetComponent<Button>().interactable = true;

        colors = Repay2.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Repay2.GetComponent<Button>().colors = colors;
        Take2.GetComponent<Button>().interactable = true;

        colors = Repay3.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Repay3.GetComponent<Button>().colors = colors;
        Take3.GetComponent<Button>().interactable = true;

        colors = Repay4.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Repay4.GetComponent<Button>().colors = colors;
        Take4.GetComponent<Button>().interactable = true;

        colors = Dollars.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Dollars.GetComponent<Button>().colors = colors;
        Pkr.GetComponent<Button>().interactable = true;

        colors = Pkr.GetComponent<Button>().colors;
        colors.selectedColor = Color.white;
        colors.normalColor = Color.white;
        Pkr.GetComponent<Button>().colors = colors;
        Dollars.GetComponent<Button>().interactable = true;

        IMFinput.text = "";
        Chinainput.text = "";
        Arabinput.text = "";
        Localinput.text = "";
        W_develope.text = "";
        D_develope.text = "";
        C_Amount.text = "";

    }
}
