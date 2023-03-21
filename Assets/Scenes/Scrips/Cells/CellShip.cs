using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellShip : Cell
{
    public CellShip(GameObject gameObject, Vector2Int pos, int indexSprite) : base(gameObject, pos, indexSprite)
    {
        this.SetStatus(CELL_EMPTY);
    }
}
