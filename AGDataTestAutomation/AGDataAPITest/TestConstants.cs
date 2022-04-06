using AGDataAPIService.DTO;

namespace AGDataAPITest
{
	public class TestConstants //Test Data constants
	{
		public static Post FirstPost = new Post()
		{
			UserId = 1,
			Id = 1,
			Title = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
			Body = "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
		};

		public static Post LastPost = new Post()
		{
			UserId = 10,
			Id = 100,
			Title = "at nam consequatur ea labore ea harum",
			Body = "cupiditate quo est a modi nesciunt soluta\nipsa voluptas error itaque dicta in\nautem qui minus magnam et distinctio eum\naccusamus ratione error aut"
		};
	}
}
