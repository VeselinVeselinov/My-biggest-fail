using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
	class BaseButton
	{
		public string TargetFloor { get; set; }

		public bool IsActive { get; set; }

		public string PressButton()
		{
			return this.TargetFloor;
		}
	}
}
