using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EMG;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    
    public InputField inputField;

    private string input;

   [SerializeField] private InCorrectPopup incorrectPopup;

    
    
    public void Unlock()
    {
        MyDb mydb = MyDb.GetInstance();
        input = inputField.text;
        Debug.Log("hello, here I am.");
        //if (input == "1234")
        if (mydb.CheckPin(input) == 1)
        {
            Debug.Log("Correct PIN " + input);
            changeScene();
        }
        else
        {
            Debug.Log("Incorrect PIN " + input);
            incorrectPopup.gameObject.SetActive(true);
            incorrectPopup.Ok_button.onClick.AddListener(OKClicked);
        }

    }
    
    private void OKClicked()
    {
        incorrectPopup.gameObject.SetActive(false);
    }
    
    public void Resigned()
    {
        incorrectPopup.gameObject.SetActive(true);
        incorrectPopup.Ok_button.onClick.AddListener(YesClicked);
    }
    private void YesClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    } 
}
