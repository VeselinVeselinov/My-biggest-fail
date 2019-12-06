using System;
using System.Linq;
using System.Threading;

namespace TestProject
{
	class Program
	{
		static void Main(string[] args)
		{
			Elevator elevator = new Elevator();
			Agent[] agents = new Agent[]
			{
				new	Agent("Petar",elevator),
				new Agent("Qnislav",elevator),
				new Agent("Alien",elevator)

			};

			var threads = agents.Select(agent => new Thread(agent.TransportTheArea51Agent)).ToArray();
			foreach (var t in threads) t.Start();
			foreach (var t in threads) t.Join();

			Console.WriteLine();
			Console.WriteLine("The elevator crashed due to alien lasers!");
		}
	}
}
