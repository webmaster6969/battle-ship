using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayingField: MonoBehaviour
{

    // 
    public GameObject eLiters, eNums, eCells;

    // 
    private List<Cell> ListCell = new List<Cell>();

    // 
    public int Width, Height;

    /*public PlayingField(int width, int height) { 
        this.Width = width;
        this.Height = height;
    }*/

    // 
    public List<Cell> GetListCell() { return ListCell; }

    //public bool generationShip = false;

    public void Start()
    {
        this.GenerationPlayingFieldSea();
        this.GenerationPlayingFieldSymbol();
        this.GenerationShipList();
    }

    public void GenerationShipList()
    {
        // 
        GenerationShip generationShip = new GenerationShip();
        int[,] ships = generationShip.Generation(10);

        // 
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        //
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

    // 
    public void GenerationPlayingFieldSea()
    {
        // 
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);


        // 
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

    // 
    public void GenerationPlayingFieldSymbol()
    {
        // 
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // 
        for (int y = 0; y < Height; y++)
        {
            GameObject cell = Instantiate(eNums);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), y));
        }

        //
        for (int x = 0; x < Width; x++)
        {
            GameObject cell = Instantiate(eLiters);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x + x, StartPosition.y), x));
        }
    }

    // 
    public void ChangeCell(int Status, int x, int y)
    {
        //Vector2Int CellPos = GetRealCoordinateCell(x, y);
       // Debug.Log("HittingCell X: " + CellPos.x + " Y: " + CellPos.y);

        Cell cell = ListCell.Find(cell => cell.GetPosition().x == x && cell.GetPosition().y == y);
        if (cell != null)
        {
            cell.SetStatus(Status);
            cell.SetIndexSprite(Status);
        }

    }

    // 
    public Cell GetCell(int x, int y, bool convertCoord = false)
    {
        int realX = x, realY = y;
        Debug.Log("HittingCell X: " + realX + " Y: " + realY);
        if (convertCoord)
        {
            Vector2Int CellPos = GetRealCoordinateCell(x, y);
            realX = CellPos.x;
            realY = CellPos.y;
            Debug.Log("HittingCell(Convert) X: " + realX + " Y: " + realY);
        }
        
        

        return ListCell.Find(cell => cell.GetPosition().x == realX && cell.GetPosition().y == realY);
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
