using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationShip
{

    // Какие корабли ставим на поле
    public int[] ShipCount = { 0, 4, 3, 2, 1 };

    // Ячейки на поле
    private int[,] ListCell;

    // Длина ячеек по вертикале и горизонтале
    private int lengCells;

    public int[,] Generation(int lengCells)
    {
        this.lengCells = lengCells;
        ListCell = new int[this.lengCells, this.lengCells];
        EnterRandomShip();
        return ListCell;
    }

    // Очищаем ячейки
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

    // Считаем если ли еще не израсходованные корабли для установки
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

    // Устанавливаем корабль с текущей клетки
    private bool EnterDeck(int ShipType, int Direct, int X, int Y)
    {

        // Пытаемся устанавить все палубы в клетки
        Vector2Int[] P = TestEnterShip(ShipType, Direct, X, Y);


        // Если установка удачна, значит заполняем основной массив и меняем значения ячеек на другие спрайты и статусы
        if (P != null)
        {

            // Перебираем все ячейки и ставим статус который меняет так же спрайт
            foreach (Vector2Int T in P)
            {
                ListCell[T.x, T.y] = 1;
            }

            return true;
        }

        return false;
    }

    // Ставим коробли на ячейки
    private void EnterRandomShip()
    {
        // Начинаем ставить с 4 палубного корабля
        int SelectShip = 4;

        int X, Y;

        // Направление
        int Direct;

        // Расставляем корабли
        while (CountShips())
        {
            // Выбираем случайное
            X = Random.Range(0, 10);
            Y = Random.Range(0, 10);

            // Выбираем направление
            Direct = Random.Range(0, 2);

            // Проверяем, встанет ли корабль правильно в выбранное поле
            if (EnterDeck(SelectShip, Direct, X, Y))
            {
                // Уменьшаем основной массив, пока совсем не закончится
                ShipCount[SelectShip]--;

                // Если все корабли данного типа закончились, выбираем следующий типа
                if (ShipCount[SelectShip] == 0)
                {
                    SelectShip--;
                }
            }
        }
    }

    // Проверяем годность ячейки для вставки в нее корабля
    private bool TestEnterDeck(int X, int Y)
    {
        // Проверяем что мы не вышли за пределы ячейки
        if ((X > -1) && (Y > -1) && (X < 10) && (Y < 10))
        {
            // Выделяем память для места куда хотим поставить корабль
            int[] XX = new int[9], YY = new int[9];

            // Метим ячейки которые хотим заполнить и чтобы между были пустые
            XX[0] = X + 1; XX[1] = X; XX[2] = X - 1;
            YY[0] = Y + 1; YY[1] = Y + 1; YY[2] = Y + 1;

            XX[3] = X + 1; XX[4] = X; XX[5] = X - 1;
            YY[3] = Y; YY[4] = Y; YY[5] = Y;

            XX[6] = X + 1; XX[7] = X; XX[8] = X - 1;
            YY[6] = Y - 1; YY[7] = Y - 1; YY[8] = Y - 1;

            // Проверяем все ячейки
            for (int I = 0; I < 9; I++)
            {
                // Проверяем что ячейка не вышла за пределы поля
                if ((XX[I] > -1) && (YY[I] > -1) && (XX[I] < 10) && (YY[I] < 10))
                {
                    // Проверяем что ячейка пуста
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

    // Устанавливаем корабль в зависимости от типа и направления
    private Vector2Int[] TestEnterShip(int ShipType, int Direct, int X, int Y)
    {
        Vector2Int[] ResultCoord = new Vector2Int[ShipType];

        if (TestEnterDeck(X, Y))
        {
            // Выбираем в зависемости от напрвления
            switch (Direct)
            {
                case 0:

                    // Пытаемся поставить корабль
                    ResultCoord = TestEnterShipDirect(ShipType, 1, 0, X, Y);

                    // Если не вышло, ставим на 90 градусов по другом
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


    // Проверяем можем ли разметить корабль с данной клетки
    private Vector2Int[] TestEnterShipDirect(int ShipType, int XD, int YD, int X, int Y)
    {
        // Выделяем память под конкретный типа карабля
        Vector2Int[] ResultCoord = new Vector2Int[ShipType];


        // Проходим по всем ячейкам
        for (int P = 0; P < ShipType; P++)
        {
            // Если ячейка свободна, тогда ставим в нее часть коробля
            if (TestEnterDeck(X, Y))
            {
                ResultCoord[P].x = X;
                ResultCoord[P].y = Y;
            }
            else
            {
                return null;
            }

            // Ввигаемся в направлении которое было передано в процедуру генерации
            X += XD;
            Y += YD;
        }

        return ResultCoord;
    }

}
