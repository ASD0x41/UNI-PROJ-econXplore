using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;
using UnityEngine.SceneManagement;

public class EndTurn : MonoBehaviour
{
    IGame game = Game.GetInstance();
    public Text TurnText;

    [SerializeField] private InCorrectPopup revoltPopup;
    [SerializeField] private InCorrectPopup bankruptPopup;
    [SerializeField] private InCorrectPopup resignPopup;
    [SerializeField] private InCorrectPopup winPopup;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTurnButton()
    {
        game.EndTurn();
        TurnText.text = game.GetTurn().ToString();
        if (game.CheckVictory())
        {
            winPopup.gameObject.SetActive(true);
            winPopup.Ok_button.onClick.AddListener(YesClicked);
        }
        if (game.CheckRevolt())
        {
            revoltPopup.gameObject.SetActive(true);
            revoltPopup.Ok_button.onClick.AddListener(YesClicked);
        }
        if (game.CheckBankruptcy())
        {
            bankruptPopup.gameObject.SetActive(true);
            bankruptPopup.Ok_button.onClick.AddListener(YesClicked);
        }
    }

    private void YesClicked()
    {
        SceneManager.LoadScene(sceneName);
    }
}