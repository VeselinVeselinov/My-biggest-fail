using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
	class Agent
	{
		public string Name { get; set; }

		private Elevator elevator;

		public enum SecutiryCredentials { Confidential, Secret, TopSecret }

		public SecutiryCredentials secutiryCredentials;

		public string currentFloor;

		static Random rand = new Random();

		public Agent(string name, Elevator elevator)
		{
			Name = name;
			this.elevator = elevator;
			secutiryCredentials = (SecutiryCredentials)rand.Next(3);
			currentFloor = GetStartingPoint();
		}

		//Returns the floors that can be accessed with the current security credentials.
		public string CheckCredentials()
		{
			switch (secutiryCredentials)
			{
				case SecutiryCredentials.Confidential:
					return Floors.allFLoors[0];
				case SecutiryCredentials.Secret:
					return Floors.allFLoors[0]+","+Floors.allFLoors[1];
				case SecutiryCredentials.TopSecret:
					return Floors.allFLoors[0] + "," + Floors.allFLoors[1]+ "," + Floors.allFLoors[2] + "," + Floors.allFLoors[3];
				default:
					throw new NotSupportedException("No corresponding security credential exists.");
			}
		}

		//Returns a safe starting floor for the agent to spawn to and by safe i mean a floor that he has security credentials for.
		private string GetStartingPoint()
		{
			var validFloors = CheckCredentials();
			string[] possibleStaringPoint = validFloors.Split(',');
			int possibleScenarios = possibleStaringPoint.Length;

			return possibleStaringPoint[rand.Next(possibleScenarios)];
		}

		//Checks if the agent has permission to enter the floor the elevator has taken him to.
		public bool VerifyDoorOpening()
		{
			var validFloors = CheckCredentials();

			if (validFloors.Contains(currentFloor))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool isInElevator = false;

		public void TransportTheArea51Agent()
		{
			while (true)
			{
				if (isInElevator)
				{
					elevator.PressButton();
					elevator.Travel(this);
					if (VerifyDoorOpening())
					{
						Console.WriteLine($"The doors have opened for agent: {Name} since he has the required credentials: {secutiryCredentials.ToString()}");
						elevator.Leave();
						isInElevator = false;
						break;
					}
					else
					{
						Console.WriteLine($"Unfortunately agent:{Name} does not meet the required credentials for this floor");
						TransportTheArea51Agent();
					}
				}
				else
				{
					elevator.CallElevator(this);
					elevator.Enter(this);
				}
			}
		}
	}
}
