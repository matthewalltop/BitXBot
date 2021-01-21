namespace BitXBot.External
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Newtonsoft.Json;

	public class ApiClient : IDisposable
    {
	    private readonly Uri _catFactUri = new Uri("https://catfact.ninja/fact");
	    private readonly HttpClient _client;
	    public ApiClient()
	    {
			this._client = new HttpClient();
	    }

	    public async Task<string> GetCatFact()
			=> JsonConvert.DeserializeObject<CatFact>(await this._client.GetStringAsync(_catFactUri)).Fact;

	    public void Dispose()
	    {
			this._client.Dispose();
	    }

    }
}
