using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMG;

public class EndTurn : MonoBehaviour
{
    IGame game = Game.GetInstance();
    public Text TurnText;

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
        // make all buttons valid again
    }
}
