using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Cell
{

    // Все типы ячеек

    // Ячейка содержит символы
    public const int CELL_SYMBOL = -1;

    // Пустая ячейка
    public const int CELL_EMPTY = 0;

    // Ячейка с частью коробля
    public const int CELL_SHIP = 1;


    // Ячейка промаха по короблю
    public const int CELL_MISS = 2;

    // Ячейка попадания по короблю
    public const int CELL_HIT = 3;


    // Объяет который находится на сцене
    private GameObject GameObject;

    // Позиция ячейки в пространстве
    private Vector2Int Position;

    // Номер ячейки
    private Vector2Int NumberCell;

    // Статус ячейки
    private int Status;

    // indes Спрайта в gameObject
    private int IndexSprite;

    public Vector2Int GetPosition() { return Position; }

    public Vector2Int GetNumberCell() { return NumberCell; }

    public int GetStatus() { return Status; }

    public int GetIndexSprite() { return IndexSprite; }

    public GameObject GetGameObject() { return GameObject; }

    public void SetStatus(int status) { 
        this.Status = status;
    }

    public void SetIndexSprite(int indexSprite)
    {
        this.IndexSprite = indexSprite;
        this.GameObject.GetComponent<Chanks>().index = indexSprite;
    }

        public Cell(GameObject gameObject, Vector2Int pos, Vector2Int NumberCell, int indexSprite)
    {
        this.Position = pos;
        this.GameObject = gameObject;
        this.GameObject.transform.position = new Vector3(Position.x, Position.y, 0);
        this.NumberCell = NumberCell;
        this.IndexSprite = indexSprite;
        this.GameObject.GetComponent<Chanks>().index = indexSprite;
    }
}
