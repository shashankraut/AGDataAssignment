using System;
using System.Collections.Generic;
using System.Text;

namespace AGDataAPIService.DTO
{
	/// <summary>
	/// Class represents comments data type object
	/// </summary>
	public class Comment
	{
		public long PostId { get; set; }
		public long Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Body { get; set; }
	}
}
