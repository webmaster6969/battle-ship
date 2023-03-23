using System;
using UnityEngine;

public struct DataObserver
{
    public const int WHO_CLICK = 1;


    public object Data { get; set; }
    public double TypeMessage { get; set; }
}

public interface IObserver
{
    string GetType();

    // Получает обновление от издателя
    void Update(DataObserver data);
}