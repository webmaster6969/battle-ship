using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager: ISubject
{

    private List<IObserver> _observers = new List<IObserver>();

    // Методы управления подпиской.
    public void Attach(IObserver observer)
    {
        Console.WriteLine("Subject: Attached an observer.");
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
        Console.WriteLine("Subject: Detached an observer.");
    }

    // Запуск обновления в каждом подписчике.
    public void Notify(string Type, DataObserver data)
    {
        Console.WriteLine("Subject: Notifying observers...");
        
        foreach (var observer in _observers.FindAll(o => o.GetType() == Type))
        {
            observer.Update(data);
        }
    }
}
