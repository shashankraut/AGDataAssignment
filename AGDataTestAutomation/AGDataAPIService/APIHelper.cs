using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AGDataAPIService
{
	public class APIHelper
	{
		private static string APIBaseURL = "https://jsonplaceholder.typicode.com/";

		/// <summary>
		/// To setup the url and create a instance of RestClient
		/// </summary>
		/// <param name="endpoint">endpoint URL</param>
		/// <returns>returns instance of RestClient</returns>
		public static RestClient SetUrl(string endpoint)
		{
			var url = Path.Combine(APIBaseURL, endpoint.TrimStart('/'));
			var restClient = new RestClient(url);
			return restClient;
		}

		/// <summary>
		/// To prepare a RestRequest instance according to Method type
		/// </summary>
		/// <param name="method">Method type [e.g. Method.GET]</param>
		/// <param name="payload">[Default null] payload data, required in case of PUT and POST methods</param>
		/// <returns>returns prepared instance of ResrRequest</returns>
		public static RestRequest CreateRequest(Method method, object payload = null)
		{
			var restRequest = new RestRequest(method);
			restRequest.AddHeader("Accept", "application/json");
			restRequest.RequestFormat = DataFormat.Json;

			if (method == Method.POST || method == Method.PUT)
			{
				restRequest.AddHeader("Content-type", "application/json, charset=UTF-8");
				restRequest.AddBody(payload);
			}

			return restRequest;
		}

		/// <summary>
		/// To execute prepared request and get the response
		/// </summary>
		/// <param name="client">RestClient instance</param>
		/// <param name="request">RestRequest instance</param>
		/// <returns>returns response of execute as IRestResponse</returns>
		public async static Task<IRestResponse> GetResponse(RestClient client, RestRequest request)
		{
			return await (Task.Run(() => client.Execute(request)));
		}

		/// <summary>
		/// To get contents of a response after deserializing to mentioned DTO
		/// </summary>
		/// <typeparam name="DTO">DTO Tyoe</typeparam>
		/// <param name="response">Rest Response</param>
		/// <returns>returns instance of DTO</returns>
		public static DTO GetContent<DTO>(IRestResponse response)
		{
			var content = response.Content;
			DTO dtoType = JsonConvert.DeserializeObject<DTO>(content);

			return dtoType;
		}
	}
}