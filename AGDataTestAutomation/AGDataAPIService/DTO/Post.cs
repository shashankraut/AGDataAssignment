namespace AGDataAPIService.DTO
{
	/// <summary>
	/// Class respresents post data type object
	/// </summary>
	public class Post
	{
		public long UserId { get; set; }
		public long Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
