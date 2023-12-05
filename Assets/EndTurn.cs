using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EMG;

public class EndTurn : MonoBehaviour
{
    IGame game = Game.GetInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        game.EndTurn();
    }
}
