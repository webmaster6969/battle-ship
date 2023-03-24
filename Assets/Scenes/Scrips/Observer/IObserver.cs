using System;
using UnityEngine;

public struct DataObserver
{
    public const int WHO_CLICK = 1;

    public DataObserver(int TypeMessage, object Data)
    {
        this.Data = Data;
        this.TypeMessage = TypeMessage;
    }

    public object Data { get; set; }
    public int TypeMessage { get; set; }
}

public interface IObserver
{
    string GetType();

    // Получает обновление от издателя
    void Update(DataObserver data);
}