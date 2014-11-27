using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using FindShelter.Web;

namespace FindShelter.Web.Test
{
    [TestClass]
    public class WebAppTest
    {
        [TestMethod]
        public async Task ApiValuesDefaultReturnsString()
        {
            await RunWithTestServer("/api/Values", HttpStatusCode.OK, async response =>
            {
                string actual = await response.Content.ReadAsStringAsync();
                Assert.AreEqual("[\"a\",\"b\",\"c\"]", actual);
            });
        }

        private static async Task RunWithTestServer(string route, HttpStatusCode expectedStatusCode, Func<HttpResponseMessage, Task> validateResponse = null)
        {
            using (var server = TestServer.Create<Startup>())
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync(route);
                Assert.IsNotNull(response);
                Assert.AreEqual(expectedStatusCode, response.StatusCode);
                if (validateResponse != null) await validateResponse(response);
            }
        }

        [TestMethod]
        public async Task DefaultPageNotFound()
        {
            await RunWithTestServer("/", HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task CreateTestServer()
        {
            using (var server = TestServer.Create(app =>
            {
                app.UseErrorPage(); // See Microsoft.Owin.Diagnostics
                app.Run(context => context.Response.WriteAsync("Hello world using OWIN TestServer"));
            }))
            {
                HttpResponseMessage response = await server.HttpClient.GetAsync("/");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
