using System.Runtime.Serialization;

public class MatchEndResponse
{
	[DataMember(Name = "gems")]
	public int Gems { get; private set; }

	[DataMember(Name = "score")]
	public int Score { get; private set; }
}
