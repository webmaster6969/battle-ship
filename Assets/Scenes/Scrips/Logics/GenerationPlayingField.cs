using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationPlayingField
{

    // ������ � ������
    protected int Width, Height;
    protected Vector2Int StartPosition;

    // ������ ����� �������� ����
    private List<Cell> ListCell = new List<Cell>();

    public GenerationPlayingField(int width, int height, Vector2Int startPosition)
    {
        ListCell = new List<Cell>();
        Width = width;
        Height = height;
        StartPosition = startPosition;
    }

    public List<Cell> GetListCell() { return ListCell; }

    public void GenerationShipList()
    {
        // ���������� ������� �� ����
        GenerationShip generationShip = new GenerationShip();
        int[,] ships = generationShip.Generation(10);

        // ����������� ������� �� �����
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
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

    // ��������� ������� �����
    public void GenerationPlayingFieldSea()
    {

        // ���������� ������� ������
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                //GameObject cell = Instantiate(eCells);
                //cell.transform.SetParent(this.transform, false);
                //cell.GetComponent<ClickPole>().whoPerent = this.gameObject;
                //cell.GetComponent<ClickPole>().coordX = StartPosition.x + x;
                //cell.GetComponent<ClickPole>().CoordY = StartPosition.y - y - 1;


                ListCell.Add(
                    new CellEmpty(
                            null,
                            new Vector2Int(StartPosition.x + x, StartPosition.y - y - 1),
                            0
                        )
                    );
            }
        }
    }

    // ��������� ��������
    public void GenerationPlayingFieldSymbol()
    {
        // ���������� �����
        for (int y = 0; y < Height; y++)
        {
           // GameObject cell = Instantiate(eNums);
           // cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(null, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        // ���������� �����
        for (int x = 0; x < Width; x++)
        {
            //GameObject cell = Instantiate(eLiters);
            //cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(null, new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }
}
