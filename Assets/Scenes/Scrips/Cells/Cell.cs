using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Cell
{

    // ¬се типы €чеек

    // ячейка содержит символы
    public const int CELL_SYMBOL = -1;

    // ѕуста€ €чейка
    public const int CELL_EMPTY = 0;

    // ячейка с частью коробл€
    public const int CELL_SHIP = 1;


    // ячейка промаха по короблю
    public const int CELL_MISS = 2;

    // ячейка попадани€ по короблю
    public const int CELL_HIT = 3;


    // ќбъ€ет который находитс€ на сцене
    private GameObject GameObject;

    // ѕозици€ €чейки
    private Vector2Int Position;

    // —татус €чейки
    private int Status;

    // indes —прайта в gameObject
    private int IndexSprite;

    public Vector2 GetPosition() { return Position; }

    public int GetStatus() { return Status; }

    public int GetIndexSprite() { return IndexSprite; }

    public GameObject GetGameObject() { return GameObject; }

    public void SetStatus(int status) { 
        this.Status = status;
    }

    public Cell(GameObject gameObject, Vector2Int pos, int indexSprite)
    {
        this.Position = pos;
        this.GameObject = gameObject;
        this.GameObject.transform.position = new Vector3(Position.x, Position.y, 0);
        this.IndexSprite = indexSprite;
        gameObject.GetComponent<Chanks>().index = indexSprite;
    }
}
