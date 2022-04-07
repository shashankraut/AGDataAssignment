using System;
using System.Collections.Generic;
using System.Text;

namespace AGDataUITest
{
	/// <summary>
	/// Represents main menus like Solutions, Market, etc.
	/// </summary>
	public enum MainMenu
	{
		Solutions = 0,
		Market = 1,
		Company = 2,
		Resources = 3,
		Contact = 4
	}

	/// <summary>
	/// Represents sub menus like Careers, Overview, etc. from all main menus
	/// </summary>
	public enum SubMenu
	{
		Overview = 0,
		Careers = 1,
		LeaderShip = 2
	}
}
