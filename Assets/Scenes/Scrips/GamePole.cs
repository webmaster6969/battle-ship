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
        Pole[X, Y].GetComponent<Chanks>().index = 1;
    }
}
