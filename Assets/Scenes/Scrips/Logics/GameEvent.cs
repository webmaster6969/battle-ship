using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{

    protected PlayingField playingField;

    public void SetPlayingField(PlayingField playingField) { 
        this.playingField = playingField;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhoClick(int x, int y)
    {
        Cell ship = playingField.GetListCell().Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_SHIP);

        if (ship != null)
        {
            ship.SetStatus(Cell.CELL_HIT);
            ship.SetIndexSprite(Cell.CELL_HIT);
            Debug.Log("Click CELL_SHIP");
        }

        Cell empty = playingField.GetListCell().Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_EMPTY);

        if (empty != null)
        {
            empty.SetStatus(Cell.CELL_MISS);
            empty.SetIndexSprite(Cell.CELL_MISS);
            Debug.Log("Click CELL_SHIP");
        }
    }
}