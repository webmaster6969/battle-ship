using Nakama.TinyJson;
using UnityEngine;

public class CoreLogicNetwork : CoreLogic
{

    // Инициализация стартовых данных
    public CoreLogicNetwork()
    {
        // Создаем состояния полей
        stateGame = new GameData(10, 10);

        // Установка ячеек
        for (int y = 0; y < 10; y++) 
        {
            for (int x = 0; x < 10; x++)
            {
                int readX = x;
                int readY = y;

                // Установка данных для клиента
                stateGame.StateClient[readX, readY].Status = Cell.CELL_EMPTY;


                // Установка данных для AI
                stateGame.StateAI[readX, readY].Status = Cell.CELL_EMPTY;

                stateGame.StateClient[readX, readY].Position = new Vector2Int(readX, readY);
                stateGame.StateAI[readX, readY].Position = new Vector2Int(readX, readY);

            }
        }
    }

    // Ход клиента
    public override void MoveAI()
    {
        
    }

    // Удар по боту
    public override void WhoClick(int x, int y)
    {
        GameConnection _connection = ManagerNetwork.getConnect();
        _connection.Socket.SendMatchStateAsync(_connection.match.Payload, 1, JsonWriter.ToJson(new Vector2Int(x,y)));
        /*switch (stateGame.StateAI[x, y].GetStatus())
        {
            case Cell.CELL_EMPTY:
                stateGame.StateAI[x, y].SetStatus(Cell.CELL_MISS);
                this.MoveAI();
                break;
            case Cell.CELL_SHIP:
                stateGame.StateAI[x, y].SetStatus(Cell.CELL_HIT);
                break;
        }*/
    }
}
