using NUnit.Framework;
using AGDataAPIService;
using AGDataAPIService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using KellermanSoftware.CompareNetObjects;
using AventStack.ExtentReports;
using System.IO;
using TestReporting;

namespace AGDataAPITest
{
	[TestFixture]
	public class APITests : TestBase
	{
		#region Member Declaration
		private JsonPlaceholderAPI placeholderAPI;
		private CompareLogic compareLogic;
		#endregion

		public APITests()
		{
			placeholderAPI = new JsonPlaceholderAPI();
			compareLogic = new CompareLogic();
		}

		[Test, Description("Fetch and verify posts using /posts GET endpoint")]
		[Category("APITests")]
		public async Task VerifyGetListOfPosts()
		{

			List<Post> listOfPosts = await placeholderAPI.GetPosts();
			Assert.AreEqual(listOfPosts.Count, 100); //Verify count of posts found
			Reporting.Log(Status.Pass, "Posts count found as expected - 100");

			//Comparing first post
			var actualFirstPost = from post in listOfPosts where post.Id == 1 select post;
			ComparisonResult result = compareLogic.Compare(TestConstants.FirstPost, actualFirstPost.FirstOrDefault());
			Assert.IsTrue(result.AreEqual, "Expected and actual first post from list are found equal");

			//Comparing last post
			var actualLastPost = from post in listOfPosts where post.Id == 100 select post;
			result = compareLogic.Compare(TestConstants.LastPost, actualLastPost.FirstOrDefault());
			Assert.IsTrue(result.AreEqual, "Expected and actual last post from list are found equal");

			Reporting.Log(Status.Pass, "Verification for received list of posts is successful");
		}

		[Test, Description("Creating and verify a new post using /posts POST endpoint")]
		[Category("APITests")]
		public async Task VerifyCreatePosts()
		{
			Post newPost = new Post() { UserId = 1, Body = "Sample post body", Title = "Sample post title" };

			Post actualPost = await placeholderAPI.CreatePost(newPost);
			Assert.IsNotNull(actualPost, "Post is created successfully");
			Assert.AreEqual(actualPost.Id, 101, "Expected and created post ID's are found equal");

			Reporting.Log(Status.Pass, "Create and verify post is successful");
		}

		[Test, Description("Get amd verify first post using /posts/{num} GET endoint")]
		[Category("APITests")]
		public async Task VerifyGetPost()
		{
			Post post = await placeholderAPI.GetPost(1);
			Assert.IsNotNull(post, "Post is fetched successfully");

			ComparisonResult result = compareLogic.Compare(TestConstants.FirstPost, post);
			Assert.IsTrue(result.AreEqual, "Expected and actual post is found equal");

			Reporting.Log(Status.Pass, "Fetching and verification for first post is successful");
		}

		[Test, Description("Update and verify first post using /posts/{num} PUT endpoint")]
		[Category("APITests")]
		public async Task VerifyUpdatePost()
		{
			Post postToUpdate = TestConstants.FirstPost;
			postToUpdate.Body = "Sample updated body";
			postToUpdate.Title = "Sample updated title";

			Post actualPost = await placeholderAPI.UpdatePost(postToUpdate, 1);
			Assert.IsNotNull(actualPost, "Post is fetched successfully");
			Assert.AreEqual(actualPost.Id, postToUpdate.Id, "Expected and created post ID's are found equal");

			Reporting.Log(Status.Pass, "Update and verification for first post is successful");
		}

		[Test, Description("Delete and verify first post using /posts/{num} DELETE endpoint")]
		[Category("APITests")]
		public async Task VerifyDeletePost()
		{
			int postNumber = 2;
			bool deleteStatus = await placeholderAPI.DeletePost(postNumber);
			Assert.IsTrue(deleteStatus, "Status of delete request is not found as OK");

			Reporting.Log(Status.Pass, $"Delete and verification for post {postNumber} is successful");
		}


		[Test, Description("Get amd verify comments from post using /posts/{num}/comments GET endoint")]
		[Category("APITests")]
		public async Task VerifyGetComments()
		{
			int postNumber = 1;
			List<Comment> actualComments = await placeholderAPI.GetComments(postNumber);
			Assert.NotZero(actualComments.Count, $"Count for comments from post {postNumber} found 0");

			string commentsJson = File.ReadAllText(@"Comments.json");
			List<Comment> expectedComments = placeholderAPI.Deserialize<List<Comment>>(commentsJson);
			ComparisonResult result = compareLogic.Compare(expectedComments, actualComments);
			Assert.IsTrue(result.AreEqual, $"Expected and actual comments from post {postNumber} are found equal");

			Reporting.Log(Status.Pass, $"Fetching and verification for comments from post {postNumber} is successful");
		}

		[Test, Description("Creating and verify a new comment with post using /posts/{num}/comments POST endpoint")]
		[Category("APITests")]
		public async Task VerifyCreateComments()
		{
			int postNumber = 1;
			Comment expectedComment = new Comment() { PostId = postNumber, Name = "Sample comment name", Body = "Sample comment body", Email = "newcomment@withpost.com" };

			Comment actualComment = await placeholderAPI.CreateComment(postNumber, expectedComment);
			Assert.IsNotNull(actualComment, $"Comment is not created with post {postNumber}");
			Assert.IsNotNull(actualComment.Id, $"ID not found with comment created with post {postNumber}");

			expectedComment.Id = actualComment.Id;
			ComparisonResult result = compareLogic.Compare(expectedComment, actualComment);
			Assert.IsTrue(result.AreEqual, $"Expected and actual comment from post {postNumber} are found equal");

			Reporting.Log(Status.Pass, $"Create and verify comment with post {postNumber} is successful");
		}
	}
}