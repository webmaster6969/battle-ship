using Nakama;
using System.Text.RegularExpressions;
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
      //  PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);

        // Устанавливаем состояние полученое от сервера боту
      //  PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);

        // Устанавливаем состояние на нажатие от клиента
       // EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepClient(this)));
    }

    public void ChangeState(GameDataStruct gameDataStruct)
    {
        GameState[,] stateClient = coreLogic.GetGameData().StateClient;
        GameState[,] stateBot = coreLogic.GetGameData().StateAI;
        
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int readX = x;
                int readY = y;

                

                // Установка данных для клиента
                battle.Cell cellClient = gameDataStruct.PlayersGame.PlayerClient.Board.Grid.Cells.Find(c => c.Location.X == x && c.Location.Y == y);
                stateClient[readX, readY].Status = cellClient.Status;


                // Установка данных для AI
                battle.Cell cellAI = gameDataStruct.PlayersGame.PlayerBot.Board.Grid.Cells.Find(c => c.Location.X == x && c.Location.Y == y);
                stateBot[readX, readY].Status = cellAI.Status;

                stateClient[readX, readY].Position = new Vector2Int(readX, readY);
                stateBot[readX, readY].Position = new Vector2Int(readX, readY);

            }
        }

        foreach (Ship ship in gameDataStruct.PlayersGame.PlayerClient.Board.Ships)
        {
            foreach (Location loc in ship.Location)
            {
                if(stateClient[loc.X, loc.Y].Status == Cell.CELL_EMPTY)
                {
                    stateClient[loc.X, loc.Y].Status = Cell.CELL_SHIP;
                }
                
            }
        }

        coreLogic.GetGameData().SetStateClient(stateClient);
        coreLogic.GetGameData().SetStateAI(stateBot);

        // Устанавливаем состояние полученое от сервера клиенту
        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);

        // Устанавливаем состояние полученое от сервера боту
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);

        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepClient(this)));
    }

    // Update is called once per frame
    private void Update()
    {
    }
}