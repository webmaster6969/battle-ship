using System;
using UnityEngine;

public class AItGameEvent : GameEvent
{

    public AItGameEvent(string Type)
    {
        this.type = Type;
    }


    public override void WhoClick(int x, int y)
    {

        if(Status != STATUS_NOT_STEP_MADE)
        {
            return;
        }

        PlayingField pf = playingField.GetComponent<PlayingField>();

        Status = STATUS_STEP_MADE;

        Cell ship = pf.GetListCell().Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_SHIP);

        if (ship != null)
        {
            ship.SetStatus(Cell.CELL_HIT);
            ship.SetIndexSprite(Cell.CELL_HIT);
            Debug.Log("Click CELL_SHIP");
        }

        Cell empty = pf.GetListCell().Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_EMPTY);

        if (empty != null)
        {
            empty.SetStatus(Cell.CELL_MISS);
            empty.SetIndexSprite(Cell.CELL_MISS);
            Debug.Log("Click CELL_SHIP");
        }
    }

    public override void Update(DataObserver data)
    {
        if (data.TypeMessage == DataObserver.WHO_CLICK)
        {
            Vector2Int click = (Vector2Int)data.Data;
            this.WhoClick(click.x, click.y);
        }
    }

    public override int UpdateGame()
    {
        //this.GetComponentInParent<ApplicationGame>().MapGameClient.GetComponent<GameEvent>().WhoClick(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
        //this.WhoClick(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
        return Status;
    }
}
