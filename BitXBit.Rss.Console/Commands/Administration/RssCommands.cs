﻿namespace BitXBit.Rss.Console.Commands
{
	using System.ComponentModel;
	using System.Threading.Tasks;
	using Discord.Commands;

	public class RssCommands: ModuleBase<SocketCommandContext>
	{
		[Command("rss-feed")]
		[Description("Manually pushes first rss feed item from BitxBit Podcast into the channel")]
	    public async Task RssFeed()
	    {
		    var feedReader = new FeedReader();
		    await Context.Channel.SendMessageAsync(feedReader.Read());
		}
    }
}
