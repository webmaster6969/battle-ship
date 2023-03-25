using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayingField: MonoBehaviour
{

    // Массивы символов, цифр и ячеек для отображения
    public GameObject eLiters, eNums, eCells;

    public string TypePlayingField;

    // Массив ячеек игрового поля
    private List<Cell> ListCell = new List<Cell>();

    // Ширина и высота
    public int Width, Height;

    /*public PlayingField(int width, int height) { 
        this.Width = width;
        this.Height = height;
    }*/


    public List<Cell> GetListCell() { return ListCell; }

    public void Start()
    {
        this.GenerationPlayingFieldSea();
        this.GenerationPlayingFieldSymbol();

        // Генерируем корабли на поле
        GenerationShip generationShip = new GenerationShip();
        int[,] ships = generationShip.Generation(10);

        // Получаем стартовую позицию
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // Расставляем коробли на доску
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int readX = StartPosition.x + x;
                int readY = StartPosition.y - y - 1;
                Cell cell = ListCell.Find(cell => cell.GetPosition().x == readX && cell.GetPosition().y == readY && cell.GetStatus() == Cell.CELL_EMPTY);
                if (cell != null && ships[x, y] == 1)
                {
                    cell.SetStatus(Cell.CELL_SHIP);
                    cell.SetIndexSprite(Cell.CELL_SHIP);
                }
                
            }
        }

    }

    // Генерация игровых ячеек
    public void GenerationPlayingFieldSea()
    {
        // Получаем стартовую позицию
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // Генерируем игровые ячейки
        for (int x = 0; x < Width; x++) 
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject cell = Instantiate(eCells);
                cell.transform.SetParent(this.transform, false);
                //cell.GetComponent<ClickPole>().whoPerent = this.gameObject;
                cell.GetComponent<ClickPole>().coordX = StartPosition.x + x;
                cell.GetComponent<ClickPole>().CoordY = StartPosition.y - y - 1;


                ListCell.Add(
                    new CellEmpty(
                            cell,
                            new Vector2Int(StartPosition.x + x, StartPosition.y - y - 1),
                            0
                        )
                    );
            }
        }
    }

    // Генерация надписей
    public void GenerationPlayingFieldSymbol()
    {
        // Получаем стартовую позицию
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // Генерируем цифры
        for (int y = 0; y < Height; y++)
        {
            GameObject cell = Instantiate(eNums);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        // Генерируем буквы
        for (int x = 0; x < Width; x++)
        {
            GameObject cell = Instantiate(eLiters);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }

    // Изменение в ячейки
    public void ChangeCell(int Status, int x, int y)
    {
        Vector2Int CellPos = GetRealCoordinateCell(x, y);
        Debug.Log("HittingCell X: " + CellPos.x + " Y: " + CellPos.y);

        Cell cell = ListCell.Find(cell => cell.GetPosition().x == CellPos.x && cell.GetPosition().y == CellPos.y && cell.GetStatus() == Cell.CELL_EMPTY);
        if (cell != null)
        {
            cell.SetStatus(Status);
            cell.SetIndexSprite(Status);
        }

    }

    // Поулчение ячейки
    public Cell GetCell(int x, int y)
    {
        

        Vector2Int CellPos = GetRealCoordinateCell(x, y);
        Debug.Log("HittingCell X: " + CellPos.x + " Y: " + CellPos.y);

        return ListCell.Find(cell => cell.GetPosition().x == CellPos.x && cell.GetPosition().y == CellPos.y);
    }

    // Получение реальных координат ячейки
    private Vector2Int GetRealCoordinateCell(int x, int y)
    {
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        int readX = StartPosition.x + x;
        int readY = StartPosition.y - y - 1;

        return new Vector2Int(readX, readY);
    }
}
