using Nakama;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Scene01MainMenuController : MonoBehaviour
{

    [SerializeField] static private GameConnection _connection;
    [SerializeField] static private AuthClientToServer authClientToServer;

    public static GameConnection getConnect()
	{
		return _connection;
	}

    private void Awake()
	{
			
	}

	private async void Start()
	{
        authClientToServer = new AuthClientToServer();
        authClientToServer.Init();
        _connection = authClientToServer.getConnect();


    }

}

