namespace BitXBot.External
{
	using System;

	public class DnDRoller
    {
		private Random Random { get; set; }

		public int GetDieRoll(int dieSide)
		{
			if (this.Random == null)
			{
				this.Random = new Random();
			}

			return this.Random.Next(1, dieSide + 1);
		}
    }
}

