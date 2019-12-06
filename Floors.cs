using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
	class Floors
	{
		public static string[] allFLoors = new string[4] { "G", "S", "T1", "T2" };

		static Random rand = new Random();

		public static string GetRandomFloor()
		{
			return allFLoors[rand.Next(4)];
		}
	}
}
