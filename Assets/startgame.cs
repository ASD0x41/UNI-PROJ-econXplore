using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMG;

public class startgame : MonoBehaviour
{



    IGame game = Game.GetInstance();

    public void NewGame()
    {
        game.NewGame();
    }

    public void Continue()
    {
        game.Continue();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
