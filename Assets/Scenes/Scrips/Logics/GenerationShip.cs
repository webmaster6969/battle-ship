using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationShip
{

    // ����� ������� ������ �� ����
    public int[] ShipCount = { 0, 4, 3, 2, 1 };

    // ������ �� ����
    private int[,] ListCell;

    // ����� ����� �� ��������� � �����������
    private int lengCells;

    public int[,] Generation(int lengCells)
    {
        this.lengCells = lengCells;
        ListCell = new int[this.lengCells, this.lengCells];
        EnterRandomShip();
        return ListCell;
    }

    // ������� ������
    private void ClearPole()
    {
        ShipCount = new int[] { 0, 4, 3, 2, 1 };

        for (int X = 0; X < lengCells; X++)
        {
            for (int Y = 0; Y < lengCells; Y++)
            {
                ListCell[X, Y] = 0;
            }
        }
    }

    // ������� ���� �� ��� �� ��������������� ������� ��� ���������
    private bool CountShips()
    {
        int Amaunt = 0;

        foreach (int Ship in ShipCount)
        {
            Amaunt += Ship;
        }

        if (Amaunt != 0)
        {
            return true;
        }

        return false;
    }

    // ������������� ������� � ������� ������
    private bool EnterDeck(int ShipType, int Direct, int X, int Y)
    {

        // �������� ���������� ��� ������ � ������
        Vector2Int[] P = TestEnterShip(ShipType, Direct, X, Y);


        // ���� ��������� ������, ������ ��������� �������� ������ � ������ �������� ����� �� ������ ������� � �������
        if (P != null)
        {

            // ���������� ��� ������ � ������ ������ ������� ������ ��� �� ������
            foreach (Vector2Int T in P)
            {
                ListCell[T.x, T.y] = 1;
            }

            return true;
        }

        return false;
    }

    // ������ ������� �� ������
    private void EnterRandomShip()
    {
        // �������� ������� � 4 ��������� �������
        int SelectShip = 4;

        int X, Y;

        // �����������
        int Direct;

        // ����������� �������
        while (CountShips())
        {
            // �������� ���������
            X = Random.Range(0, 10);
            Y = Random.Range(0, 10);

            // �������� �����������
            Direct = Random.Range(0, 2);

            // ���������, ������� �� ������� ��������� � ��������� ����
            if (EnterDeck(SelectShip, Direct, X, Y))
            {
                // ��������� �������� ������, ���� ������ �� ����������
                ShipCount[SelectShip]--;

                // ���� ��� ������� ������� ���� �����������, �������� ��������� ����
                if (ShipCount[SelectShip] == 0)
                {
                    SelectShip--;
                }
            }
        }
    }

    // ��������� �������� ������ ��� ������� � ��� �������
    private bool TestEnterDeck(int X, int Y)
    {
        // ��������� ��� �� �� ����� �� ������� ������
        if ((X > -1) && (Y > -1) && (X < 10) && (Y < 10))
        {
            // �������� ������ ��� ����� ���� ����� ��������� �������
            int[] XX = new int[9], YY = new int[9];

            // ����� ������ ������� ����� ��������� � ����� ����� ���� ������
            XX[0] = X + 1; XX[1] = X; XX[2] = X - 1;
            YY[0] = Y + 1; YY[1] = Y + 1; YY[2] = Y + 1;

            XX[3] = X + 1; XX[4] = X; XX[5] = X - 1;
            YY[3] = Y; YY[4] = Y; YY[5] = Y;

            XX[6] = X + 1; XX[7] = X; XX[8] = X - 1;
            YY[6] = Y - 1; YY[7] = Y - 1; YY[8] = Y - 1;

            // ��������� ��� ������
            for (int I = 0; I < 9; I++)
            {
                // ��������� ��� ������ �� ����� �� ������� ����
                if ((XX[I] > -1) && (YY[I] > -1) && (XX[I] < 10) && (YY[I] < 10))
                {
                    // ��������� ��� ������ �����
                    if (ListCell[XX[I], YY[I]] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        return false;
    }

    // ������������� ������� � ����������� �� ���� � �����������
    private Vector2Int[] TestEnterShip(int ShipType, int Direct, int X, int Y)
    {
        Vector2Int[] ResultCoord = new Vector2Int[ShipType];

        if (TestEnterDeck(X, Y))
        {
            // �������� � ����������� �� ����������
            switch (Direct)
            {
                case 0:

                    // �������� ��������� �������
                    ResultCoord = TestEnterShipDirect(ShipType, 1, 0, X, Y);

                    // ���� �� �����, ������ �� 90 �������� �� ������
                    if (ResultCoord == null)
                    {
                        ResultCoord = TestEnterShipDirect(ShipType, -1, 0, X, Y);
                    }

                    break;

                case 1:
                    ResultCoord = TestEnterShipDirect(ShipType, 0, 1, X, Y);

                    if (ResultCoord == null)
                    {
                        ResultCoord = TestEnterShipDirect(ShipType, 0, -1, X, Y);
                    }

                    break;
            }

            return ResultCoord;
        }

        return null;
    }


    // ��������� ����� �� ��������� ������� � ������ ������
    private Vector2Int[] TestEnterShipDirect(int ShipType, int XD, int YD, int X, int Y)
    {
        // �������� ������ ��� ���������� ���� �������
        Vector2Int[] ResultCoord = new Vector2Int[ShipType];


        // �������� �� ���� �������
        for (int P = 0; P < ShipType; P++)
        {
            // ���� ������ ��������, ����� ������ � ��� ����� �������
            if (TestEnterDeck(X, Y))
            {
                ResultCoord[P].x = X;
                ResultCoord[P].y = Y;
            }
            else
            {
                return null;
            }

            // ��������� � ����������� ������� ���� �������� � ��������� ���������
            X += XD;
            Y += YD;
        }

        return ResultCoord;
    }

}
