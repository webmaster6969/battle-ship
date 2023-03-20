using System.Collections.Generic;
using UnityEngine;

public class GamePole : MonoBehaviour
{
    // Наборы символов и ячеек для отображения
    public GameObject eLiters, eNums, ePole;

    // Какие корабли ставим на поле
    public int[] ShipCount = { 0, 4, 3, 2, 1 };

    private GameObject[,] Cells;

    // Длина ячеек по вертикале и горизонтале
    private int lengCells = 10;

    // Список частей коробля
    private List<Ship> ListShip = new List<Ship>();

    // Массивы символов и ячеек для отображения
    private GameObject[] Liters;
    private GameObject[] Nums;

    // Общий подчет, сколько палуб живо на данном полотне
    public int LifeShip()
    {
        int countLife = 0;

        foreach (Ship Test in ListShip)
        {
            foreach (TestCoord Paluba in Test.ShipCoord)
            {
                int TestBlock = Cells[Paluba.X, Paluba.Y].GetComponent<Chanks>().index;
            }
        }

        return countLife;
    }

    public void WhoClick(int X, int Y)
    {
        /*if(TestEnterDeck(X, Y))
        {
            Pole[X, Y].GetComponent<Chanks>().index = 1;
        }*/

        Shoot(X, Y);
    }

    // Очищаем полотно
    private void ClearPole()
    {
        ShipCount = new int[] { 0, 4, 3, 2, 1 };
        ListShip.Clear();

        for (int X = 0; X < lengCells; X++)
        {
            for (int Y = 0; Y < lengCells; Y++)
            {
                Cells[X, Y].GetComponent<Chanks>().index = 0;
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

    // Создаем основной полотно
    private void CreatePole()
    {
        // От какой точки будем отолкиваться для создания полотна
        Vector3 StartPoze = transform.position;

        float XX = StartPoze.x + 1;
        float YY = StartPoze.y - 1;

        // Выделяем память для символов
        Liters = new GameObject[lengCells];
        Nums = new GameObject[lengCells];

        // Выделяем память для ячеек
        Cells = new GameObject[10, 10];

        // Расставляем символы
        for (int Nadpis = 0; Nadpis < lengCells; Nadpis++)
        {
            Liters[Nadpis] = Instantiate(eLiters);
            Liters[Nadpis].transform.position = new Vector3(XX, StartPoze.y, StartPoze.z);
            Liters[Nadpis].GetComponent<Chanks>().index = Nadpis;
            XX++;

            Nums[Nadpis] = Instantiate(eNums);
            Nums[Nadpis].transform.position = new Vector3(StartPoze.x, YY, StartPoze.z);
            Nums[Nadpis].GetComponent<Chanks>().index = Nadpis;
            YY--;
        }

        XX = StartPoze.x + 1;
        YY = StartPoze.y - 1;

        // Раставляем основные ячейки
        for (int Y = 0; Y < lengCells; Y++)
        {
            for (int X = 0; X < lengCells; X++)
            {
                Cells[X, Y] = Instantiate(ePole);
                Cells[X, Y].GetComponent<Chanks>().index = 0;
                Cells[X, Y].transform.position = new Vector3(XX, YY, StartPoze.z);
                Cells[X, Y].GetComponent<ClickPole>().whoPerent = this.gameObject;
                Cells[X, Y].GetComponent<ClickPole>().coordX = X;
                Cells[X, Y].GetComponent<ClickPole>().CoordY = Y;
                XX++;
            }
            XX = StartPoze.x + 1;
            YY--;
        }
    }

    // Устанавливаем корабль с текущей клетки
    private bool EnterDeck(int ShipType, int Direct, int X, int Y)
    {

        // Пытаемся устанавить все палубы в клетки
        TestCoord[] P = TestEnterShip(ShipType, Direct, X, Y);


        // Если установка удачна, значит заполняем основной массив и меняем значения ячеек на другие спрайты и статусы
        if (P != null)
        {

            // Перебираем все ячейки и ставим статус который меняет так же спрайт
            foreach (TestCoord T in P)
            {
                Cells[T.X, T.Y].GetComponent<Chanks>().index = 1;
            }

            Ship Deck;

            Deck.ShipCoord = P;

            ListShip.Add(Deck);

            return true;
        }

        return false;
    }

    // Ставим коробли на ячейки
    private void EnterRandomShip()
    {
        // Очищаем ячейки
        ClearPole();
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

    // Реакция на выстрел
    private bool Shoot(int X, int Y)
    {
        // Выбираем ячейку
        GameObject cellSelect = Cells[X, Y];

        // Выбираем ячейку по которому сделан выстрел
        int PoleSelect = cellSelect.GetComponent<Chanks>().index;

        // Устанавливаем результат в лож
        bool Result = false;

        // Анализируем и задаем статус ячейки в зависемости от типа ячейки
        switch (PoleSelect)
        {
            case 0: // Пустая ячейка, ставим промах
                cellSelect.GetComponent<Chanks>().index = 2;
                Result = false;
                break;

            case 1: // На поле есть часть коробля, ставим попадание в ячейку
                cellSelect.GetComponent<Chanks>().index = 3;
                Result = true;

                // Анализиуем погиб ли корабль или нет
                if (TestShoot(X, Y))
                {
                }
                else
                {
                }
                break;
        }

        return Result;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Чистим ячейки (ставим на изначальное значение)
        CreatePole();

        // Создаем корабли
        EnterRandomShip();
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
                    if (Cells[XX[I], YY[I]].GetComponent<Chanks>().index != 0)
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
    private TestCoord[] TestEnterShip(int ShipType, int Direct, int X, int Y)
    {
        TestCoord[] ResultCoord = new TestCoord[ShipType];

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
    private TestCoord[] TestEnterShipDirect(int ShipType, int XD, int YD, int X, int Y)
    {
        // Выделяем память под конкретный типа карабля
        TestCoord[] ResultCoord = new TestCoord[ShipType];


        // Проходим по всем ячейкам
        for (int P = 0; P < ShipType; P++)
        {
            // Если ячейка свободна, тогда ставим в нее часть коробля
            if (TestEnterDeck(X, Y))
            {
                ResultCoord[P].X = X;
                ResultCoord[P].Y = Y;
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

    // Проверка на убийство коробля
    private bool TestShoot(int X, int Y)
    {
        bool Result = false;

        // Идем по всем короблям
        foreach (Ship Test in ListShip)
        {

            // Идем по конкретному кораблю
            foreach (TestCoord Paluba in Test.ShipCoord)
            {

                // Если клетка которая подверглась выстрелу содержит корабль, тогда мы пытаемся узнать жив ли корабль
                if ((Paluba.X == X) && (Paluba.Y == Y))
                {
                    int CountKill = 0;
                    // Проверяем Выбранный корабль на все мертвые точки
                    foreach (TestCoord KillPaluba in Test.ShipCoord)
                    {
                        int TestBlock = Cells[KillPaluba.X, KillPaluba.Y].GetComponent<Chanks>().index;

                        if (TestBlock == 3)
                        {
                            CountKill++;
                        }
                    }


                    // Если мервых точек столько же, сколько палуб у коробля, тогда корабль признаем мертвым
                    if (CountKill == Test.ShipCoord.Length)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
            }
        }

        return Result;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // Части коробля
    private struct Ship
    {
        public TestCoord[] ShipCoord;
    }

    // Коодинаты части коробля
    private struct TestCoord
    {
        public int X, Y;
    }
}