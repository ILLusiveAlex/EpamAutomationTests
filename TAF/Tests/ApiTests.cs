using EpamAutomationTests.Clients;
using EpamAutomationTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpamAutomationTests.Tests
{
    [TestClass]
    [TestCategory("API")]
    public class ApiTests
    {
        private JsonPlaceholderClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new JsonPlaceholderClient();
        }

        [TestMethod]
        public async Task GetUsers_ShouldReturnListOfUsersWithRequiredFields()
        {
            var response = await _client.GetUsersAsync();

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Count > 0);

            var firstUser = response.Data.First();
            Assert.IsNotNull(firstUser.Id);
            Assert.IsNotNull(firstUser.Name);
            Assert.IsNotNull(firstUser.Username);
            Assert.IsNotNull(firstUser.Email);
            Assert.IsNotNull(firstUser.Address);
            Assert.IsNotNull(firstUser.Phone);
            Assert.IsNotNull(firstUser.Website);
            Assert.IsNotNull(firstUser.Company);
        }

        [TestMethod]
        public async Task GetUsers_ShouldHaveCorrectContentTypeHeader()
        {
            var response = await _client.GetUsersAsync();

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.ContentType);
            Assert.AreEqual("application/json", response.ContentType);
        }

        [TestMethod]
        public async Task GetUsers_ShouldReturnValidUserData()
        {
            var response = await _client.GetUsersAsync();

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(10, response.Data.Count);

            var ids = response.Data.Select(u => u.Id).ToList();
            Assert.AreEqual(ids.Count, ids.Distinct().Count());

            foreach (var user in response.Data)
            {
                Assert.IsFalse(string.IsNullOrEmpty(user.Name));
                Assert.IsFalse(string.IsNullOrEmpty(user.Username));
                Assert.IsFalse(string.IsNullOrEmpty(user.Company.Name));
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldReturnCreatedUserWithId()
        {
            var newUser = new User
            {
                Name = "Test User",
                Username = "testuser"
            };

            var response = await _client.CreateUserAsync(newUser);

            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Id > 0);
        }

        [TestMethod]
        public async Task GetInvalidEndpoint_ShouldReturnNotFound()
        {
            var response = await _client.GetInvalidEndpointAsync();

            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
} 