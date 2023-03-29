using Nakama;
using Nakama.TinyJson;
using Satori;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataPointGrid
{
    public bool debug;
    public int[] botGrid;
    public int[] clientGrid;
    public int Status;
}


public class ConnectMatch : MonoBehaviour
{

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

    public async void Connect()
    {
        _connection = Scene01MainMenuController.getConnect();
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
                DataPointGrid data = JsonUtility.FromJson<DataPointGrid>(stateJson);
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
