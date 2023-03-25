using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepClient : IState
{

    protected ApplicationGame ApplicationGame;

    public StepClient(ApplicationGame game)
    {
        ApplicationGame = game;
    }

    public void WhoClick(int x, int y)
    {
        Debug.Log("StepClient WhoClick");
        ApplicationGame.WhoClickAI(x, y);
    }
}
