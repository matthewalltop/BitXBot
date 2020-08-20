namespace BitXBit.Rss.Console.Commands
{
	using System;
	using System.ComponentModel;
	using System.Threading.Tasks;
	using System.Linq;
	using Discord.Commands;
	using Discord.WebSocket;


	public class ClearMessagesCommand: ModuleBase<SocketCommandContext>
	{
		[Command("clear")]
		[Description("Clears given number of messages from channel")]
		public async Task ClearMessages(int messageCount)
		{
			var userWhoSentMessage = (SocketGuildUser) Context.Message.Author;

			var userIsAdmin = userWhoSentMessage.Roles.Any(x =>
				x.Name.Equals("Mods", StringComparison.InvariantCultureIgnoreCase) ||
				x.Name.Equals("Hosts", StringComparison.InvariantCultureIgnoreCase));

			if (!userIsAdmin)
			{
				await Context.Channel.SendMessageAsync(
					$" {userWhoSentMessage.Mention} Sorry, scrub. You don't have permission to use this command.");
				return;
			}

			var messages =
				await Context.Channel.GetMessagesAsync(messageCount).Skip(1).FirstOrDefault();

			await Context.Channel.DeleteMessagesAsync(messages);
		}
    }
}
