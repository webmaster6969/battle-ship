using UnityEngine;

// Обработка игровых событий
public class GameEvent : IObserver
{
    // Основной объект приложения который следит за всеми модулями
    protected ApplicationGame ApplicationGame;

    // Текущее состяние
    protected IState State;

    // Тип слушателя
    protected string type;

    // Создания объекта
    public GameEvent(ApplicationGame ApplicationGame)
    {
        this.ApplicationGame = ApplicationGame;
        type = "GameEvents";
        State = new StepClient(ApplicationGame);
    }


    string IObserver.GetType()
    {
        return type;
    }

    // Метод реакция на подписку и на событие
    public virtual void Update(DataObserver data)
    {
        // Реакция на нажатие на ячейку на поле
        if(data.TypeMessage == DataObserver.WHO_CLICK)
        {
            Vector2Int v = (Vector2Int)data.Data;
            State.WhoClick(v.x, v.y);
        }

        // Реакция на смену состояния
        if (data.TypeMessage == DataObserver.CHANGE_STATE)
        {
            SetState((IState)data.Data);
        }

        //throw new System.NotImplementedException();
    }

    public IState GetState()
    {
        return this.State;
    }

    public void SetState(IState State)
    {
        this.State = State;
    }
}