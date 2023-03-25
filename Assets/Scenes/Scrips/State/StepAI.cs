using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAI : IState
{
    protected ApplicationGame ApplicationGame;
    public StepAI(ApplicationGame game)
    {
        ApplicationGame = game;
    }

    public void WhoClick(int x, int y)
    {
        Debug.Log("StepAI WhoClick");
        ApplicationGame.WhoClickClient(x, y);
    }
}
