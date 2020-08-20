namespace BitXBit.Rss.Console.Commands.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading.Tasks;
	using Discord;
	using Discord.Commands;

	public class TestCommand : ModuleBase<SocketCommandContext>
	{
		[Command("test")]
		[Description("Shows server rules in channel.")]
		public async Task Test()
		{
			var user = Context.User;

			var embed = GetUserEmbed(user);

			await Context.Channel.SendMessageAsync(string.Empty, false, embed);
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
