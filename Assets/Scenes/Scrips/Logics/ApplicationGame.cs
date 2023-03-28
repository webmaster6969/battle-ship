using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ApplicationGame : MonoBehaviour
{
    // Игровое поле клиента
    public PlayingField PlayingFieldClient;

    // Игровое поле AI
    public PlayingField PlayingFieldAI;

    // Управление подписками
    public GameObject EventManager;

    public CoreLogic coreLogic;

    // Start is called before the first frame update
    void Start()
    {
        /*GenerationPlayingField clientGen = new GenerationPlayingField(10, 10, new Vector2Int((int)PlayingFieldClient.transform.position.x, (int)PlayingFieldClient.transform.position.y));
        clientGen.GenerationPlayingFieldSymbol();
        clientGen.GenerationPlayingFieldSea();
        clientGen.GenerationShipList();
        PlayingFieldClient.SetListCell(clientGen.GetListCell());*/

        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);
        EventManager.GetComponent<EventManager>().Attach(new GameEvent(this));
    }

    public void WhoClickClient(int x, int y)
    {
        /*Cell cell = PlayingFieldClient.GetCell(x, y, true);
    
        if (cell != null)
        {
            switch (cell.GetStatus())
            {
                case Cell.CELL_EMPTY:
                    PlayingFieldClient.ChangeCell(Cell.CELL_MISS, cell.GetPosition().x, cell.GetPosition().y);
                    break;
                case Cell.CELL_SHIP:
                    PlayingFieldClient.ChangeCell(Cell.CELL_HIT, cell.GetPosition().x, cell.GetPosition().y);
                    break;
            }
        }

        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepClient(this)));*/
    }

    // Удар по AI полю
    public void WhoClickAI(int x, int y)
    {
        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepAI(this)));
        coreLogic.WhoClick(x, y);
        PlayingFieldClient.SetStateCells(coreLogic.GetGameData().StateClient);
        PlayingFieldAI.SetStateCells(coreLogic.GetGameData().StateAI);
        EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.CHANGE_STATE, new StepClient(this)));
        /*Cell cell = PlayingFieldAI.GetCell(x, y);

        if(cell != null)
        {
            switch (cell.GetStatus()) {
                case Cell.CELL_EMPTY:
                    PlayingFieldAI.ChangeCell(Cell.CELL_MISS, x, y);
                    break;
                case Cell.CELL_SHIP:
                    PlayingFieldAI.ChangeCell(Cell.CELL_HIT, x, y);
                    break;
            }
        }*/

        //this.WhoClickClient(Random.Range(0, 10), Random.Range(0, 10));


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
