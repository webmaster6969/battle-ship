using Nakama;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class AuthClientToServer
{
    [SerializeField] private GameConnection _connection;
    static private AuthClientToServer authClientToServer;

    public GameConnection getConnect()
	{
		return _connection;
	}

    public async void Init()
    {

        if (_connection == null || _connection.Session == null)
        {
            string deviceId = GetDeviceId();

            Debug.Log("Start deviceId = " + deviceId);

            if (!string.IsNullOrEmpty(deviceId))
            {
                PlayerPrefs.SetString(GameConstants.DeviceIdKey, deviceId);
            }

            _connection = ScriptableObject.CreateInstance<GameConnection>();
            await InitializeGame(deviceId);
        }
    }

    private async Task InitializeGame(string deviceId)
    {
        var client = new Client("http", "localhost", 7350, "defaultkey", UnityWebRequestAdapter.Instance);
        client.Timeout = 5;

        var socket = client.NewSocket(useMainThread: true);

        string authToken = PlayerPrefs.GetString(GameConstants.AuthTokenKey, null);
        bool isAuthToken = !string.IsNullOrEmpty(authToken);

        string refreshToken = PlayerPrefs.GetString(GameConstants.RefreshTokenKey, null);

        ISession session = null;

        // refresh token can be null/empty for initial migration of client to using refresh tokens.
        if (isAuthToken)
        {
            Debug.Log("isAuthToken!");
            session = Session.Restore(authToken, refreshToken);

            // Check whether a session is close to expiry.
            if (session.HasExpired(DateTime.UtcNow.AddDays(1)))
            {
                try
                {
                    // get a new access token
                    session = await client.SessionRefreshAsync(session);
                    Debug.Log("get a new access token!");
                }
                catch (ApiResponseException)
                {
                    // get a new refresh token
                    session = await client.AuthenticateDeviceAsync(deviceId);
                    PlayerPrefs.SetString(GameConstants.RefreshTokenKey, session.RefreshToken);
                }

                PlayerPrefs.SetString(GameConstants.AuthTokenKey, session.AuthToken);
                Debug.Log("PlayerPrefs.SetString!");
            }
        }
        else
        {
            session = await client.AuthenticateDeviceAsync(deviceId);
            PlayerPrefs.SetString(GameConstants.AuthTokenKey, session.AuthToken);
            PlayerPrefs.SetString(GameConstants.RefreshTokenKey, session.RefreshToken);
        }

        socket.Closed += () => Connect(socket, session);

        Connect(socket, session);

        IApiAccount account = null;
        Debug.Log("Connect(socket, session)!");
        try
        {
            account = await client.GetAccountAsync(session);
            Debug.Log("GetAccountAsync!");
        }
        catch (ApiResponseException e)
        {
            Debug.LogError("Error getting user account: " + e.Message);
        }

        _connection.Init(client, socket, account, session);
        Debug.Log("_connection.Init!");
    }

    private string GetDeviceId()
    {
        string deviceId = "";

        deviceId = PlayerPrefs.GetString(GameConstants.DeviceIdKey);

        if (string.IsNullOrWhiteSpace(deviceId))
        {
            deviceId = Guid.NewGuid().ToString();
        }

        return deviceId;
    }

    private async void OnApplicationQuit()
    {
        await _connection.Socket.CloseAsync();
    }

    private async void Connect(ISocket socket, ISession session)
    {
        try
        {
            if (!socket.IsConnected)
            {
                await socket.ConnectAsync(session);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error connecting socket: " + e.Message);
        }
    }

}
