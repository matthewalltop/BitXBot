namespace BitXBot.Console
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Discord.WebSocket;

	public static class HelloThere
	{
		public static async Task<bool> GeneralKenobi(this SocketUserMessage message)
		{
			const string grievousUrl = "https://giphy.com/gifs/8JTFsZmnTR1Rs1JFVP";
			var sanitizedString = new string(message.Content.ToCharArray().Where(c => !char.IsPunctuation(c)).ToArray())
				.Replace(" ", "").ToLowerInvariant();

			if (!sanitizedString.Equals("HelloThere", StringComparison.InvariantCultureIgnoreCase)) return false;
			await message.Channel.SendMessageAsync(grievousUrl);
			return true;

		}
	}
}
