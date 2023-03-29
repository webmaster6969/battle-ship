public static class GameConstants
{
	//  Fields ----------------------------------------

	// CreateAssetMenu
	private const string CreateAssetMenu = "Pirate Panic/";
	public const string CreateAssetMenu_AvatarSprites = CreateAssetMenu + "AvatarSprites";
	public const string CreateAssetMenu_Card = CreateAssetMenu + "Card";
	public const string CreateAssetMenu_CardList = CreateAssetMenu + "Card List";
	public const string CreateAssetMenu_GameConfiguration = CreateAssetMenu + "GameConfiguration";
	public const string CreateAssetMenu_GameConnection = CreateAssetMenu + "GameConnection";

	// MenuItem
	private const string MenuItem = "Window/Pirate Panic/";
	public const string MenuItem_OpenPiratePanicDocumentation = MenuItem + "Open Docs: Pirate Panic";
	public const string MenuItem_OpenNakamaDocumentation = MenuItem + "Open Docs: Nakama";
	public const string MenuItem_OpenDeveloperConsole = MenuItem + "Open Developer Console";

	// PlayerPrefs
	public const string DeviceIdKey = "nakama.deviceId";
	public static string AuthTokenKey = "nakama.authToken";
	public static string RefreshTokenKey = "nakama.refreshToken";

	// Urls
	public const string DocumentationUrl_PiratePanic = "https://github.com/heroiclabs/unity-sampleproject";
	public const string DocumentationUrl_Nakama = "https://heroiclabs.com/docs/index.html";
	public const string DeveloperConsoleUrl = "http://localhost:7351/";


}
