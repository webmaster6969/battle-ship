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


    public void Start()
    {
        this.GenerationPlayingFieldSea();
        this.GenerationPlayingFieldSymbol();
    }

    // ������� ��������, ���� � ����� ��� �����������
    public GameObject eLiters, eNums, eCells;

    // ��������� ������� �����
    public void GenerationPlayingFieldSea()
    {
        // �������� ��������� �������
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // ���������� ������� ������
        for (int y = 0; y < Height; y++) 
        {
            for (int x = 0; x < Width; x++)
            {
                GameObject cell = Instantiate(eCells);
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
            ListCell.Add(new CellSymbol(Instantiate(eNums), new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        // ���������� �����
        for (int x = 0; x < Width; x++)
        {
            ListCell.Add(new CellSymbol(Instantiate(eLiters), new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }

    // ������� �� ������� �� ����
    public void WhoClick(int x, int y)
    {
        Debug.Log("Send WhoClick X: " + x + " Y: " + y);
        Cell ship = ListCell.Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_SHIP);

        if ( ship != null )
        {
            Debug.Log("Click CELL_SHIP");
        }

        Cell empty = ListCell.Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y && cell.GetStatus() == Cell.CELL_EMPTY);

        if (empty != null)
        {
            Debug.Log("Click CELL_SHIP");
        }
    }
}
