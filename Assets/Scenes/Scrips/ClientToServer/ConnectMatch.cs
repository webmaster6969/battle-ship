using Nakama;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Структура ячейки
namespace battle{
    [System.Serializable]
    public struct Cell
    {

        public Location Location;
        public int Status;
    }
}

// Структура лист ячеек
[System.Serializable]
public struct Grid
{

    public List<battle.Cell> Cells;
}

// Структура поля
[System.Serializable]
public struct Board
{

    public int Size;
    public List<Ship> Ships;
    public Grid Grid;
}

// Позиция ячейки
[System.Serializable]
public struct Location
{

    public int X;
    public int Y;
}

// Структура корабля и позиция на поле
[System.Serializable]
public struct Ship
{

    public int Size;
    public Location[] Location;
    public int Hits;
}

// Структура Игрока
[System.Serializable]
public struct Player
{

    public string Name;
    public Board Board;
    public int Hits;
    public int Misses;
}

// Структура игрока
[System.Serializable]
public struct Players
{

    public Player PlayerClient;
    public Player PlayerBot;
}

// Структура игроков
[System.Serializable]
public struct GameDataStruct
{
    public bool Debug;
    public Players PlayersGame;
    public int Status;
}

public class ConnectMatch : MonoBehaviour
{
    // Говорит о том что пришел объект все полей
    const int SendAllGridsOpCode = 2;
    private IMatchmakerTicket _ticket;
    private GameConnection _connection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Создаем матч
    public async void Connect()
    {
        _connection = ManagerNetwork.getConnect();
        _connection.Socket.ReceivedMatchmakerMatched += OnMatchmakerMatched;

        // Join the matchmaker
        try
        {
            //var match = await _connection.Socket.CreateMatchAsync("match");
            _connection.match = await _connection.Socket.RpcAsync("rpc_create_match");
            // var match = await _connection.Socket.JoinMatchAsync(m);
            // Acquires matchmaking ticket used to join a match
            Debug.Log("match: " + _connection.match.Payload);
            /*_ticket = await _connection.Socket.AddMatchmakerAsync(
                query: "*",
                minCount: 2,
                maxCount: 2,
                stringProperties: null,
                numericProperties: null);*/
            await _connection.Socket.JoinMatchAsync(_connection.match.Payload);




            Debug.Log("Get ticket");

        }
        catch (Exception e)
        {
            Debug.LogWarning("An error has occured while joining the matchmaker: " + e);
        }

        _connection.Socket.ReceivedMatchState += matchState =>
        {
            var stateJson = Encoding.UTF8.GetString(matchState.State);

            if (matchState.OpCode == SendAllGridsOpCode)
            {
                GameDataStruct data = JsonUtility.FromJson<GameDataStruct>(stateJson);
                this.GetComponent<ApplicationGame>().ChangeState(data);
                Debug.Log("STATE: " + data);
            }

            Debug.Log("STATE: " + stateJson);
        };


    }

   
    private void OnMatchmakerMatched(IMatchmakerMatched matched)
    {
        _connection.BattleConnection = new BattleConnection(matched);
        _connection.Socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;

        Debug.Log("matchmaker matched called");

        //SceneManager.LoadScene(GameConfigurationManager.Instance.GameConfiguration.SceneNameBattle);
    }
}
