﻿namespace BitXBot.Console.Commands.Fun
{
	using System.Threading.Tasks;
	using Discord.Commands;
	using External;

	public class CatFactCommand: ModuleBase<SocketCommandContext>
    {
		[Command("catfact")]
		[Summary("Sends a catfact")]
	    public async Task CatFactAsync()
		{
			string catFact;
		    using (var client = new ApiClient())
		    {
			    catFact = await client.GetCatFact();
		    }

			await Context.Channel.SendMessageAsync(catFact);
	    }
    }
}
