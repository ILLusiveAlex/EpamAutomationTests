using RestSharp;
using System;
using System.Threading.Tasks;

namespace EpamAutomationTests.Core
{
    public class BaseApiClient
    {
        protected readonly RestClient _client;
        protected readonly string _baseUrl;

        public BaseApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new RestClient(baseUrl);
        }

        protected async Task<RestResponse<T>> ExecuteRequestAsync<T>(RestRequest request)
        {
            Logger.Info($"Executing {request.Method} request to {_baseUrl}{request.Resource}");
            
            try
            {
                var response = await _client.ExecuteAsync<T>(request);
                Logger.Info($"Response status code: {response.StatusCode}");
                
                if (!response.IsSuccessful)
                {
                    Logger.Error($"Request failed: {response.ErrorMessage}");
                }
                
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error executing request: {ex.Message}");
                throw;
            }
        }

        protected async Task<RestResponse> ExecuteRequestAsync(RestRequest request)
        {
            Logger.Info($"Executing {request.Method} request to {_baseUrl}{request.Resource}");
            
            try
            {
                var response = await _client.ExecuteAsync(request);
                Logger.Info($"Response status code: {response.StatusCode}");
                
                if (!response.IsSuccessful)
                {
                    Logger.Error($"Request failed: {response.ErrorMessage}");
                }
                
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error executing request: {ex.Message}");
                throw;
            }
        }

        protected RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Accept", "application/json");
            return request;
        }
    }
} 