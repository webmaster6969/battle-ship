using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSymbol : Cell
{
    public CellSymbol(GameObject gameObject, Vector2Int pos, Vector2Int NumberCell, int indexSprite) : base(gameObject, pos, NumberCell, indexSprite)
    {
        this.SetStatus(CELL_SYMBOL);
    }
}