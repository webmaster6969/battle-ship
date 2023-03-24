using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationGame : MonoBehaviour
{

    // Поле клиента
    public ClientGameEvent EventGameClient;

    // Поле клиента
    public AItGameEvent EventGameAI;

    // Ход клиента
    public const int STATUS_STEP_CLIENT = 1;

    public const int STATUS_STEP_AI = 2;

    // Управление подписками
    private EventManager EventManager;

    // Статус игры
    public int Status;


    // Start is called before the first frame update
    void Start()
    {
        EventGameClient = new ClientGameEvent("Client");
        EventGameAI = new AItGameEvent("AI");

        EventManager = new EventManager();
        EventManager.Attach(EventGameClient);
        EventManager.Attach(EventGameAI);
        Status = STATUS_STEP_CLIENT;
    }

    public void WhoClick(string Type, int x, int y)
    {
        EventManager.Notify(Type, new DataObserver(DataObserver.WHO_CLICK, new Vector2Int(x, y)));
    }

    // Update is called once per frame
    void Update()
    {
       
       /* if(Status == STATUS_STEP_CLIENT)
        {
            int StatusClient = MapGameClient.GetComponent<GameEvent>().UpdateGame();

            if(StatusClient == GameEvent.STATUS_STEP_MADE)
            {
                Status = STATUS_STEP_AI;
                MapGameAI.GetComponent<GameEvent>().SetStatus(GameEvent.STATUS_NOT_STEP_MADE);
            }
        } else if (Status == STATUS_STEP_AI)
        {
            int StatusAI = MapGameAI.GetComponent<GameEvent>().UpdateGame();

            if (StatusAI == GameEvent.STATUS_STEP_MADE)
            {
                Status = STATUS_STEP_CLIENT;
                MapGameClient.GetComponent<GameEvent>().SetStatus(GameEvent.STATUS_NOT_STEP_MADE);
            }
        }*/

    }
}
