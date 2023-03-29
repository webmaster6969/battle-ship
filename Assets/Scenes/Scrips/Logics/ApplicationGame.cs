using UnityEngine;

// Основной объект приложения который следит за всеми модулями
public class ApplicationGame : MonoBehaviour
{
    // Игровое поле клиента
    public PlayingField PlayingFieldClient;

    // Игровое поле AI
    public PlayingField PlayingFieldAI;

    // Управление подписками
    public GameObject EventManager;

    // Управление логикой и состоянием поля
    public CoreLogic coreLogic;

    // Start is called before the first frame update
    private void Start()
    {
        // Устанавливаем состояние полученое от сервера клиенту
        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);

        // Устанавливаем состояние полученое от сервера боту
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);
        EventManager.GetComponent<EventManager>().Attach(new GameEvent(this));
    }

    public void WhoClickClient(int x, int y)
    {
    }

    // Удар по AI полю
    public void WhoClickAI(int x, int y)
    {
        // Устанавливаем стейт при котором пользователь не сможет взаимодействовать с полем, будет ждать ответа сервера
        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepAI(this)));

        // Отправляем координаты нажатия в доступную логику
        coreLogic.WhoClick(x, y);

        // Устанавливаем состояние полученое от сервера клиенту
        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);

        // Устанавливаем состояние полученое от сервера боту
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);

        // Устанавливаем состояние на нажатие от клиента
        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepClient(this)));
    }

    // Update is called once per frame
    private void Update()
    {
    }
}