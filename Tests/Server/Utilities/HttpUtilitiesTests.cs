using System.Net;
using System.Text;
using Shared.Networking.Http;

namespace Tests.Server.Utilities
{
    [TestFixture]
    public class HttpUtilitiesTests
    {
        [Test]
        public void TryGetAuthenticationToken_ShouldReturnTrueForValidToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://example.com")
            {
                Headers =
                {
                    Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "valid-token")
                }
            };

            bool result = HttpUtilities.TryGetAuthenticationToken(request, out string? token);

            Assert.IsTrue(result);
            Assert.That(token, Is.EqualTo("valid-token"));
        }

        [Test]
        public void TryGetAuthenticationToken_ShouldReturnFalseForInvalidToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

            bool result = HttpUtilities.TryGetAuthenticationToken(request, out string? token);

            Assert.IsFalse(result);
            Assert.IsNull(token);
        }

        [Test]
        public void HttpResponseMessageToString_ShouldReturnValidString()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"message\": \"success\"}", Encoding.UTF8, "application/json")
            };

            string result = HttpUtilities.HttpResponseMessageToString(response);

            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Contains("\"message\": \"success\""));
        }

        [Test]
        public void ParseHttpRequest_ShouldReturnNullForInvalidString()
        {
            string invalidRequestString = "INVALID REQUEST";

            HttpRequestMessage? request = HttpUtilities.ParseHttpRequest(invalidRequestString);
            
            Assert.IsNull(request);
        }

        [Test]
        public void GenerateErrorResponse_ShouldReturnErrorResponse()
        {
            string message = "An error occurred";

            HttpResponseMessage response = HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, message);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains("\"error\": \"BadRequest\""));
        }

        [Test]
        public void GenerateResponse_ShouldReturnValidResponse()
        {
            string content = "{\"message\": \"success\"}";

            HttpResponseMessage response = HttpUtilities.GenerateResponse(HttpStatusCode.OK, content);
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains("\"message\": \"success\""));
        }
    }
}
