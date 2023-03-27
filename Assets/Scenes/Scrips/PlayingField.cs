using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayingField: MonoBehaviour
{

    // ������� ��������, ���� � ����� ��� �����������
    public GameObject eLiters, eNums, eCells;

    // ������ ����� �������� ����
    private List<Cell> ListCell = new List<Cell>();

    // ������ � ������
    public int Width, Height;

    /*public PlayingField(int width, int height) { 
        this.Width = width;
        this.Height = height;
    }*/

    // ��������� ������ �����
    public List<Cell> GetListCell() { return ListCell; }

    public void SetListCell(List<Cell> l)
    {
        ListCell = l;
    }

    //public bool generationShip = false;

    public void Start()
    {
        //this.GenerationPlayingFieldSymbol();
        //this.GenerationPlayingFieldSea();
        //this.GenerationShipList();
    }

    // ��������� � ������
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

    // ��������� ������
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

    // ��������� �������� ��������� ������
    private Vector2Int GetRealCoordinateCell(int x, int y)
    {
        Vector2Int StartPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        int readX = StartPosition.x + x;
        int readY = StartPosition.y - y - 1;

        return new Vector2Int(readX, readY);
    }
}
