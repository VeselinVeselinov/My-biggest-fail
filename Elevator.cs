using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestProject
{
	class Elevator
	{
		private Semaphore semaphore;
		private Agent passenger;

		public string currentFloor = Floors.GetRandomFloor();
		public string targetFloor;
		public bool isOccupied = false;
		public static Random rand = new Random();

		public BaseButton[] elevatorButtons = new BaseButton[4]
		{
			new BaseButton(){ TargetFloor ="G", IsActive=true },
			new BaseButton(){ TargetFloor ="S", IsActive=true },
			new BaseButton(){ TargetFloor ="T1", IsActive=true },
			new BaseButton(){ TargetFloor ="T2", IsActive=true }
		};

		public Elevator()
		{
			semaphore = new Semaphore(1,1);
		}

		public void CallElevator(Agent agent)
		{
			semaphore.WaitOne();
			passenger = agent;

			Console.WriteLine($"The elevator is at floor {currentFloor}");
			Console.WriteLine($"Agent:{agent.Name} has called the elevator from floor:{agent.currentFloor}");

			targetFloor = agent.currentFloor;

			if (targetFloor==currentFloor)
			{
				Console.WriteLine($"The elevator is on floor {currentFloor} and it is opens its doors for {agent.Name}");
			}
			else
			{
				Travel(agent);
			}

		}

		public void Enter(Agent agent)
		{
			agent.isInElevator = true;
		}

		public void Travel(Agent agent)
		{
			int time = CalculateTravelTime(currentFloor, targetFloor);

			Thread.Sleep(time * 1000);

			Console.WriteLine("The elevator is travelling in the current direction.");

			currentFloor = targetFloor;
			agent.currentFloor = currentFloor;
			targetFloor = null;

			Console.WriteLine($"The elevator has arrived at floor {currentFloor}");
		}

		public void Leave()
		{
			semaphore.Release();
			passenger = null;
		}

		public void PressButton()
		{
			targetFloor = elevatorButtons[rand.Next(4)].PressButton();
			while (targetFloor==currentFloor)
			{
				PressButton();
			}
			Console.WriteLine($"Agent:{passenger.Name} has set the elevator to travel to floor:{targetFloor}");
		}

		//Calculates how much seconds the elevator will travel between two floors.
		private int CalculateTravelTime(string currentFloor, string targetFloor)
		{
			var startPoint=Array.IndexOf(Floors.allFLoors,currentFloor);
			var endPoint = Array.IndexOf(Floors.allFLoors, currentFloor);

			return Math.Abs(endPoint - startPoint);
		}
	}
}
