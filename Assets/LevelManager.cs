using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    
    public InputField inputField;

    private string input;

   [SerializeField] private InCorrectPopup incorrectPopup;
    
    public void Unlock()
    {
        input = inputField.text;
        if (input == "1234")
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

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    } 
}
