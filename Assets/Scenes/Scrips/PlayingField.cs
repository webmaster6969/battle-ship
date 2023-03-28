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

    public void Awake()
    {
        this.GenerationPlayingFieldSea();
        this.GenerationPlayingFieldSymbol();
        //this.GenerationShipList();
    }


    public void SetStateCells(GameState[,] state)
    {
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        //
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
                cell.GetComponent<ClickPole>().coordX = x;
                cell.GetComponent<ClickPole>().CoordY = y;


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
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x - 1, StartPosition.y - 1 - y), new Vector2Int(0, y), y));
        }

        //
        for (int x = 0; x < Width; x++)
        {
            GameObject cell = Instantiate(eLiters);
            cell.transform.SetParent(this.transform, false);
            ListCell.Add(new CellSymbol(cell, new Vector2Int(StartPosition.x + x, StartPosition.y), new Vector2Int(x, 0), x));
        }
    }

    // 
    public void ChangeCell(int Status, int x, int y)
    {
        //Vector2Int CellPos = GetRealCoordinateCell(x, y);
       // Debug.Log("HittingCell X: " + CellPos.x + " Y: " + CellPos.y);

        Cell cell = ListCell.Find(cell => cell.GetNumberCell().x == x && cell.GetNumberCell().y == y);
        if (cell != null)
        {
            cell.SetStatus(Status);
            cell.SetIndexSprite(Status);
        }

    }

    // 
    public Cell GetCell(int x, int y, bool convertCoord = false)
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
