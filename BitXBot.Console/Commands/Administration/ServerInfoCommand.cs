namespace BitXBot.Console.Commands.Administration
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading.Tasks;
	using Discord;
	using Discord.Commands;
	using External;

	public class ServerInfoCommand : ModuleBase<SocketCommandContext>
	{ 
		[Command("server-info")]
		[Description("Shows server info in channel.")]
		public async Task ServerInfo()
		{
			var guildInfo = Context.Guild;
			//var bossBotMessage = await Context.Channel.GetMessageAsync(501513017742196746);

			var serverInfo = new ServerInfo
			{
				ServerName = guildInfo.Name,
				Owner = guildInfo.Owner.Username,
				Region = guildInfo.VoiceRegionId,
				TextChannels = guildInfo.TextChannels.Count,
				VoiceChannels = guildInfo.VoiceChannels.Count,
				MemberCount = guildInfo.MemberCount,
				HumanCount = guildInfo.Users.Count(x => !x.IsBot),
				BotCount = guildInfo.Users.Count(x => x.IsBot),
				OnlineCount = guildInfo.Users.Count(x => x.Status != UserStatus.Offline),
				RoleCount = guildInfo.Roles.Count,
			};

			var concreteEmbed = GetEmbed(serverInfo);
			await Context.Channel.SendMessageAsync("", false, concreteEmbed);
		}

		public Embed GetEmbed(ServerInfo serverInfo)
		{
			var guildInfo = Context.Guild;
			var embedBuilder = new EmbedBuilder
			{
				Author = new EmbedAuthorBuilder
				{
					IconUrl = guildInfo.IconUrl,
					Name = guildInfo.Name,
					Url = guildInfo.SplashUrl
				},
				Color = GetRandomColor(),
				ThumbnailUrl = guildInfo.IconUrl,
				Timestamp = DateTime.Now,
			};
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Owner",
				Value = serverInfo.Owner

			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Region",
				Value = serverInfo.Region
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Text Channels",
				Value = serverInfo.TextChannels
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Voice Channels",
				Value = serverInfo.VoiceChannels
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Members",
				Value = serverInfo.MemberCount
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Humans",
				Value = serverInfo.HumanCount
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Bots",
				Value = serverInfo.BotCount
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Online",
				Value = serverInfo.OnlineCount
			});
			embedBuilder.Fields.Add(new EmbedFieldBuilder
			{
				IsInline = true,
				Name = "Roles",
				Value = serverInfo.RoleCount
			});

			return embedBuilder.Build();
		}
		public Color GetRandomColor()
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
