using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationGame : MonoBehaviour
{

    // Поле клиента
    public GameObject MapGameClient;

    // Поле клиента
    public GameObject MapGameAI;

    // Ход клиента
    public const int STATUS_STEP_CLIENT = 1;

    public const int STATUS_STEP_AI = 2;

    // Статус игры
    public int Status;


    // Start is called before the first frame update
    void Start()
    {
        Status = STATUS_STEP_CLIENT;
    }

    // Update is called once per frame
    void Update()
    {
        if(Status == STATUS_STEP_CLIENT)
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
        }
        
    }
}
