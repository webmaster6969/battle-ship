using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellShip : Cell
{
    public CellShip(GameObject gameObject, Vector2Int pos, Vector2Int NumberCell, int indexSprite) : base(gameObject, pos, NumberCell, indexSprite)
    {
        this.SetStatus(CELL_EMPTY);
    }
}
