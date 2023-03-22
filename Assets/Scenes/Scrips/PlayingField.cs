using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField: MonoBehaviour
{

    // ������ ����� �������� ����
    private List<Cell> ListCell = new List<Cell>();

    // ������ � ������
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

        GenerationShip generationShip = new GenerationShip();
        int[,] ships = generationShip.Generation(10);

        // �������� ��������� �������
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

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

        if (this.GetComponent<GameEvent>() != null)
        {
            this.GetComponent<GameEvent>().SetPlayingField(this);
        }

    }

    // ������� ��������, ���� � ����� ��� �����������
    public GameObject eLiters, eNums, eCells;

    // ��������� ������� �����
    public void GenerationPlayingFieldSea()
    {
        // �������� ��������� �������
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // ���������� ������� ������
        for (int x = 0; x < Width; x++) 
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject cell = Instantiate(eCells);
                cell.transform.SetParent(this.transform, false);
                cell.GetComponent<ClickPole>().whoPerent = this.gameObject;
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

    // ��������� ��������
    public void GenerationPlayingFieldSymbol()
    {
        // �������� ��������� �������
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // ���������� �����
        for (int y = 0; y < Height; y++)
        {
            GameObject cell = Instantiate(eNums);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        // ���������� �����
        for (int x = 0; x < Width; x++)
        {
            GameObject cell = Instantiate(eLiters);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }

    // ������� �� ������� �� ����
    public void WhoClick(int x, int y)
    {
        Debug.Log("Send WhoClick X: " + x + " Y: " + y);

        if(this.GetComponent<GameEvent>() != null)
        {
            this.GetComponent<GameEvent>().WhoClick(x, y);
        }
        
    }
}
