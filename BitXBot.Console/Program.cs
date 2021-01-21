namespace BitXBot.Console
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using Discord;
	using Discord.Commands;
	using Discord.WebSocket;

	using Commands.Fun;
	using System.IO;

	public class Program
    {
		private CommandService _commands;
	    private DiscordSocketClient _client;
	    private IServiceProvider _services;
		private static IConfiguration Configuration { get; set; }
		private static string _discordToken;

		// TODO: Finish setting up the configuration object.
		private static void Main(string[] args)
		{
			Configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddCommandLine(args)
					.Build();

			_discordToken = Configuration["DiscordToken"];

			new Program().StartAsync().GetAwaiter().GetResult();
		}

		public async Task StartAsync()
	    {
		    _client = new DiscordSocketClient();
			_commands = new CommandService();

		    _services = new ServiceCollection()
			    .AddSingleton(_client)
			    .AddSingleton(_commands)
			    .BuildServiceProvider();

		    await InstallCommandsAsync();

			_client.Log += Log;
		    _client.UserJoined += AnnounceJoinedUser;
		    _client.UserBanned += PwnUserAfterBan;
			await _client.LoginAsync(TokenType.Bot, _discordToken);
		    await _client.StartAsync();

			await Task.Delay(-1);
	    }

	    public async Task InstallCommandsAsync()
	    {
		    _client.MessageReceived += HandleCommandsAsync;
		    await _commands.AddModulesAsync(Assembly.GetAssembly(typeof(CatFactCommand)));
			Console.WriteLine("Installing Commands");
	    }

	    private Task Log(LogMessage message)
	    {
			Console.WriteLine(message);
		    return Task.CompletedTask;
	    }

	    private async Task HandleCommandsAsync(SocketMessage messageParam)
	    {
		    // Don't process the command if it was a System Message
		    if (!(messageParam is SocketUserMessage message)) return;
		    // Create a number to track where the prefix ends and the command begins
		    var argPos = 0;

			//TODO: Refactor to process reply-only commands.
		    //if (await message.ContainsWorldOfWarcraftReferences(guild, argPos)) return;
			if (await message.GeneralKenobi()) return;


		    // Determine if the message is a command, based on if it starts with '!' or a mention prefix
		    if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
		    // Create a Command Context
		    var context = new SocketCommandContext(_client, message);
		    // Execute the command. (result does not indicate a return value, 
		    // rather an object stating if the command executed successfully)
		    await _commands.ExecuteAsync(context, argPos, _services);
		    //if (!result.IsSuccess)
			   // await context.Channel.SendMessageAsync(result.ErrorReason);
		}

	    public async Task AnnounceJoinedUser(SocketGuildUser user) //Welcomes the new user
	    {
			// Find a way to remove the magic number here.
		    var channel = _client.GetChannel(743180167421362288) as SocketTextChannel; // Gets the channel to send the message in
		    await channel.SendMessageAsync($"Hey {user.Mention},welcome to **{channel.Guild.Name}**! Thanks for joining the community!"); //Welcomes the new user
		    await channel.SendMessageAsync(string.Empty, false, GetUserEmbed(user));
		}

	    public async Task PwnUserAfterBan(SocketUser user, SocketGuild guild)
	    {
		    // Find a way to remove the magic number here.
		    var channel = _client.GetChannel(743180167421362288) as SocketTextChannel; // Gets the channel to send the message in
			await channel.SendMessageAsync($"Hey {user.Mention}, You got super fucking banned from **{channel.Guild.Name}**! Thanks for leaving the community!"); //Welcomes the new user
			await channel.SendFileAsync(@"C:\Users\Matthew\source\repos\BitXBot\bitbotSticker.png", "Eat shit, nerd!");
		}

	    private Embed GetUserEmbed(IUser user)
	    {
		    // Find a way to remove the magic number here.
			var guildInfo = _client.GetGuild(741733933439385690);
			var embedBuilder = new EmbedBuilder
			{
				Color = GetRandomColor(),
				ImageUrl = user.GetAvatarUrl(),
				Timestamp = DateTime.Now,
				
				Footer = new EmbedFooterBuilder
				{
					Text = $"You are user #{guildInfo.Users.Count} to join the server."
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
