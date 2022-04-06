using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using AGDataAPIService.DTO;
using System.Threading.Tasks;
using System.IO;

namespace AGDataAPIService
{
	public class JsonPlaceholderAPI
	{
		/// <summary>
		/// To get collection of posts using /posts endpoint and GET emthod
		/// </summary>
		/// <returns>List of Post DTO</returns>
		public async Task<List<Post>> GetPosts()
		{
			var restClient = APIHelper.SetUrl("/posts");
			var restRequest = APIHelper.CreateRequest(Method.GET);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var posts = APIHelper.GetContent<List<Post>>(response);

			return posts;
		}

		/// <summary>
		/// To create a new post using /posts endpoint and POST method
		/// </summary>
		/// <param name="createPost">Instance of Post DTO</param>
		/// <returns>Returns instance of Post received as response</returns>
		public async Task<Post> CreatePost(Post createPost)
		{
			var restClient = APIHelper.SetUrl("posts");
			var restRequest = APIHelper.CreateRequest(Method.POST, createPost);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var posts = APIHelper.GetContent<Post>(response);

			return posts;
		}

		/// <summary>
		/// To get a specific post using mentioned post number using /posts endpoint and GET method
		/// </summary>
		/// <param name="postNumber">Post number to get</param>
		/// <returns>>Returns instance of Post received as response</returns>
		public async Task<Post> GetPost(int postNumber)
		{
			var restClient = APIHelper.SetUrl(Path.Combine("/posts", postNumber.ToString()));
			var restRequest = APIHelper.CreateRequest(Method.GET);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var post = APIHelper.GetContent<Post>(response);

			return post;
		}

		/// <summary>
		/// To update a specific post for mentioned post number using /posts endpoint and PUT method
		/// </summary>
		/// <param name="updatePost">Instance of Post DTO for update</param>
		/// <param name="postNumber">Post number to update</param>
		/// <returns>Returns instance of updated Post DTO</returns>
		public async Task<Post> UpdatePost(Post updatePost, int postNumber)
		{
			var restClient = APIHelper.SetUrl(Path.Combine("/posts", postNumber.ToString()));
			var restRequest = APIHelper.CreateRequest(Method.PUT, updatePost);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var posts = APIHelper.GetContent<Post>(response);

			return posts;
		}

		/// <summary>
		/// To delete a specific post of mentioned post number using /posts endpoint and DELETE method
		/// </summary>
		/// <param name="postNumber">Post number to delete</param>
		/// <returns>Returns true if response for delete request is successful, else false</returns>
		public async Task<bool> DeletePost(int postNumber)
		{
			bool statusOK = false;
			var restClient = APIHelper.SetUrl(Path.Combine("/posts", postNumber.ToString()));
			var restRequest = APIHelper.CreateRequest(Method.DELETE);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			if (response.IsSuccessful && response.StatusDescription.Equals("OK"))
				statusOK = true;

			return statusOK;
		}

		/// <summary>
		/// To get collection of comments from specific post of mentioned number using /posts/{num}/comments endpoint as GET method
		/// </summary>
		/// <param name="postNumber">Post number</param>
		/// <returns>Returns collection of Comment DTO</returns>
		public async Task<List<Comment>> GetComments(int postNumber)
		{
			var restClient = APIHelper.SetUrl(Path.Combine("/posts", postNumber.ToString(), "comments"));
			var restRequest = APIHelper.CreateRequest(Method.GET);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var comments = APIHelper.GetContent<List<Comment>>(response);

			return comments;
		}

		/// <summary>
		/// To deserialize the contents to DTO type
		/// </summary>
		/// <typeparam name="DTO">DTO type</typeparam>
		/// <param name="contents">Contents to deserialize</param>
		/// <returns>Returns deserialized instance of DTO type</returns>
		public DTO Deserialize<DTO>(string contents)
		{
			DTO deserializeObject = JsonConvert.DeserializeObject<DTO>(contents);
			return deserializeObject;
		}

		/// <summary>
		/// To create a new comment with a specific post of mentioned number using /posts/{num}/comments endpoint as POST method
		/// </summary>
		/// <param name="postNumber">Post number to add comment with</param>
		/// <param name="comment">Instance of Comment DTO wih a Post</param>
		/// <returns>Returns Comment DTO instance received from response</returns>
		public async Task<Comment> CreateComment(int postNumber, Comment comment)
		{
			var restClient = APIHelper.SetUrl(Path.Combine("/posts", postNumber.ToString(), "comments"));
			var restRequest = APIHelper.CreateRequest(Method.POST, comment);

			var response = await (Task.Run(() => restClient.Execute(restRequest)));
			var createdComment = APIHelper.GetContent<Comment>(response);

			return createdComment;
		}
	}
}
