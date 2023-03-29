
using System.Runtime.Serialization;
using Nakama;

public class MatchEndRequest
{
	[DataMember(Name = "matchId")]
	public string MatchId { get; private set; }

	[DataMember(Name = "placement")]
	public MatchEndPlacement Placement { get; private set; }

	[DataMember(Name = "time")]
	public float Time { get; private set; }

	[DataMember(Name = "towersDestroyed")]
	public int TowersDestroyed { get; private set; }

	public MatchEndRequest(string matchId, MatchEndPlacement placement, float time, int towersDestroyed)
	{
		MatchId = matchId;
		Placement = placement;
		Time = time;
		TowersDestroyed = towersDestroyed;
	}
}

