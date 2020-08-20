
namespace BitXBit.Rss.Console.Commands
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Discord.Commands;
	public class DieRollerCommand : ModuleBase<SocketCommandContext>
	{
		[Command("d")]
		[Summary("Rolls a DnD die")]
		public async Task RollDie(int dieToRoll)
		{
			var allowedDice = new List<int> { 4, 6, 8, 10, 12, 20 };
			if (!allowedDice.Contains(dieToRoll))
				return;

			var dieRoll = new DnDRoller();
			await Context.Channel.SendMessageAsync($"d{dieToRoll} : You rolled a {dieRoll.GetDieRoll(dieToRoll)}");
		}
	}
}
