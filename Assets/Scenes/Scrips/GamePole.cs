using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePole : MonoBehaviour
{

    public GameObject eLiters, eNums, ePole;

    GameObject[] Liters;
    GameObject[] Nums;
    GameObject[,] Pole;

    int lengPole = 10;

    void CreatePole()
    {
        Vector3 StartPoze = transform.position;

        float XX = StartPoze.x + 1;
        float YY = StartPoze.y - 1;

        Liters = new GameObject[lengPole];
        Nums = new GameObject[lengPole];

        Pole = new GameObject[10, 10];

        for (int Nadpis = 0; Nadpis < lengPole; Nadpis++)
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

        for (int Y = 0; Y < lengPole; Y++)
        {
            for (int X = 0; X < lengPole; X++)
            {
                Pole[X, Y] = Instantiate(ePole);
                Pole[X, Y].GetComponent<Chanks>().index = 0;
                Pole[X, Y].transform.position = new Vector3(XX, YY, StartPoze.z);
                Pole[X, Y].GetComponent<ClickPole>().whoPerent = this.gameObject;
                Pole[X, Y].GetComponent<ClickPole>().coordX = X;
                Pole[X, Y].GetComponent<ClickPole>().CoordY = Y;
                XX++;
            }
            XX = StartPoze.x + 1;
            YY--;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePole();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhoClick(int X, int Y)
    {
        if(TestEnterDeck(X, Y))
        {
            Pole[X, Y].GetComponent<Chanks>().index = 1;
        }
    }

    bool TestEnterDeck(int X, int Y)
    {
        if((X > -1) && (Y > -1) && (X < 10) && (Y < 10)) {
            int[] XX = new int[9], YY = new int[9];

            XX[0] = X + 1; XX[1] = X;       XX[2] = X - 1;
            YY[0] = Y + 1; YY[1] = Y + 1;   YY[2] = Y + 1;

            XX[3] = X + 1;  XX[4] = X; XX[5] = X - 1;
            YY[3] = Y;      YY[4] = Y; YY[5] = Y;

            XX[6] = X + 1; XX[7] = X;       XX[8] = X - 1;
            YY[6] = Y - 1; YY[7] = Y - 1;   YY[8] = Y - 1;


            for (int I = 0; I < 9; I++)
            {
                if((XX[I] > -1) && (YY[I] > -1) && (XX[I] < 10) && (YY[I] < 10))
                {
                    if(Pole[XX[I], YY[I]].GetComponent<Chanks>().index != 0)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        return false;
    }
}
