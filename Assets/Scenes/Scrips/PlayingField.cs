using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UnityEngine;

// Основное игровое поле
public class PlayingField: MonoBehaviour
{

    // Набор букв, цифр и клеток
    public GameObject eLiters, eNums, eCells;

    // Набор всех клеток
    private List<Cell> ListCell = new List<Cell>();

    // Ширина и высота поля
    public int Width, Height;

    // Получения набора поля
    public List<Cell> GetListCell() { return ListCell; }

    //public bool generationShip = false;

    public void Awake()
    {
        // Генерация ячеек поля
        this.GenerationPlayingFieldSea();

        // Генерация ячеек символов
        this.GenerationPlayingFieldSymbol();
    }

    // Установка состояния ячеек
    public void SetStateCells(GameState[,] state)
    {
        // Проходим по всему полю
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Cell cell = ListCell.Find(cell => cell.GetNumberCell().x == x && cell.GetNumberCell().y == y);
                if (cell != null)
                {
                    cell.SetStatus(state[x, y].Status);
                    cell.SetIndexSprite(state[x, y].Status);
                }

            }
        }
    }

    // 
    public void GenerationPlayingFieldSea()
    {
        // Откуда начинается отчет установки поля
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // Проходим по всему массиву
        for (int x = 0; x < Width; x++) 
        {
            for (int y = 0; y < Height; y++)
            {
                // Создаем игровой объект
                GameObject cell = Instantiate(eCells);
                // Устанавливаем его пложение в иерархии
                cell.transform.SetParent(this.transform, false);
                //cell.GetComponent<ClickPole>().whoPerent = this.gameObject;
                // Ставим виртуальное положение
                cell.GetComponent<ClickPole>().coordX = x;
                cell.GetComponent<ClickPole>().CoordY = y;

                // Добавляем ячейку к основному состоянию
                ListCell.Add(
                    new CellEmpty(
                            cell,
                            new Vector2Int(StartPosition.x + x, StartPosition.y - y - 1),
                            new Vector2Int(x, y),
                            0
                        )
                    );
            }
        }
    }

    // Генерация символов
    public void GenerationPlayingFieldSymbol()
    {
        // Откуда начинается установки ячеек
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // Ставим цифры в свое положение
        for (int y = 0; y < Height; y++)
        {
            GameObject cell = Instantiate(eNums);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), new Vector2Int(0, y), y));
        }

        // Ставим сиволы в свое положение
        for (int x = 0; x < Width; x++)
        {
            GameObject cell = Instantiate(eLiters);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x + x, StartPosition.y), new Vector2Int(x, 0), x));
        }
    }

    // Меняем состояние ячейки
    public void ChangeCell(int Status, int x, int y)
    {
        Cell cell = ListCell.Find(cell => cell.GetNumberCell().x == x && cell.GetNumberCell().y == y);
        if (cell != null)
        {
            cell.SetStatus(Status);
            cell.SetIndexSprite(Status);
        }

    }

    // Получаем нужную ячейку
    public Cell GetCell(int x, int y)
    {
        return ListCell.Find(cell => cell.GetNumberCell().x == x && cell.GetNumberCell().y == y);
    }

    // 
    private Vector2Int GetRealCoordinateCell(int x, int y)
    {
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        int readX = StartPosition.x + x;
        int readY = StartPosition.y - y - 1;

        return new Vector2Int(readX, readY);
    }
}
