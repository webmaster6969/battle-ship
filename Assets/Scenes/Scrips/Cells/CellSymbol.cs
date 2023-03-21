using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSymbol : Cell
{
    public CellSymbol(GameObject gameObject, Vector2Int pos, int indexSprite) : base(gameObject, pos, indexSprite)
    {
        this.SetStatus(CELL_SYMBOL);
    }
}