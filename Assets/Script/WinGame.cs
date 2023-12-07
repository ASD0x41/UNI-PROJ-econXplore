using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using EMG;

public class WinGame : MonoBehaviour
{
    // Start is called before the first frame update
    
    public string sceneName;
    
    [SerializeField] private InCorrectPopup savePopup;

    IGame game = Game.GetInstance();


   
     public void SaveExit()
    {

        game.Exit();
        // SAVE & EXIT
        Debug.Log("Almost done!");
        savePopup.gameObject.SetActive(true);
        savePopup.Ok_button.onClick.AddListener(YesClicked);
    }
    private void YesClicked()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
