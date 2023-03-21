using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField: MonoBehaviour
{

    // Массив ячеек игрового поля
    private List<Cell> ListCellSea = new List<Cell>();

    // Ширина и высота
    public int Width, Height;

    /*public PlayingField(int width, int height) { 
        this.Width = width;
        this.Height = height;
    }*/


    public void Start()
    {
        this.GenerationPlayingFieldSea();
        this.GenerationPlayingFieldSymbol();
    }

    // Массивы символов, цифр и ячеек для отображения
    public GameObject eLiters, eNums, eCells;

    // Генерация игровых ячеек
    public void GenerationPlayingFieldSea()
    {
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        for (int y = 0; y < Height; y++) 
        {
            for (int x = 0; x < Width; x++)
            {
                ListCellSea.Add(
                    new CellEmpty(
                            Instantiate(eCells),
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
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        for (int y = 0; y < Height; y++)
        {
            ListCellSea.Add(new CellSymbol(Instantiate(eNums), new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        for (int x = 0; x < Width; x++)
        {
            ListCellSea.Add(new CellSymbol(Instantiate(eLiters), new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }

    public void WhoClick(int x, int y)
    {
        Cell ship = ListCellSea.Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_SHIP);

        if ( ship != null )
        {

        }

        Cell empty = ListCellSea.Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_EMPTY);

        if (empty != null)
        {

        }
    }
}
