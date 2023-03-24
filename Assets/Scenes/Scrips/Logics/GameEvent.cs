using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : IObserver
{

    protected GameObject playingField;

    protected string type;

    public const int STATUS_NOT_STEP_MADE = 0;
    public const int STATUS_STEP_MADE = 1;

    protected int Status = STATUS_NOT_STEP_MADE;

    public void SetPlayingField(GameObject playingField) { 
        this.playingField = playingField;
    }

    public void SetStatus(int Status)
    {
        this.Status = Status;
    }

    public virtual int UpdateGame()
    {
        return Status;
    }

    public virtual void WhoClick(int x, int y)
    {
        
    }

    string IObserver.GetType()
    {
        return type;
    }

    public virtual void Update(DataObserver data)
    {
        throw new System.NotImplementedException();
    }
}