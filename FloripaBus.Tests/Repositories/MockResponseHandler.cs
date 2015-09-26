using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace FloripaBus.Tests
{
	public class MockResponseHandler : DelegatingHandler
	{
		private readonly IDictionary<Uri, HttpResponseMessage> _responses;

		public MockResponseHandler()
		{
			_responses = new Dictionary<Uri, HttpResponseMessage>(); 
		}

		public void AddResponse(string uri, HttpResponseMessage responseMessage)
		{
			_responses.Add(new Uri(uri),responseMessage);
		}

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (_responses.ContainsKey(request.RequestUri))
				return _responses[request.RequestUri];
			
			return new HttpResponseMessage (HttpStatusCode.NotFound) { RequestMessage = request };
		}
	}
}

