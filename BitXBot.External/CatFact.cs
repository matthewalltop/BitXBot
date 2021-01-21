namespace BitXBot.External
{
	using Newtonsoft.Json;

	public class CatFact
    {
		[JsonProperty("fact")]
	    public string Fact { get; set; }

		[JsonProperty("length")]
	    public int Length { get; set; }
    }
}
