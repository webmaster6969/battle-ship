using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ApplicationGame : MonoBehaviour
{

    // Игровые события
    public GameEvent GameEvent;

    // Игровое поле клиента
    public PlayingField PlayingFieldClient;

    // Игровое поле AI
    public PlayingField PlayingFieldAI;

    // Ход клиента
    public const int STATUS_STEP_CLIENT = 1;

    public const int STATUS_STEP_AI = 2;

    // Управление подписками
    public GameObject EventManager;

    // Статус игры
    public int Status;


    // Start is called before the first frame update
    void Start()
    {
        GameEvent = new GameEvent(this);
        EventManager.GetComponent<EventManager>().Attach(GameEvent);
        Status = STATUS_STEP_CLIENT;
    }

    public void WhoClickClient(int x, int y)
    {
        Cell cell = PlayingFieldClient.GetCell(x, y);

        if (cell != null)
        {
            switch (cell.GetStatus())
            {
                case Cell.CELL_EMPTY:
                    PlayingFieldClient.ChangeCell(Cell.CELL_MISS, x, y);
                    break;
            }
        }
    }

    public void WhoClickAI(int x, int y)
    {
        Cell cell = PlayingFieldAI.GetCell(x, y);

        if(cell != null)
        {
            switch (cell.GetStatus()) {
                case Cell.CELL_EMPTY:
                    PlayingFieldAI.ChangeCell(Cell.CELL_MISS, x, y);
                    break;
            }
        }

        
      //  MapGameAI.GetComponent<GameEvent>().SetStatus(GameEvent.STATUS_NOT_STEP_MADE);
        //EventManager.Notify(Type, new DataObserver(DataObserver.WHO_CLICK, new Vector2Int(x, y)));
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
