public interface ISubject
{
    // Присоединяет наблюдателя к издателю.
    void Attach(IObserver observer);

    // Отсоединяет наблюдателя от издателя.
    void Detach(IObserver observer);

    // Уведомляет всех наблюдателей о событии.
    void Notify(string Type, DataObserver data);
}