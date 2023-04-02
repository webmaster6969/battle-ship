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
    public CoreLogicNetwork coreLogic;

    // Start is called before the first frame update
    private void Start()
    {
        coreLogic = new CoreLogicNetwork();
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

    public void ChangeState(GameDataStruct gameDataStruct)
    {
        GameState[,] stateClient = new GameState[10, 10];
        GameState[,] stateBot = new GameState[10, 10];
        
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int readX = x;
                int readY = y;

                // Установка данных для клиента
                stateClient[readX, readY].Status = gameDataStruct.PlayersGame.PlayerClient.Board.Grid[x][y];


                // Установка данных для AI
                stateBot[readX, readY].Status = gameDataStruct.PlayersGame.PlayerBot.Board.Grid[x][y];

                stateClient[readX, readY].Position = new Vector2Int(readX, readY);
                stateBot[readX, readY].Position = new Vector2Int(readX, readY);

            }
        }

        // Устанавливаем состояние полученое от сервера клиенту
        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);

        // Устанавливаем состояние полученое от сервера боту
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}