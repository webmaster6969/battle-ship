public interface ISubject
{
    // ѕрисоедин€ет наблюдател€ к издателю.
    void Attach(IObserver observer);

    // ќтсоедин€ет наблюдател€ от издател€.
    void Detach(IObserver observer);

    // ”ведомл€ет всех наблюдателей о событии.
    void Notify(string Type);
}