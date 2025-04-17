using EpamAutomationTests.Core;
using EpamAutomationTests.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EpamAutomationTests.Clients
{
    public class JsonPlaceholderClient : BaseApiClient
    {
        public JsonPlaceholderClient() : base("https://jsonplaceholder.typicode.com")
        {
        }

        public async Task<RestResponse<List<User>>> GetUsersAsync()
        {
            var request = CreateRequest("users", Method.Get);
            return await ExecuteRequestAsync<List<User>>(request);
        }

        public async Task<RestResponse<User>> CreateUserAsync(User user)
        {
            var request = CreateRequest("users", Method.Post);
            request.AddJsonBody(user);
            return await ExecuteRequestAsync<User>(request);
        }

        public async Task<RestResponse> GetInvalidEndpointAsync()
        {
            var request = CreateRequest("invalidendpoint", Method.Get);
            return await ExecuteRequestAsync(request);
        }
    }
} 