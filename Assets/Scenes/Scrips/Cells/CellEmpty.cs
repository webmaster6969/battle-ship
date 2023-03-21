using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEmpty : Cell
{
    public CellEmpty(GameObject gameObject, Vector2Int pos, int indexSprite) : base(gameObject, pos, indexSprite)
    {
        this.SetStatus(CELL_EMPTY);
    }
}
