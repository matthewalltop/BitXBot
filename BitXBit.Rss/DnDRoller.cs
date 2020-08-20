using System;
using System.Collections.Generic;
namespace BitXBit.Rss
{
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

