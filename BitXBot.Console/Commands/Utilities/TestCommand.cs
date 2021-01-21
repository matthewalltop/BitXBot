namespace BitXBot.Console.Commands.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading.Tasks;
	using Discord;
	using Discord.Commands;
	using Discord.WebSocket;

	public class TestCommand : ModuleBase<SocketCommandContext>
	{
		[Command("test")]
		[Description("Shows server rules in channel.")]
		public async Task Test()
		{

			var userWhoSentMessage = (SocketGuildUser)Context.Message.Author;

			var userIsAdmin = userWhoSentMessage.Roles.Any(x =>
				x.Name.Equals("Mods", StringComparison.InvariantCultureIgnoreCase) ||
				x.Name.Equals("Hosts", StringComparison.InvariantCultureIgnoreCase));

			//if (!userIsAdmin)
			//{
			//	await Context.Channel.SendMessageAsync(
			//		$" {userWhoSentMessage.Mention} Sorry, scrub. You don't have permission to use this command.");
			//	return;
			//}

			var user = Context.User;

			await Context.Channel.SendMessageAsync($"Hey {user.Mention}, You got super fucking banned from **{Context.Guild.Name}**! Thanks for leaving the community!");
			await Context.Channel.SendFileAsync(@"Resources\bitbotSticker.png", "Eat shit, nerd!");
		}

		private Embed GetUserEmbed(IUser user)
		{
			// Find a way to remove the magic number here.
			var guildInfo = Context.Guild;
			var embedBuilder = new EmbedBuilder
			{
				ImageUrl = user.GetAvatarUrl(),
				Timestamp = DateTime.Now,
				Footer = new EmbedFooterBuilder
				{
					Text = $"You are user #{guildInfo.Users.Count} to join the server.\n"
				}
			};

			return embedBuilder.Build();
		}

		private static Color GetRandomColor()
		{
			var colors = new List<Color>
			{
				Color.DarkBlue,
				Color.DarkGreen,
				Color.DarkGrey,
				Color.DarkMagenta,
				Color.DarkOrange,
				Color.DarkMagenta,
				Color.DarkPurple,
			};
			var random = new Random();

			var randomColor = colors.ElementAt(random.Next(colors.Count));
			return randomColor;
		}



	}
}
