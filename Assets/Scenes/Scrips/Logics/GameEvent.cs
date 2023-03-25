using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : IObserver
{

    protected ApplicationGame ApplicationGame;
    protected IState State;

    protected string type;

    public GameEvent(ApplicationGame ApplicationGame)
    {
        this.ApplicationGame = ApplicationGame;
        type = "GameEvents";
        State = new StepClient(ApplicationGame);
    }


    string IObserver.GetType()
    {
        return type;
    }

    public virtual void Update(DataObserver data)
    {
        if(data.TypeMessage == DataObserver.WHO_CLICK)
        {
            Vector2Int v = (Vector2Int)data.Data;
            State.WhoClick(v.x, v.y);
            State = new StepAI(ApplicationGame);
        }
        //throw new System.NotImplementedException();
    }
}