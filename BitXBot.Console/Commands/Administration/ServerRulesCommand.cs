namespace BitXBot.Console.Commands.Administration
{
	using System.ComponentModel;
	using System.Threading.Tasks;
	using Discord.Commands;

	public class ServerRulesCommand : ModuleBase<SocketCommandContext>
	{
		[Command("server-rules")]
		[Description("Shows server rules in channel.")]
		public async Task ServerRules()
		{
			var info = Context.Guild.Id;

			await Context.Channel.SendMessageAsync($"{info}");
		}
	}
}
