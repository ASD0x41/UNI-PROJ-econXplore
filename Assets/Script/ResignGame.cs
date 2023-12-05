using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResignGame : MonoBehaviour
{
    // Start is called before the first frame update
    
    public string sceneName;
    
    [SerializeField] private InCorrectPopup incorrectPopup;
   
     public void Resigned()
    {
        incorrectPopup.gameObject.SetActive(true);
        incorrectPopup.Ok_button.onClick.AddListener(YesClicked);
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
