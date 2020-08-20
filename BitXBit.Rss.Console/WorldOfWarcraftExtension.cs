

namespace BitXBit.Rss.Console
{
	using System.Linq;
	using System.Threading.Tasks;

	using Discord;
	using Discord.WebSocket;

	public static class WorldOfWarcraftExtension
    {
	    public static async Task<bool> ContainsWorldOfWarcraftReferences(this SocketUserMessage message, SocketGuild guild, int argPos)
	    {
		    string[] worldOfWarcraftReferenceDict = {"WOW", "World of Warcraft", "w.o.w", "w.o.w.",
			    "Do you play wow", "Will you be playing wow", "Have you played wow", "Have you ever played wow", "Is wow among those",
				"Will you play wow with me", "Let's play wow", "Anyone want to play wow", "Are there any wow players here"
		    };

		    var containsWorldOfWarcraftReference = worldOfWarcraftReferenceDict.Select(reference => message.Content.Contains(reference))
			    .Any(messageContainsWorldOfWarcraftReference => messageContainsWorldOfWarcraftReference);

		    if (!containsWorldOfWarcraftReference) return false;

		    var userWhoPostedWowReference = message.Author;
		    await userWhoPostedWowReference.SendMessageAsync(
			    @"We noticed you have been posting about World of Warcraft. Continuing this behavior qualifies for an immediate and irreversible ban.
Please tread lightly");

		    await guild.AddBanAsync(userWhoPostedWowReference);
			
			await message.DeleteAsync();
		    return true;

	    }
	}
}
